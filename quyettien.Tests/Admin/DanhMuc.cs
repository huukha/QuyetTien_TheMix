using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using quyettien.Areas.admin.Controllers;
using quyettien.Models;

namespace quyettien.Tests.Admin
{
    [TestClass]
    public class DanhMuc
    {
        [TestMethod]
        public void TestIndexDanhMuc()
        {
            var controller = new DanhMucController();
            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<ProductType>));

        }


        [TestMethod]
        public void TestThemDanhMuc1()
        {
            var controller = new DanhMucController();
            var result = controller.Them() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestThemDanhMuc2()
        {
            var controller = new DanhMucController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = new ProductType();

            model.ProductTypeCode = "MLT";
            model.ProductTypeName = "Máy Laptop";


            var result = controller.Them(model) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestGiaoDienSuaDanhMuc()
        {
            var controller = new DanhMucController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = new ProductType();
            var firstID = db.ProductTypes.First().ID;
            var result = controller.Sua(firstID) as RedirectToRouteResult;
            Assert.AreEqual("DTH", db.ProductTypes.First().ProductTypeCode);

        }


        [TestMethod]
        public void TestSuaDanhMuc()
        {
            var controller = new DanhMucController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = db.ProductTypes.AsNoTracking().First();


            model.ProductTypeCode = "DTH";

            var result0 = controller.Sua(model) as RedirectToRouteResult;
            Assert.IsNotNull(result0);
        }


        [TestMethod]
        public void XoaDanhMuc()
        {
            var controller = new DanhMucController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = db.ProductTypes.AsNoTracking().First();
            var firstID = db.ProductTypes.OrderByDescending(x => x.ID).First().ID;

            int count = db.ProductTypes.Count();
            using (var scope = new TransactionScope())
            {

                var result0 = controller.DeleteConfirmed(firstID) as RedirectToRouteResult;
                Assert.IsNotNull(result0);
                Assert.AreEqual("Index", result0.RouteValues["action"]);
                Assert.AreEqual(count - 1, db.ProductTypes.Count());

            }
        }
    }

}
