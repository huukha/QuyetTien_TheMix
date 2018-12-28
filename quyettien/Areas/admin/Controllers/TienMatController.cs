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
    public class TienMatController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: admin/TienMat
        public ActionResult Index()
        {
            return View(db.CashBills.ToList());
        }

        // GET: admin/TienMat/Xem/5
        public ActionResult Xem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // GET: admin/TienMat/Them
        public ActionResult Them()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            return View();
        }

        // POST: admin/TienMat/Them
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill, CashBillDetail cashBillDetail)
        {
            if (ModelState.IsValid)
            {
                cashBill.Date = DateTime.Now;
                db.CashBills.Add(cashBill);
                db.CashBillDetails.Add(cashBillDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", cashBillDetail.ProductID);
            return View(cashBill);
        }

        // GET: SalePrice
        public int SalePrice(int id)
        {
            var price = db.Products.Find(id).SalePrice;
            return price;
            //return Json(price, JsonRequestBehavior.AllowGet);
        }

        // GET: admin/TienMat/Sua/5
        public ActionResult Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: admin/TienMat/Sua/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashBill);
        }

        //// GET: admin/TienMat/Xoa/5
        //public ActionResult Xoa(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CashBill cashBill = db.CashBills.Find(id);
        //    if (cashBill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cashBill);
        //}

        //// POST: admin/TienMat/Xoa/5
        //[HttpPost, ActionName("Xoa")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CashBill cashBill = db.CashBills.Find(id);
        //    db.CashBills.Remove(cashBill);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // Print bill
        public ActionResult In(int id)
        {
            var bill = db.CashBills.FirstOrDefault(b => b.ID == id);

            if (bill != null)
            {
                CashBillModel cb = new CashBillModel();
                cb.BillCode = bill.BillCode;
                cb.CustomerName = bill.CustomerName;
                cb.PhoneNumber = bill.PhoneNumber;
                cb.Address = bill.Address;
                cb.Date = bill.Date;
                cb.Shipper = bill.Shipper;
                cb.Note = bill.Note;
                cb.GrandTotal = bill.GrandTotal;
                cb.CASHBILL_DETAIL = bill.CashBillDetails.ToList();

                return View(cb);
            }
            else
            {
                return View();
            }

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
