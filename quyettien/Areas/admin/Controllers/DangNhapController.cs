using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyettien.Models;

namespace quyettien.Areas.admin.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: admin/DangNhap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Account ad)
        {
            if (ModelState.IsValid)
            {
                using (DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities())
                {
                    var username = db.Accounts.Where(a => a.username.Equals(ad.username)).FirstOrDefault();
                    if (username != null)
                    {

                        var admin = db.Accounts.Where(a => a.username.Equals(ad.username) && a.password.Equals(ad.password)).FirstOrDefault();
                        if (admin != null)
                        {
                            Session["admin_session"] = admin.username.ToString();
                            Session["fullname"] = admin.fullname.ToString();

                            return RedirectToAction("Index", "Index");
                        }
                        else
                        {
                            ad.errorMessage = "Mật khẩu không chính xác";
                        }
                    }
                    else
                    {
                        ad.errorMessage = "Tài khoản không tồn tại";
                    }

                    return View("Index", ad);
                }
            }
            return View();            
        }
    }
}