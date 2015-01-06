﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Project_WatervalEntities : DbContext
    {
        public Project_WatervalEntities()
            : base("name=Project_WatervalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountLaw> AccountLaw { get; set; }
        public virtual DbSet<AccountRole> AccountRole { get; set; }
        public virtual DbSet<AssignmentCode> AssignmentCode { get; set; }
        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<Competence> Competence { get; set; }
        public virtual DbSet<LearnGoal> LearnGoal { get; set; }
        public virtual DbSet<LearningTool> LearningTool { get; set; }
        public virtual DbSet<LearnLine> LearnLine { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<ModelWithWorkform> ModelWithWorkform { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleStudyPhasingBlock> ModuleStudyPhasingBlock { get; set; }
        public virtual DbSet<Phasing> Phasing { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<Study> Study { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Theme> Theme { get; set; }
        public virtual DbSet<WeekSchedule> WeekSchedule { get; set; }
        public virtual DbSet<Workform> Workform { get; set; }
        public virtual DbSet<GradeType> GradeType { get; set; }
    }
}
