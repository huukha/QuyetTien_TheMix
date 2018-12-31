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
    }

}
