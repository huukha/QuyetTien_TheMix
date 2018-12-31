using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quyettien.Models;

namespace quyettien.Controllers
{
    public class San_PhamController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: San_Pham
        public ActionResult Index(int? id)
        {
            //var products = db.Products.Include(p => p.ProductType).Where(p => p.Status == true);

            //ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName");
            //return View(products.ToList());

            if (id == 0 || id == null)
            {
                var products = db.Products.Include(p => p.ProductType).Where(p => p.Status == true);

                ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName");
                return View(products.ToList());
            }
            else
            {
                var products = db.Products.Include(p => p.ProductType).Where(p => p.Status == true && p.ProductTypeID == id).ToList();

                ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName");
                return View(products);
            }
        }

        // Get Image
        public FileResult GetImage(string id)
        {
            var path = Server.MapPath("~/images");
            path = System.IO.Path.Combine(path, id);
            return File(path, "images");
        }

        // GET: San_Pham/Details/5
        public ActionResult Chi_Tiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: DanhMuc
        public ActionResult MenuDanhMuc()
        {
            var categories = db.ProductTypes.ToList();
            return PartialView(categories);
        }

        // GET: SanPhamMoi
        public ActionResult SanPhamMoi()
        {
            var newProduct = db.Products.Where(p => p.Status == true).OrderByDescending(p => p.ID).ToList().Take(5);
            return PartialView(newProduct);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
