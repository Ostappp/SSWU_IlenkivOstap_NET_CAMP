﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ComputerStoreModelContainer : DbContext
    {
        public ComputerStoreModelContainer()
            : base("name=ComputerStoreModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Item> ItemSet { get; set; }
        public virtual DbSet<Category> CategorySet { get; set; }
        public virtual DbSet<Manufacturer> ManufacturerSet { get; set; }
        public virtual DbSet<ItemParams> ItemParamsSet { get; set; }
    }
}
