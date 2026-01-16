using HRMS.Data;
using HRMS.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{
    [AuthorizeRole("Master")] // 🔐 ADMIN ONLY
    public class SchemaController : Controller
    {
        private readonly AppDbContext _context;

        public SchemaController(AppDbContext context)
        {
            _context = context;
        }

        // ================= VIEW TABLES =================
        public IActionResult Index()
        {
            var tables = _context.Database
                .SqlQueryRaw<string>(
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'")
                .ToList();

            return View(tables);
        }

        // ================= VIEW COLUMNS =================
        public IActionResult Columns(string tableName)
        {
            var columns = _context.Database
                .SqlQueryRaw<ColumnInfo>(
                    @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
                      FROM INFORMATION_SCHEMA.COLUMNS
                      WHERE TABLE_NAME = @table",
                    new SqlParameter("@table", tableName))
                .ToList();

            ViewBag.TableName = tableName;
            return View(columns);
        }

        // ================= CREATE TABLE =================
        [HttpGet]
        public IActionResult CreateTable()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTable(string tableName)
        {
            var sql = $"CREATE TABLE [{tableName}] (Id INT IDENTITY(1,1) PRIMARY KEY)";
            _context.Database.ExecuteSqlRaw(sql);

            return RedirectToAction(nameof(Index));
        }

        // ================= ADD COLUMN =================
        [HttpGet]
        public IActionResult AddColumn(string tableName)
        {
            ViewBag.TableName = tableName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddColumn(string tableName, string columnName, string dataType, bool allowNull)
        {
            var nullSql = allowNull ? "NULL" : "NOT NULL";

            var sql = $@"
                ALTER TABLE [{tableName}]
                ADD [{columnName}] {dataType} {nullSql}
            ";

            _context.Database.ExecuteSqlRaw(sql);

            return RedirectToAction("Columns", new { tableName });
        }
        // ================= DELETE COLUMN =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteColumn(string tableName, string columnName)
        {
            // 🔒 Prevent deleting Id (primary key)
            if (columnName.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Primary key column cannot be deleted.";
                return RedirectToAction("Columns", new { tableName });
            }

            var sql = $@"
        ALTER TABLE [{tableName}]
        DROP COLUMN [{columnName}]
    ";

            _context.Database.ExecuteSqlRaw(sql);

            TempData["Success"] = $"Column '{columnName}' deleted successfully.";
            return RedirectToAction("Columns", new { tableName });
        }
        // ================= DELETE TABLE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTable(string tableName)
        {
            // 🔒 Block critical tables
            var protectedTables = new[]
            {
        "Employees",
        "AppUsers",
        "__EFMigrationsHistory"
    };

            if (protectedTables.Contains(tableName, StringComparer.OrdinalIgnoreCase))
            {
                TempData["Error"] = $"Table '{tableName}' is protected and cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            var sql = $"DROP TABLE [{tableName}]";
            _context.Database.ExecuteSqlRaw(sql);

            TempData["Success"] = $"Table '{tableName}' deleted successfully.";
            return RedirectToAction(nameof(Index));
        }


    }


    // ================= HELPER MODEL =================
    public class ColumnInfo
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string IS_NULLABLE { get; set; }
    }
}