using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quyettien.Areas.admin.Controllers
{
    public class DangXuatController : Controller
    {
        // GET: admin/DangXuat
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }
    }
}