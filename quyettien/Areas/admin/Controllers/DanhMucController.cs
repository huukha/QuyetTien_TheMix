using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quyettien.Models;

namespace quyettien.Areas.admin.Controllers
{
    public class DanhMucController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: admin/DanhMuc
        public ActionResult Index()
        {
            return View(db.ProductTypes.ToList());
        }

        // GET: admin/DanhMuc/Xem/5
        public ActionResult Xem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // GET: admin/DanhMuc/Them
        public ActionResult Them()
        {
            return View();
        }

        // POST: admin/DanhMuc/Them
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them([Bind(Include = "ID,ProductTypeCode,ProductTypeName")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                productType.ProductTypeCode = productType.ProductTypeCode.ToUpper();

                db.ProductTypes.Add(productType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productType);
        }

        // GET: admin/DanhMuc/Sua/5
        public ActionResult Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // POST: admin/DanhMuc/Sua/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua([Bind(Include = "ID,ProductTypeCode,ProductTypeName")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                productType.ProductTypeCode = productType.ProductTypeCode.ToUpper();

                db.Entry(productType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: admin/DanhMuc/Xoa/5
        public ActionResult Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // POST: admin/DanhMuc/Xoa/5
        [HttpPost, ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType productType = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(productType);
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
