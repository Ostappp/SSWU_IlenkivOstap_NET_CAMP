//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task2.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SerialNum { get; set; }
        public System.DateTime DateOfManufaturer { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ItemParams ItemParams { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
