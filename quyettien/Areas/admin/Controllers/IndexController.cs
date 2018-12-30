using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyettien.Models;

namespace quyettien.Areas.admin.Controllers
{
    public class IndexController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: admin/Index
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInfo()
        {
            var sqlQuantityCB = "select SUM(Quantity) from CashBillDetail";
            var sqlQuantityIB = "select SUM(Quantity) from InstallmentBIllDetail";
            var sqlRevenueCB = "select SUM(GrandTotal) from CashBill where DATEPART(MONTH,Date) = MONTH(GETDATE())";
            var sqlRevenueIB = "select SUM(Taken) from InstallmentBill where DATEPART(MONTH,Date) = MONTH(GETDATE())";
            var sqlCB = "select COUNT(*) from CashBill";
            var sqlIB = "select COUNT(*) from InstallmentBill";
            var sqlProduct = "Select COUNT(*) from Product";


            var quantity = db.Database.SqlQuery<int>(sqlQuantityCB).First() + db.Database.SqlQuery<int>(sqlQuantityIB).First();
            var revenue = db.Database.SqlQuery<int>(sqlRevenueCB).First() + db.Database.SqlQuery<int>(sqlRevenueIB).First();
            var orders = db.Database.SqlQuery<int>(sqlCB).First() + db.Database.SqlQuery<int>(sqlIB).First();
            var product = db.Database.SqlQuery<int>(sqlProduct).First();

            return Json(new {
                sanPhamDaBan = quantity,
                doanhThuThang = revenue,
                tongHoaDon = orders,
                tongSanPham = product,
            });
        }

        public JsonResult GetChart()
        {
            var sql = "SELECT pt.ProductTypeName category, COUNT(p.id) quantity FROM ProductType pt INNER JOIN Product p ON pt.id = p.ProductTypeID GROUP BY pt.ProductTypeName ORDER BY COUNT(p.id) DESC";

            var chart = db.Database.SqlQuery<Object>(sql);
            //var chart = db.Database.SqlQuery<string>(sql).ToList();
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}