﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoDinar
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SFabricaEntities : DbContext
    {
        public SFabricaEntities()
            : base("name=SFabricaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Cvet> Cvet { get; set; }
        public virtual DbSet<Furnitura> Furnitura { get; set; }
        public virtual DbSet<FurnituraIzdeliya> FurnituraIzdeliya { get; set; }
        public virtual DbSet<Izdeliya> Izdeliya { get; set; }
        public virtual DbSet<Risunok> Risunok { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SkladFurnituri> SkladFurnituri { get; set; }
        public virtual DbSet<SkladTkani> SkladTkani { get; set; }
        public virtual DbSet<Sostav> Sostav { get; set; }
        public virtual DbSet<Tip> Tip { get; set; }
        public virtual DbSet<Tkani> Tkani { get; set; }
        public virtual DbSet<TkaniIzdeliya> TkaniIzdeliya { get; set; }
        public virtual DbSet<Zakaz> Zakaz { get; set; }
        public virtual DbSet<ZakazniyeIzdeliya> ZakazniyeIzdeliya { get; set; }
    }
}
