//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BireyselCalisma2.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Makale
    {
        public int MakaleId { get; set; }
        public string MTitle { get; set; }
        public string MDetail { get; set; }
        public Nullable<int> CategoryID { get; set; }
    
        public virtual Kategori Kategori { get; set; }
    }
}