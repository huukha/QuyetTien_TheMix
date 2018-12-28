using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quyettien.Models
{
    public class CashBillModel
    {
        public int ID { get; set; }
        public string BillCode { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public System.DateTime Date { get; set; }
        public string Shipper { get; set; }
        public string Note { get; set; }
        public int GrandTotal { get; set; }

        public List<CashBillDetail> CASHBILL_DETAIL { get; set; }
    }
}