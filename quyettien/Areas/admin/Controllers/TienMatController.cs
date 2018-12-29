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
        //[ValidateAntiForgeryToken]
        public JsonResult Them([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            if (ModelState.IsValid)
            {
                cashBill.Date = DateTime.Now;
                db.CashBills.Add(cashBill);
                db.SaveChanges();

                //return Json("Thêm thành công");


                int BillID = db.CashBills.Max(b => b.ID);
                return Json(new { billID = BillID}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var modelState = ModelState.ToDictionary
                (
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()

                );
                return Json(new { modelState = modelState }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ThemChiTiet(List<CashBillDetail> cashBillDetails)
        {
            int billID = cashBillDetails[0].BillID;
            db.CashBillDetails.RemoveRange(db.CashBillDetails.Where(cbd => cbd.BillID == billID));
            foreach (CashBillDetail cbd in cashBillDetails)
            {
                db.CashBillDetails.Add(cbd);
            }
            db.SaveChanges();
            return Json(true);
        }


        // GET: SalePrice
        public int SalePrice(int id)
        {
            var price = db.Products.Find(id).SalePrice;
            return price;
        }

        // GET: DanhSachSanPham
        public JsonResult DanhSachSanPham()
        {
            var ProductList = new SelectList(db.Products, "ID", "ProductName");
            return Json(new { productList = ProductList }, JsonRequestBehavior.AllowGet);
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
        //[ValidateAntiForgeryToken]
        public JsonResult Sua([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            
            if (ModelState.IsValid)
            {
                db.CashBills.Attach(cashBill);
                db.Entry(cashBill).Property(cb => cb.CustomerName).IsModified = true;
                db.Entry(cashBill).Property(cb => cb.PhoneNumber).IsModified = true;
                db.Entry(cashBill).Property(cb => cb.Address).IsModified = true;
                db.Entry(cashBill).Property(cb => cb.Shipper).IsModified = true;
                db.Entry(cashBill).Property(cb => cb.Note).IsModified = true;
                db.Entry(cashBill).Property(cb => cb.GrandTotal).IsModified = true;

                //db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return Json("Cập nhật thành công");

                int BillID = cashBill.ID;
                return Json(new { billID = BillID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var modelState = ModelState.ToDictionary
                (
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()

                );
                return Json(new { modelState = modelState }, JsonRequestBehavior.AllowGet);
            }
            //return View(cashBill);
        }

        public JsonResult ChiTietHoaDon(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.CashBillDetails.Where(cbd=> cbd.BillID == id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
