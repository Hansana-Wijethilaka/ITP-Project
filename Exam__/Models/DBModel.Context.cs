﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam__.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SipminiEntities2 : DbContext
    {
        public SipminiEntities2()
            : base("name=SipminiEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Exam2> Exam2 { get; set; }
        public virtual DbSet<grade> grades { get; set; }
        public virtual DbSet<subject> subjects { get; set; }
        public virtual DbSet<teacher> teachers { get; set; }
        public virtual DbSet<teacher_grade> teacher_grade { get; set; }
        public virtual DbSet<teacher_subject> teacher_subject { get; set; }
        public virtual DbSet<upload_file> upload_file { get; set; }
        public virtual DbSet<upload_file_teacher> upload_file_teacher { get; set; }
    }
}