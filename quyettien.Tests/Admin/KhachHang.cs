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
    }
}
