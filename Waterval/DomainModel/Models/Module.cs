//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Module
    {
        public Module()
        {
            this.AssignmentCode = new HashSet<AssignmentCode>();
            this.GradeType = new HashSet<GradeType>();
            this.Level = new HashSet<Level>();
            this.ModelWithWorkform = new HashSet<ModelWithWorkform>();
            this.ModuleStudyPhasingBlock = new HashSet<ModuleStudyPhasingBlock>();
            this.WeekSchedule = new HashSet<WeekSchedule>();
            this.Study = new HashSet<Study>();
            this.LearnGoal = new HashSet<LearnGoal>();
            this.LearningTool = new HashSet<LearningTool>();
            this.LearnLine = new HashSet<LearnLine>();
            this.Theme = new HashSet<Theme>();
        }
    
        public int Module_ID { get; set; }
        public Nullable<int> PrevModule_ID { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }
        public string Entry_Level { get; set; }
        public string Definition_Short { get; set; }
        public string Definition_Long { get; set; }
        public string Foreknowledge { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<int> Account_ID { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual ICollection<AssignmentCode> AssignmentCode { get; set; }
        public virtual ICollection<GradeType> GradeType { get; set; }
        public virtual ICollection<Level> Level { get; set; }
        public virtual ICollection<ModelWithWorkform> ModelWithWorkform { get; set; }
        public virtual ICollection<ModuleStudyPhasingBlock> ModuleStudyPhasingBlock { get; set; }
        public virtual ICollection<WeekSchedule> WeekSchedule { get; set; }
        public virtual ICollection<Study> Study { get; set; }
        public virtual ICollection<LearnGoal> LearnGoal { get; set; }
        public virtual ICollection<LearningTool> LearningTool { get; set; }
        public virtual ICollection<LearnLine> LearnLine { get; set; }
        public virtual ICollection<Theme> Theme { get; set; }
    }
}
