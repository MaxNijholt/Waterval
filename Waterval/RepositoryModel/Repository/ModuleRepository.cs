using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using RepositoryModel.IRepository;
using System.Data.Entity.Validation;

namespace RepositoryModel.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        Project_WatervalEntities dbContext;

        public ModuleRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }

        public List<DomainModel.Models.Module> GetAll()
        {
         
            return dbContext.Module.ToList();
        }

        public DomainModel.Models.Module Get(int module_id)
        {
            return dbContext.Module.Find(module_id);
        }

        public DomainModel.Models.Module Create(DomainModel.Models.Module module)
        {

            AddLinkingsModule(module);

            dbContext.Module.Add(module);
            dbContext.SaveChanges();
            return module;
        }

        public DomainModel.Models.Module Update(DomainModel.Models.Module update)
        {
            Module module = dbContext.Module.SingleOrDefault(b => b.Module_ID == update.Module_ID);
            if (module == null) return null;
            module.Title = update.Title;
            module.PrevModule_ID = update.PrevModule_ID;
            module.CourseCode = update.CourseCode;
            module.Entry_Level = update.Entry_Level;
            module.Definition_Short = update.Definition_Long;
            module.Definition_Long = update.Definition_Long;
            module.Foreknowledge = update.Foreknowledge;
            module.Account_ID = update.Account_ID;

            UpdateLinkingsModule(module);
            AddLinkingsModule(update);

            dbContext.SaveChanges();
            return module;
        }

        public void Delete(int module_id)
        {
            Module module = dbContext.Module.SingleOrDefault(b => b.Module_ID == module_id);

            module.isDeleted = true;
            module.DeleteDate = DateTime.UtcNow;

            dbContext.SaveChanges();

        }

        /// <summary>
        //Gets the new version based on an id of the old one.
        /// </summary>
        /// <param name="prevCompetence_ID">the Id of the old version</param>
        /// <returns></returns>
        public Module GetNewVersion(int prevModule_ID)
        {
            Module newModule = dbContext.Module.Where(c => c.PrevModule_ID == prevModule_ID).SingleOrDefault();
            return newModule;
        }

        public void LinkingsOfModule(Module module)
        {
            List<LearnLine> learnlines = new List<LearnLine>();

            if (learnlines == null)
            {
                for (int index = 0; index < module.LearnLine.Count; index++)
                {
                    learnlines.Add(dbContext.LearnLine.Find(module.LearnLine.ElementAt(index).LearnLine_ID));
                }

                module.LearnLine = learnlines;
            }
            else
            {
                for (int index = 0; index < module.LearnLine.Count; index++)
                {
                    module.LearnLine.Remove(dbContext.LearnLine.Find(module.LearnLine.ElementAt(index).LearnLine_ID));
                }
            }

            List<Theme> themes = new List<Theme>();

            if (themes == null)
            {
                for (int index = 0; index < module.Theme.Count; index++)
                {
                    themes.Add(dbContext.Theme.Find(module.Theme.ElementAt(index).Theme_ID));
                }

                module.Theme = themes;
            }
            else
            {
                for (int index = 0; index < module.Theme.Count; index++)
                {
                    module.Theme.Remove(dbContext.Theme.Find(module.Theme.ElementAt(index).Theme_ID));
                }
            }

            List<LearningTool> learningtools = new List<LearningTool>();

            if (learningtools == null)
            {

                for (int index = 0; index < module.LearningTool.Count; index++)
                {
                    learningtools.Add(dbContext.LearningTool.Find(module.LearningTool.ElementAt(index).LearnTool_ID));
                }

                module.LearningTool = learningtools;
            }
            else
            {
                for (int index = 0; index < module.LearningTool.Count; index++)
                {
                    module.LearningTool.Remove(dbContext.LearningTool.Find(module.LearningTool.ElementAt(index).LearnTool_ID));
                }
            }

            List<LearnGoal> learngoal = new List<LearnGoal>();

            if (learngoal == null)
            {

                for (int index = 0; index < module.LearnGoal.Count; index++)
                {
                    learngoal.Add(dbContext.LearnGoal.Find(module.LearnGoal.ElementAt(index).LearnGoal_ID));
                }

                module.LearnGoal = learngoal;
            }
            else
            {
                for (int index = 0; index < module.LearnGoal.Count; index++)
                {
                    module.LearnGoal.Remove(dbContext.LearnGoal.Find(module.LearnGoal.ElementAt(index).LearnGoal_ID));
                }
            }

        }

        public void AddLinkingsModule(Module module)   {

            List<LearnLine> learnlines = new List<LearnLine>();

            for (int index = 0; index < module.LearnLine.Count; index++)
            {
                learnlines.Add(dbContext.LearnLine.Find(module.LearnLine.ElementAt(index).LearnLine_ID));
            }

            module.LearnLine = learnlines;



            List<Theme> themes = new List<Theme>();

            for (int index = 0; index < module.Theme.Count; index++)
            {
                themes.Add(dbContext.Theme.Find(module.Theme.ElementAt(index).Theme_ID));
            }

            module.Theme = themes;


            List<LearningTool> learningtools = new List<LearningTool>();

            for (int index = 0; index < module.LearningTool.Count; index++)
            {
                learningtools.Add(dbContext.LearningTool.Find(module.LearningTool.ElementAt(index).LearnTool_ID));
            }

            module.LearningTool = learningtools;

            List<LearnGoal> learngoal = new List<LearnGoal>();

            for (int index = 0; index < module.LearnGoal.Count; index++)
            {
                learngoal.Add(dbContext.LearnGoal.Find(module.LearnGoal.ElementAt(index).LearnGoal_ID));
            }

            module.LearnGoal = learngoal;

        }


        public void UpdateLinkingsModule(Module module)
        {
            for (int index = 0; index < module.LearnLine.Count; index++)
            {
                module.LearnLine.Remove(dbContext.LearnLine.Find(module.LearnLine.ElementAt(index).LearnLine_ID));
            }

            for (int index = 0; index < module.Theme.Count; index++)
            {
                module.Theme.Remove(dbContext.Theme.Find(module.Theme.ElementAt(index).Theme_ID));
            }

            for (int index = 0; index < module.LearningTool.Count; index++)
            {
                module.LearningTool.Remove(dbContext.LearningTool.Find(module.LearningTool.ElementAt(index).LearnTool_ID));
            }

            for (int index = 0; index < module.LearnGoal.Count; index++)
            {
                module.LearnGoal.Remove(dbContext.LearnGoal.Find(module.LearnGoal.ElementAt(index).LearnGoal_ID));
            }

        }


        public void StudyBlockPhasingAndModules(int module_id, int study_id, int block_id, int phasing_id)
        {
            if (dbContext.ModuleStudyPhasingBlock.Any(l => l.Module_ID == module_id && l.Study_ID == study_id && l.Block_ID == block_id && l.Phasing_ID == phasing_id))
                return;

            ModuleStudyPhasingBlock model = new ModuleStudyPhasingBlock();
            model.Phasing_ID = phasing_id;
            model.Module_ID = module_id;
            model.Study_ID = study_id;
            model.Block_ID = block_id;
            dbContext.ModuleStudyPhasingBlock.Add(model);
            dbContext.SaveChanges();
        }
        public void StudyBlockPhasingAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.ModuleStudyPhasingBlock.Where(x => x.Module_ID == module_id);
            dbContext.ModuleStudyPhasingBlock.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void CompentenceAndModules(int module_id, int competence_id, string level)
        {

            if (dbContext.Level.Any(l => l.Module_ID == module_id && l.Competence_ID == competence_id))
                return;

            Level model = new Level();
            model.Competence_ID = competence_id;
            model.Module_ID = module_id;
            model.Level1 = level;
            dbContext.Level.Add(model);
            dbContext.SaveChanges();
        }

        public void CompentenceAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.Level.Where(x => x.Module_ID == module_id);
            dbContext.Level.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void WorkformAndModulesVersion2(int module_id, int workform_id, int duration, string frequency, int workload)
        {

            if (dbContext.ModelWithWorkform.Any(l => l.Module_ID == module_id && l.Workform_ID == workform_id))
                return;

            ModelWithWorkform model = new ModelWithWorkform();
            model.Workform_ID = workform_id;
            model.Module_ID = module_id;
            model.Duration = duration;
            model.Frequency = frequency;
            model.Workload = workload;
            dbContext.ModelWithWorkform.Add(model);
            dbContext.SaveChanges();
        }

        public void WorkformAndModulesDeleteVersion2(int module_id)
        {
            var itemsToDelete = dbContext.ModelWithWorkform.Where(x => x.Module_ID == module_id);
            dbContext.ModelWithWorkform.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void WorkformAndModules(int module_id, int workform_id, int duration, string frequency, int workload)
        {

            if (dbContext.ModelWithWorkform.Any(l => l.Module_ID == module_id && l.Workform_ID == workform_id))
                return;

            ModelWithWorkform model = new ModelWithWorkform();
            model.Workform_ID = workform_id;
            model.Module_ID = module_id;
            model.Duration = duration;
            model.Frequency = frequency;
            model.Workload = workload;
            dbContext.ModelWithWorkform.Add(model);
            dbContext.SaveChanges();
        }

        public void WorkformAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.ModelWithWorkform.Where(x => x.Module_ID == module_id);
            dbContext.ModelWithWorkform.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void GradetypesAndModules(int module_id, string description)
        {

            if (dbContext.GradeType.Any(l => l.Module_ID == module_id))
                return;

            GradeType model = new GradeType();
            model.Module_ID = module_id;
            model.GradeDescription = description;

            dbContext.GradeType.Add(model);
            dbContext.SaveChanges();
        }

        public void GradetypesAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.GradeType.Where(x => x.Module_ID == module_id);
            dbContext.GradeType.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void WeekSchedulesAndModules(int module_id, string description, int weeknr)
        {

            if (dbContext.WeekSchedule.Any(l => l.Module_ID == module_id))
                return;

            WeekSchedule model = new WeekSchedule();
            model.Module_ID = module_id;
            model.Description = description;
            model.WeekNr = weeknr;

            dbContext.WeekSchedule.Add(model);
            dbContext.SaveChanges();
        }

        public void WeekSchedulesAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.WeekSchedule.Where(x => x.Module_ID == module_id);
            dbContext.WeekSchedule.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }

        public void AssignmentcodeAndModules(int module_id, string description, int ec)
        {

            if (dbContext.AssignmentCode.Any(l => l.Module_ID == module_id))
                return;

            AssignmentCode model = new AssignmentCode();
            model.Module_ID = module_id;
            model.Description = description;
            model.EC = ec;

            dbContext.AssignmentCode.Add(model);
            dbContext.SaveChanges();
        }

        public void AssignmentcodeAndModulesDelete(int module_id)
        {
            var itemsToDelete = dbContext.AssignmentCode.Where(x => x.Module_ID == module_id);
            dbContext.AssignmentCode.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }




        public List<Module> GetWithCompetence(int id)
        {
            return dbContext.Level.Where(c => c.Competence_ID == id).Select(a => a.Module).ToList();
        }
    }
}
