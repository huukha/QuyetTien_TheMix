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
    }
}
