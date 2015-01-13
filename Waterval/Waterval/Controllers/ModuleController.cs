using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace MvcApplication1.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
        private ModuleRepository moduleRepository;
        private SearchRepository search;

        private LearnLineRepository learnLineRepository;
        private ThemeRepository themeRepository;
        private CompetenceRepository competenceRepository;
        private LearningToolRepository learningtoolRepository;
        //private LearnGoalRepository learngoalRepository;

        //private ProgramRepository programRepository;
        //private StudyRepository studyRepository;



        private WorkformRepository workformRepository;


        public ModuleController()
        {
            moduleRepository = new ModuleRepository();
            search = new SearchRepository();
            learnLineRepository = new LearnLineRepository();
            themeRepository = new ThemeRepository();
            competenceRepository = new CompetenceRepository();
            workformRepository = new WorkformRepository();
            learningtoolRepository = new LearningToolRepository();
            //learngoalRepository = new LearnGoalRepository();
            //programRepository = new ProgramRepository();
            //studyRepository = new StudyRepository();

        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int pagesize = 10, int id=0)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ResultAmount = pagesize;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var modules = (id ==0) ? moduleRepository.GetAll(): moduleRepository.GetWithCompetence(id) ;
            if (!String.IsNullOrEmpty(searchString))
            {
                modules = search.GetModulesWith(searchString);
            }
            switch (sortOrder)
            {
                case "Title":
                    modules = modules.OrderBy(b => b.Title).ToList();
                    break;
                case "Vakcode":
                    modules = modules.OrderBy(b => b.CourseCode).ToList();
                    break;
                case "EC":
                    modules = modules.OrderBy(b => b.AssignmentCode.Sum(s => s.EC)).ToList();
                    break;
                case "Ingangsniveau":
                    modules = modules.OrderBy(b => b.Entry_Level).ToList();
                    break;
                default:
                    modules = modules.OrderByDescending(b => b.Title).ToList();
                    break;
            }
            int pageSize = pagesize;
            int pageNumber = (page ?? 1);
            return View(modules.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Create()
        {
            Module module = new Module();

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);

            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);

            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);


            return View(module);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult Create(Module module)
        {
            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);

            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);
            try
            {

                @ViewBag.NewID = newVersion(module.Module_ID);

                //We create a new module and we store the id of it in a variabele
                var id = moduleRepository.Create(module).Module_ID;

                //We save the combination

                //WERKENDE
                foreach (var item in module.Level)
                    moduleRepository.CompentenceAndModules(id, item.Competence_ID, item.Level1);

                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.WorkformAndModules(id, item.Workform_ID, item.Duration, item.Frequency, item.Workload);

                foreach (var item in module.GradeType)
                    moduleRepository.GradetypesAndModules(id, item.GradeDescription);

                foreach (var item in module.WeekSchedule)
                    moduleRepository.WeekSchedulesAndModules(id, item.Description, item.WeekNr);

                foreach (var item in module.AssignmentCode)
                    moduleRepository.AssignmentcodeAndModules(id, item.Description, item.EC);

                //We go back to the index.
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }


                foreach (var item in module.ModelWithWorkform)
                    item.Workform = workformRepository.Get(item.Workform_ID);
                foreach (var item in module.GradeType)
                    item.Module = moduleRepository.Get(item.Module_ID);
                foreach (var item in module.WeekSchedule)
                    item.Module = moduleRepository.Get(item.Module_ID);
                foreach (var item in module.AssignmentCode)
                    item.Module = moduleRepository.Get(item.Module_ID);
                foreach (var item in module.Level)
                    item.Competence = competenceRepository.Get(item.Competence_ID);

                //Did something go wrong we return the view with the model.
                return View(module);
            }

        }


        public ActionResult Details(int id)
        {
            Module module = moduleRepository.Get(id);

            @ViewBag.NewID = newVersion(id);

            return View(module);
        }

        public ActionResult Delete(Module module)
        {

            moduleRepository.Delete(module.Module_ID);
            return View();
        }

        public ActionResult Edit(int id)
        {

            Module module = moduleRepository.Get(id);

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);

            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);

            @ViewBag.NewID = newVersion(id);

            return View(module);
        }

        [HttpPost]
        public ActionResult Edit(int id, Module module)
        {


            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);

            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);

            foreach (var item in module.ModelWithWorkform)
                item.Workform = workformRepository.Get(item.Workform_ID);

            try
            {
                //Get a list of modules based on this learnline, workform, gradetype. 
                @ViewBag.LearnLineList = GetLearnLines(module).Where(m => m.isDeleted = false);
                @ViewBag.ThemeList = GetThemes(module).Where(m => m.isDeleted = false);
                @ViewBag.LearningToolList = GetLearningTools(module).Where(m => m.isDeleted = false);

                //  @ViewBag.LearnGoalList = GetLearnGoals(module).Where(m => m.isDeleted = false);

                @ViewBag.CompetenceList = GetCompetence(module).Where(m => m.isDeleted = false);
                //@ViewBag.ProgramList = GetProgram(module).Where(m => m.isDeleted = false);
                //@ViewBag.StudyList = GetStudy(module).Where(m => m.isDeleted = false);
                @ViewBag.WorkformList = GetWorkForms(module).Where(m => m.isDeleted = false);
                @ViewBag.GradeTypes = GetGradeTypes(module).Where(m => m.isDeleted = false);
                @ViewBag.WeekSchedule = GetWeekschedule(module).Where(m => m.isDeleted = false);
                @ViewBag.AssignmentCode = GetAssignmentcode(module).Where(m => m.isDeleted = false);


                //if we update the model and somethign went wrong we send an error messge back
                if (moduleRepository.Update(module) == null)
                    return View(module).ViewBag.Error = "Er is iets fout gegaan.";


                //We delete all of the levels from this competence. 
                moduleRepository.CompentenceAndModulesDelete(module.Module_ID);
                moduleRepository.WorkformAndModulesDelete(module.Module_ID);
                moduleRepository.AssignmentcodeAndModulesDelete(module.Module_ID);
                moduleRepository.WeekSchedulesAndModulesDelete(module.Module_ID);
                moduleRepository.GradetypesAndModulesDelete(module.Module_ID);

                foreach (var item in module.Level)
                    moduleRepository.CompentenceAndModules(id, item.Competence_ID, item.Level1);

                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.WorkformAndModules(id, item.Workform_ID, item.Duration, item.Frequency, item.Workload);

                foreach (var item in module.WeekSchedule)
                    moduleRepository.WeekSchedulesAndModules(id, item.Description, item.WeekNr);

                foreach (var item in module.GradeType)
                    moduleRepository.GradetypesAndModules(id, item.GradeDescription);

                foreach (var item in module.AssignmentCode)
                    moduleRepository.AssignmentcodeAndModules(id, item.Description, item.EC);


                return RedirectToAction("Index");
            }

            catch
            {
                return View(module);
            }

        }




        //TODO moet aangepast worden met koppelingen
        public ActionResult toNewVersion(int id)
        {
            Module module = moduleRepository.Get(id);

            var model = new Module();
            model.PrevModule_ID = module.PrevModule_ID;
            model.Title = module.Title;
            model.CourseCode = module.CourseCode;
            model.Entry_Level = module.Entry_Level;
            model.Definition_Short = module.Definition_Long;
            model.Definition_Long = module.Definition_Long;
            model.Foreknowledge = module.Foreknowledge;
            model.Account_ID = module.Account_ID;
            //+ koppelingen nog
            model.Theme = module.Theme;
            model.WeekSchedule = module.WeekSchedule;
            model.Study = module.Study;
            model.ModelWithWorkform = module.ModelWithWorkform;
            model.Level = module.Level;
            model.LearnLine = module.LearnLine;
            model.LearningTool = module.LearningTool;
            model.LearnGoal = module.LearnGoal;
            model.GradeType = module.GradeType;
            model.AssignmentCode = module.AssignmentCode;

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);
            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);


            @ViewBag.NewID = newVersion(id);

            return View(model);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult toNewVersion(int id, Module module)
        {
            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            //@ViewBag.LearnGoalList = GetLearnGoals(module);

            @ViewBag.CompetenceList = GetCompetence(module);
            //@ViewBag.ProgramList = GetProgram(module);
            //@ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);

            try
            {
                @ViewBag.NewID = newVersion(id);

                int newestID = moduleRepository.Create(module).Module_ID;

                foreach (var item in module.Level)
                    moduleRepository.CompentenceAndModules(newestID, item.Competence_ID, item.Level1);

                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.WorkformAndModules(newestID, item.Workform_ID, item.Duration, item.Frequency, item.Workload);

                foreach (var item in module.GradeType)
                    moduleRepository.GradetypesAndModules(newestID, item.GradeDescription);

                foreach (var item in module.WeekSchedule)
                    moduleRepository.WeekSchedulesAndModules(newestID, item.Description, item.WeekNr);

                foreach (var item in module.AssignmentCode)
                    moduleRepository.AssignmentcodeAndModules(newestID, item.Description, item.EC);

                module.Module_ID = newestID;

                moduleRepository.AddLinkingsModule(module);


                moduleRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {

                foreach (var item in module.ModelWithWorkform)
                    item.Workform = workformRepository.Get(item.Workform_ID);
                foreach (var item in module.GradeType)
                    item.Module = moduleRepository.Get(item.Module_ID);
                foreach (var item in module.Level)
                    item.Competence = competenceRepository.Get(item.Competence_ID);


                return View(module);
            }
        }


        private int newVersion(int id)
        {
            Module newer = moduleRepository.GetNewVersion(id);
            return (newer != null) ? newer.Module_ID : -1;
        }

        private List<LearnLine> GetLearnLines(Module module)
        {
            List<LearnLine> learnLines = learnLineRepository.GetAll();

            if (module != null)
                foreach (LearnLine lnl in module.LearnLine)
                    learnLines.Remove(learnLines.Where(b => b.LearnLine_ID == lnl.LearnLine_ID).First());

            return learnLines.Where(m => m.isDeleted == false).ToList();
        }

        private List<Theme> GetThemes(Module module)
        {
            List<Theme> themes = themeRepository.GetAll();

            if (module != null)
                foreach (Theme t in module.Theme)
                    themes.Remove(themes.Where(b => b.Theme_ID == t.Theme_ID).First());

            return themes.Where(m => m.isDeleted == false).ToList();
        }

        private List<Workform> GetWorkForms(Module module)
        {
            List<Workform> workforms = workformRepository.GetAll();

            if (module != null)
                foreach (ModelWithWorkform mwf in module.ModelWithWorkform)
                    workforms.Remove(workforms.Where(a => a.Workform_ID == mwf.Workform_ID).First());

            return workforms.Where(m => m.isDeleted == false).ToList();
        }

        private List<Competence> GetCompetence(Module module)
        {

            List<Competence> competences = competenceRepository.GetAll();

            if (module != null)
                foreach (Level lvl in module.Level)
                    competences.Remove(competences.Where(b => b.Competence_ID == lvl.Competence_ID).FirstOrDefault());

            return competences.Where(m => m.isDeleted == false).ToList();
        }

        //private List<LearnGoal> GetLearnGoals(Module module)
        //{
        //    List<LearnGoal> learngoals = learngoalRepository.GetAll();

        //    if (module != null)
        //        foreach (LearnGoal t in module.LearnGoal)
        //            learngoals.Remove(learngoals.Where(b => b.LearnGoal_ID == t.LearnGoal_ID).First());

        //    return learngoals.Where(m => m.isDeleted == false).ToList();
        //}

        private List<LearningTool> GetLearningTools(Module module)
        {
            List<LearningTool> learningtools = learningtoolRepository.GetAll();

            if (module != null)
                foreach (LearningTool t in module.LearningTool)
                    learningtools.Remove(learningtools.Where(b => b.LearnTool_ID == t.LearnTool_ID).First());

            return learningtools.Where(m => m.isDeleted == false).ToList();
        }

        //private List<Program> GetProgram(Module module)
        //{
        //    List<Program> programs = programRepository.GetAll();

        //    if (module != null)
        //        foreach (Program lnl in module.Program)
        //            programs.Remove(programs.Where(b => b.Program_ID == lnl.Program_ID).First());

        //    return programs.Where(m => m.isDeleted == false).ToList();
        //}

        //private List<Study> GetStudy(Module module)
        //{
        //    List<Study> studies = studyRepository.GetAll();

        //    if (module != null)
        //        foreach (Study lnl in module.Study)
        //            studies.Remove(studies.Where(b => b.Study_ID == lnl.Study_ID).First());

        //    return studies.Where(m => m.isDeleted == false).ToList();
        //}

        private List<Module> GetGradeTypes(Module module)
        {
            List<Module> gradetypes = moduleRepository.GetAll();

            if (module != null)
                foreach (GradeType mwf in module.GradeType)
                    gradetypes.Remove(gradetypes.Where(b => b.Module_ID == mwf.Module_ID).FirstOrDefault());

            return gradetypes.Where(m => m.isDeleted == false).ToList();
        }

        private List<Module> GetWeekschedule(Module module)
        {
            List<Module> weekschedules = moduleRepository.GetAll();

            if (module != null)
                foreach (WeekSchedule mwf in module.WeekSchedule)
                    weekschedules.Remove(weekschedules.Where(b => b.Module_ID == mwf.Module_ID).FirstOrDefault());

            return weekschedules.Where(m => m.isDeleted == false).ToList();
        }

        private List<Module> GetAssignmentcode(Module module)
        {
            List<Module> assignmentcodes = moduleRepository.GetAll();

            if (module != null)
                foreach (AssignmentCode mwf in module.AssignmentCode)
                    assignmentcodes.Remove(assignmentcodes.Where(b => b.Module_ID == mwf.Module_ID).FirstOrDefault());

            return assignmentcodes.Where(m => m.isDeleted == false).ToList();
        }

    }
}
