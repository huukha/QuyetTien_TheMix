﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quyettien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class InstallmentBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstallmentBill()
        {
            this.InstallmentBillDetails = new HashSet<InstallmentBillDetail>();
        }

        public int ID { get; set; }
        public string BillCode { get; set; }
        public int CustomerID { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        public string Shipper { get; set; }
        public string Note { get; set; }
        public string Method { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời hạn")]
        [Range(1, 365, ErrorMessage = "Thời hạn trong khoảng từ 1 đến 365")]
        public int Period { get; set; }

        public int GrandTotal { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số tiền đưa trước")]
        [Range(1, 2000000000, ErrorMessage = "Tiền đưa trước trong khoảng từ 1 đến 2.000.000.000")]
        public int Taken { get; set; }

        public int Remain { get; set; }

        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstallmentBillDetail> InstallmentBillDetails { get; set; }
    }
}
