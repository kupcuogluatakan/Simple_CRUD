using BireyselCalisma2.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BireyselCalisma2.Models
{
    public class CustomKullanici
    {
        public bool RememberMe { get; set; }
        public Kullanıcı Kullanıcı { get; set; }
    }
}