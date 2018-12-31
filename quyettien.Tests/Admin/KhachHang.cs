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
    public class KhachHang
    {
        [TestMethod]
        public void TestIndexKhachHang()
        {
            var controller = new KhachHangController();
            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Customer>));

        }
        [TestMethod]
        public void TestGiaoDienThemKhachHang()
        {
            var controller = new KhachHangController();
            var result = controller.Them() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestThemKhachHang()
        {
            var controller = new KhachHangController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = new Customer();

            model.CustomerCode = "222333444";
            model.CustomerName = "Họ Và Tên";
            model.Address = "Quận 13";
            model.PhoneNumber = "0987654321";
            model.YearOfBirth = 1998;


            var result = controller.Them(model) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestSuaKhachHang1()
        {
            var controller = new KhachHangController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = new Customer();
            var firstID = db.Customers.First().ID;
            var result = controller.Sua(firstID) as RedirectToRouteResult;
            Assert.AreEqual("0773322489", db.Customers.First().PhoneNumber);

        }
        [TestMethod]
        public void TestSuaKhachHang2()
        {
            var controller = new KhachHangController();
            var db = new DIENMAYQUYETTIENEntities();
            var model = db.Customers.AsNoTracking().First();


            model.PhoneNumber = "0773322489";

            var result0 = controller.Sua(model) as RedirectToRouteResult;
            Assert.IsNotNull(result0);
        }
    }
}
