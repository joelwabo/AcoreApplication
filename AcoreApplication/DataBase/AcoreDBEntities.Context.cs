﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AcoreApplication.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AcoreDBEntities : DbContext
    {
        public AcoreDBEntities()
            : base("name=AcoreDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Automate> Automate { get; set; }
        public virtual DbSet<Historique> Historique { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<Recette> Recette { get; set; }
        public virtual DbSet<Redresseur> Redresseur { get; set; }
        public virtual DbSet<Registre> Registre { get; set; }
        public virtual DbSet<Segment> Segment { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
