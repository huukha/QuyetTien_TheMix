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
    public class SanPham
    {
        [TestMethod]
        public void Index()
        {
            var controller = new SanPhamController();
            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Products.Count(), ((List<Product>)result.Model).Count);
        }


        [TestMethod]
        public void TestThemSanPham1()
        {
            var controller = new SanPhamController();
            var result = controller.Them() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["ProductTypeID"], typeof(SelectList));
        }

        [TestMethod]
        public void TestSuaSanPham()
        {
            var controller = new SanPhamController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = new Product();
            var firstID = db.Products.First().ID;
            var result = controller.Sua(firstID) as ViewResult;
            Assert.IsInstanceOfType(result.ViewData["ProductTypeID"], typeof(SelectList));
            Assert.AreEqual("Điện thoại iPhone X", db.Products.First().ProductName);
        }

        [TestMethod]
        public void TestXoaSanPham()
        {
            var controller = new SanPhamController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = db.Products.AsNoTracking().First();
            var firstID = db.Products.OrderByDescending(x => x.ID).First().ID;


            int count = db.Products.Count();
            using (var scope = new TransactionScope())
            {

                var result0 = controller.DeleteConfirmed(firstID) as RedirectToRouteResult;
                Assert.IsNotNull(result0);
                Assert.AreEqual("Index", result0.RouteValues["action"]);
                Assert.AreEqual(count - 1, db.Products.Count());

            }
        }
    }
}
