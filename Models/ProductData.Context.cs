﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineDelivery.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineOrder : DbContext
    {
        public OnlineOrder()
            : base("name=OnlineOrder")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OrderMain> OrderMains { get; set; }
        public virtual DbSet<OrderSub> OrderSubs { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Productiteam> Productiteams { get; set; }
        public virtual DbSet<Shopkeeper> Shopkeepers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
