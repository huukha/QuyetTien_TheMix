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

    public partial class Account
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        public string fullname { get; set; }

        public string errorMessage { get; set; }
    }
}

