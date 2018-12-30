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

    public partial class ProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã danh mục")]
        [RegularExpression(@"^[a-zA-Z]{1,40}$", ErrorMessage = "Chỉ được sử dụng kí tự chữ không dấu và không có khoảng trắng")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Tên danh mục là chuỗi có 3 kí tự")]
        public string ProductTypeCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên danh mục từ 5 đến 100 kí tự")]
        public string ProductTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
