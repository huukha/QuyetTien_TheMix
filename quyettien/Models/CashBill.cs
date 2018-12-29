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

    public partial class CashBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashBill()
        {
            this.CashBillDetails = new HashSet<CashBillDetail>();
        }
    
        public int ID { get; set; }
        public string BillCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Họ tên từ 5 đến 50 kí tự")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Điện thoại là 10 kí tự số bắt đầu bằng số 0")]
        [RegularExpression(@"^[0][0-9]{9}$", ErrorMessage = "Điện thoại là 10 kí tự số bắt đầu bằng số 0")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Địa chỉ từ 5 đến 100 kí tự")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        public string Shipper { get; set; }
        public string Note { get; set; }
        
        public int GrandTotal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashBillDetail> CashBillDetails { get; set; }
    }
}
