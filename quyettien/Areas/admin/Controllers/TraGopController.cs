﻿using System;
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
    public class TraGopController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: admin/TraGop
        public ActionResult Index()
        {
            var installmentBills = db.InstallmentBills.Include(i => i.Customer);
            return View(installmentBills.ToList());
        }

        // GET: admin/TraGop/Xem/5
        public ActionResult Xem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            return View(installmentBill);
        }

        // GET: admin/TraGop/Them
        public ActionResult Them()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName");
            return View();
        }

        // POST: admin/TraGop/Them
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            if (ModelState.IsValid)
            {
                db.InstallmentBills.Add(installmentBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // GET: admin/TraGop/Sua/5
        public ActionResult Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // POST: admin/TraGop/Sua/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", installmentBill.CustomerID);
            return View(installmentBill);
        }

        //// GET: admin/TraGop/Xoa/5
        //public ActionResult Xoa(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InstallmentBill installmentBill = db.InstallmentBills.Find(id);
        //    if (installmentBill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(installmentBill);
        //}

        //// POST: admin/TraGop/Xoa/5
        //[HttpPost, ActionName("Xoa")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    InstallmentBill installmentBill = db.InstallmentBills.Find(id);
        //    db.InstallmentBills.Remove(installmentBill);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // Print Bill
        public ActionResult In(int id)
        {
            var bill = db.InstallmentBills.FirstOrDefault(b => b.ID == id);

            if (bill != null)
            {
                InstallmentBillModel ib = new InstallmentBillModel();
                ib.BillCode = bill.BillCode;
                ib.CustomerID = bill.CustomerID;
                ib.CustomerName = bill.Customer.CustomerName;
                ib.Address = bill.Customer.Address;
                ib.Phone = bill.Customer.PhoneNumber;
                ib.Date = bill.Date;
                ib.GrandTotal = bill.GrandTotal;
                ib.Taken = bill.Taken;
                ib.Remain = bill.Remain;
                ib.Method = bill.Method;
                ib.Period = bill.Period;
                ib.Note = bill.Note;
                ib.Shipper = bill.Shipper;
                ib.INSTALLMENTBILL_DETAIL = bill.InstallmentBillDetails.ToList();

                return View(ib);
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
