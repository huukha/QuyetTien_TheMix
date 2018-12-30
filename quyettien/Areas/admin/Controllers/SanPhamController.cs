using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quyettien.Models;
using System.Transactions;

namespace quyettien.Areas.admin.Controllers
{
    public class SanPhamController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: admin/SanPham
        public ActionResult Index()
        {
            //var products = db.Products.Include(p => p.ProductType);
            var products = db.Products.OrderByDescending(p => p.ID);
            return View(products.ToList());
        }

        // Get Image
        public FileResult GetImage(string id)
        {
            var path = Server.MapPath("~/images");
            path = System.IO.Path.Combine(path, id);
            return File(path, "images");
        }

        // GET: admin/SanPham/Xem/5
        public ActionResult Xem(int? id)
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

        // GET: admin/SanPham/Them
        public ActionResult Them()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName");
            return View();
        }

        // POST: admin/SanPham/Them
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them([Bind(Include = "ID,ProductCode,ProductName,ProductTypeID,SalePrice,OriginPrice,InstallmentPrice,Quantity,Avatar,Status,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                    var path = Server.MapPath("~/images");
                    path = path + "/" + product.ID;

                    if(Request.Files["avatar"] != null && Request.Files["avatar"].ContentLength > 0)
                    {
                        Request.Files["avatar"].SaveAs(path);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("avatar", "Vui lòng chọn hình ảnh sản phẩm");
                    }
                }
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // GET: admin/SanPham/Sua/5
        public ActionResult Sua(int? id)
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
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // POST: admin/SanPham/Sua/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua([Bind(Include = "ID,ProductCode,ProductName,ProductTypeID,SalePrice,OriginPrice,InstallmentPrice,Quantity,Avatar,Status,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    var sqlGetTypeCode = "select ProductTypeCode from ProductType where ID = " + product.ProductTypeID;
                    var sqlGetCurrentCode = "select ProductCode from Product where ID = " + product.ID;

                    var newTypeCode = db.Database.SqlQuery<string>(sqlGetTypeCode).First();
                    var currentCode = db.Database.SqlQuery<string>(sqlGetCurrentCode).First();

                    var newCode = currentCode.Substring(currentCode.Length - 6, 6);

                    product.ProductCode = newTypeCode + newCode;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    var path = Server.MapPath("~/images");
                    path = path + "/" + product.ID;

                    if (Request.Files["avatar"] != null && Request.Files["avatar"].ContentLength > 0)
                    {
                        Request.Files["avatar"].SaveAs(path);
                    }

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // GET: admin/SanPham/Xoa/5
        public ActionResult Xoa(int? id)
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

        // POST: admin/SanPham/Xoa/5
        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
