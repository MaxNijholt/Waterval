﻿using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Controllers {
	public class ModuleController : Controller {
		//
		// GET: /Module/
		private ModuleRepository moduleRepository;
		private SearchRepository search;

		private LearnLineRepository learnLineRepository;
		private ThemeRepository themeRepository;
		private CompetenceRepository competenceRepository;
		private LearningToolRepository learningtoolRepository;
		private LearnGoalRepository learngoalRepository;
		private StudyRepository studyRepository;
		private BlockRepository blockRepository;
		private WorkformRepository workformRepository;
		private PhasingRepository phasingRepository;
        private AccountRepository accountRepository;

		public ModuleController ( ) {
			moduleRepository = new ModuleRepository( );
			search = new SearchRepository( );
			learnLineRepository = new LearnLineRepository( );
			themeRepository = new ThemeRepository( );
			competenceRepository = new CompetenceRepository( );
			workformRepository = new WorkformRepository( );
			learningtoolRepository = new LearningToolRepository( );
			learngoalRepository = new LearnGoalRepository( );
			studyRepository = new StudyRepository( );
			blockRepository = new BlockRepository( );
			phasingRepository = new PhasingRepository( );
            accountRepository = new AccountRepository( );
		}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int pagesize = 10, int id = 0, bool isPDF = false)
        {

			ViewBag.CurrentSort = sortOrder;
			ViewBag.ResultAmount = pagesize;
			ViewBag.NameSortParm = String.IsNullOrEmpty( sortOrder ) ? "Title" : "";

			if ( searchString != null ) {
				page = 1;
			} else {
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var modules = ( id == 0 ) ? moduleRepository.GetAll( ).Where( m => m.isDeleted == false ) : moduleRepository.GetWithCompetence( id );

            if (isPDF)
                pagesize = modules.Count();

            if ( !String.IsNullOrEmpty( searchString ) ) {
				modules = search.GetModulesWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				modules = modules.OrderBy( b => b.Title ).ToList( );
				break;
				case "Vakcode":
				modules = modules.OrderBy( b => b.CourseCode ).ToList( );
				break;
				case "EC":
				modules = modules.OrderBy( b => b.AssignmentCode.Sum( s => s.EC ) ).ToList( );
				break;
				case "Ingangsniveau":
				modules = modules.OrderBy( b => b.Entry_Level ).ToList( );
				break;
				default:
				modules = modules.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
            @ViewBag.isPDF = isPDF;
			return View( modules.ToPagedList( pageNumber, pageSize ) );
		}

		[Authorize( Roles = "CreateModule" )]
		public ActionResult Create ( ) {
			Module module = new Module( );

			@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );

			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );

			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
            @ViewBag.GetAccounts = accountRepository.GetAll().Where(a => a.isActive == true);

			return View( module );
		}

		[ValidateInput( true ), HttpPost]
		public ActionResult Create ( Module module, int account_id ) {
			@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );
			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );
			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);


			try {

				@ViewBag.NewID = newVersion( module.Module_ID );

				//We create a new module and we store the id of it in a variabele
				var id = moduleRepository.Create( module ).Module_ID;

				//We save the combination

				//WERKENDE
				foreach ( var item in module.Level )
					moduleRepository.CompentenceAndModules( id, item.Competence_ID, item.Level1 );

             
				foreach ( var item in module.ModelWithWorkform )
					moduleRepository.WorkformAndModules( id, item.Workform_ID, item.Duration, item.Frequency, item.Workload );

				foreach ( var item in module.GradeType )
					moduleRepository.GradetypesAndModules( id, item.GradeDescription );

				foreach ( var item in module.WeekSchedule )
					moduleRepository.WeekSchedulesAndModules( id, item.Description, item.WeekNr );

				foreach ( var item in module.AssignmentCode )
					moduleRepository.AssignmentcodeAndModules( id, item.Description, item.EC );

				foreach ( var item in module.ModuleStudyPhasingBlock )
					moduleRepository.StudyBlockPhasingAndModules( id, item.Study_ID, item.Block_ID, item.Phasing_ID );

                moduleRepository.AddLinkingsModule(module);

                module.Account_ID = account_id;


				//We go back to the index.
				return RedirectToAction( "Index" );
			}
			catch ( DbEntityValidationException dbEx ) {
				foreach ( var validationErrors in dbEx.EntityValidationErrors ) {
					foreach ( var validationError in validationErrors.ValidationErrors ) {
						Trace.TraceInformation( "Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage );
					}
				}


				foreach ( var item in module.ModelWithWorkform )
					item.Workform = workformRepository.Get( item.Workform_ID );
				foreach ( var item in module.GradeType )
					item.Module = moduleRepository.Get( item.Module_ID );
				foreach ( var item in module.WeekSchedule )
					item.Module = moduleRepository.Get( item.Module_ID );
				foreach ( var item in module.AssignmentCode )
					item.Module = moduleRepository.Get( item.Module_ID );
				foreach ( var item in module.Level )
					item.Competence = competenceRepository.Get( item.Competence_ID );
				foreach ( var item in module.ModuleStudyPhasingBlock )
					item.Module = moduleRepository.Get( item.Module_ID );


				//Did something go wrong we return the view with the model.
				return View( module );
			}

		}


		public ActionResult Details ( int id,bool isPDF = false) {
			Module module = moduleRepository.Get( id );

			@ViewBag.NewID = newVersion( id );
            @ViewBag.isPDF = isPDF;
			return View( module );
		}

		[Authorize( Roles = "DeleteModule" )]
		public ActionResult Delete ( Module module ) {

			moduleRepository.Delete( module.Module_ID );
			return View( );
		}

		[Authorize( Roles = "EditModule" )]
		public ActionResult Edit ( int id ) {

			Module module = moduleRepository.Get( id );

			@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );

			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );
			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
            @ViewBag.GetAccounts = accountRepository.GetAll().Where(a => a.isActive == true);

			@ViewBag.NewID = newVersion( id );

			return View( module );
		}

		[HttpPost]
		public ActionResult Edit ( int id, Module module, int account_id ) {

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.ThemeList = GetThemes(module);
            @ViewBag.LearnGoalList = GetLearnGoals(module);
            @ViewBag.LearningToolList = GetLearningTools(module);
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
            @ViewBag.CompetenceList = GetCompetence(module);
            @ViewBag.StudyList = GetStudy(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);
            @ViewBag.WeekSchedule = GetWeekschedule(module);
            @ViewBag.AssignmentCode = GetAssignmentcode(module);
            @ViewBag.GetAccounts = accountRepository.GetAll();

            try
            {
                @ViewBag.NewID = newVersion(module.Module_ID);

                @ViewBag.LearnLineList = GetLearnLines(module).Where(m => m.isDeleted = false);
                @ViewBag.ThemeList = GetThemes(module).Where(m => m.isDeleted = false);
                @ViewBag.LearningToolList = GetLearningTools(module).Where(m => m.isDeleted = false);
                @ViewBag.LearnGoalList = GetLearnGoals(module).Where(m => m.isDeleted = false);
                @ViewBag.CompetenceList = GetCompetence(module).Where(m => m.isDeleted = false);
                @ViewBag.StudyList = GetStudy(module).Where(m => m.isDeleted = false);
                @ViewBag.WorkformList = GetWorkForms(module).Where(m => m.isDeleted = false);
                @ViewBag.GradeTypes = GetGradeTypes(module).Where(m => m.isDeleted = false);
                @ViewBag.WeekSchedule = GetWeekschedule(module).Where(m => m.isDeleted = false);
                @ViewBag.AssignmentCode = GetAssignmentcode(module).Where(m => m.isDeleted = false);
                @ViewBag.GetBlocks = GetBlock(module).Where(m => m.isDeleted = false);
                @ViewBag.GetPhasings = GetPhasings(module).Where(m => m.isDeleted = false);
                @ViewBag.GetAccounts = accountRepository.GetAll();

                moduleRepository.CompentenceAndModulesDelete(module.Module_ID);
                moduleRepository.WorkformAndModulesDelete(module.Module_ID);
                moduleRepository.StudyBlockPhasingAndModulesDelete(module.Module_ID);

                foreach (var item in module.Level)
                    moduleRepository.CompentenceAndModules(id, item.Competence_ID, item.Level1);

                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.WorkformAndModules(id, item.Workform_ID, item.Duration, item.Frequency, item.Workload);

                foreach (var item in module.ModuleStudyPhasingBlock)
                    moduleRepository.StudyBlockPhasingAndModules(id, item.Study_ID, item.Block_ID, item.Phasing_ID);
                
                moduleRepository.Update(module);
                return RedirectToAction("Index");

            }
            catch
            {
                return View(module);
            }

	/*		@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );
			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetAccounts = accountRepository.GetAll();

				//Get a list of modules based on this learnline, workform, gradetype. 
				@ViewBag.LearnLineList = GetLearnLines( module ).Where( m => m.isDeleted = false );
				@ViewBag.ThemeList = GetThemes( module ).Where( m => m.isDeleted = false );
				@ViewBag.LearningToolList = GetLearningTools( module ).Where( m => m.isDeleted = false );
				@ViewBag.LearnGoalList = GetLearnGoals( module ).Where( m => m.isDeleted = false );
				@ViewBag.CompetenceList = GetCompetence( module ).Where( m => m.isDeleted = false );
				@ViewBag.StudyList = GetStudy( module ).Where( m => m.isDeleted = false );
				@ViewBag.WorkformList = GetWorkForms( module ).Where( m => m.isDeleted = false );
				@ViewBag.GradeTypes = GetGradeTypes( module ).Where( m => m.isDeleted = false );
				@ViewBag.WeekSchedule = GetWeekschedule( module ).Where( m => m.isDeleted = false );
				@ViewBag.AssignmentCode = GetAssignmentcode( module ).Where( m => m.isDeleted = false );
                @ViewBag.GetBlocks = GetBlock(module).Where(m => m.isDeleted = false);
                @ViewBag.GetPhasings = GetPhasings(module).Where(m => m.isDeleted = false);
            */

				//if we update the model and somethign went wrong we send an error messge back


				//We delete all of the levels from this competence. 
                //moduleRepository.CompentenceAndModulesDelete( module.Module_ID );
                //moduleRepository.WorkformAndModulesDelete( module.Module_ID );
                //moduleRepository.AssignmentcodeAndModulesDelete( module.Module_ID );
                //moduleRepository.WeekSchedulesAndModulesDelete( module.Module_ID );
                //moduleRepository.GradetypesAndModulesDelete( module.Module_ID );
                //moduleRepository.StudyBlockPhasingAndModulesDelete(module.Module_ID);

                //module.Account_ID = account_id;

                //moduleRepository.UpdateLinkingsModule(module);
                //moduleRepository.AddLinkingsModule(module);


                    //foreach (var item in module.Level)
                    //    moduleRepository.CompentenceAndModules(id, item.Competence_ID, item.Level1);

                    //foreach (var item in module.ModelWithWorkform)
                    //    moduleRepository.WorkformAndModules(id, item.Workform_ID, item.Duration, item.Frequency, item.Workload);

                    //foreach (var item in module.WeekSchedule)
                    //    moduleRepository.WeekSchedulesAndModules(id, item.Description, item.WeekNr);

                    //foreach (var item in module.GradeType)
                    //    moduleRepository.GradetypesAndModules(id, item.GradeDescription);

                    //foreach (var item in module.AssignmentCode)
                    //    moduleRepository.AssignmentcodeAndModules(id, item.Description, item.EC);

                    //foreach (var item in module.ModuleStudyPhasingBlock)
                    //    moduleRepository.StudyBlockPhasingAndModules(id, item.Study_ID, item.Block_ID, item.Phasing_ID);

                //return RedirectToAction( "Index" );

		}




		[Authorize( Roles = "toNewVersionModule" )]
		//TODO moet aangepast worden met koppelingen
		public ActionResult toNewVersion ( int id ) {
			Module module = moduleRepository.Get( id );

			var model = new Module( );
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
            model.ModuleStudyPhasingBlock = module.ModuleStudyPhasingBlock;

			@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );
			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );
			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
            @ViewBag.GetAccounts = accountRepository.GetAll().Where(a => a.isActive == true);


			@ViewBag.NewID = newVersion( id );

			return View( model );
		}

		[ValidateInput( true ), HttpPost]
		public ActionResult toNewVersion ( int id, Module module ) {
			@ViewBag.LearnLineList = GetLearnLines( module );
			@ViewBag.ThemeList = GetThemes( module );
			@ViewBag.LearnGoalList = GetLearnGoals( module );
			@ViewBag.LearningToolList = GetLearningTools( module );
			@ViewBag.CompetenceList = GetCompetence( module );
			@ViewBag.StudyList = GetStudy( module );
			@ViewBag.WorkformList = GetWorkForms( module );
			@ViewBag.GradeTypes = GetGradeTypes( module );
			@ViewBag.WeekSchedule = GetWeekschedule( module );
			@ViewBag.AssignmentCode = GetAssignmentcode( module );
            @ViewBag.GetBlocks = GetBlock(module);
            @ViewBag.GetPhasings = GetPhasings(module);
            @ViewBag.GetAccounts = accountRepository.GetAll();

			try {
				@ViewBag.NewID = newVersion( id );

				int newestID = moduleRepository.Create( module ).Module_ID;

            
				foreach ( var item in module.Level )
					moduleRepository.CompentenceAndModules( newestID, item.Competence_ID, item.Level1 );

				foreach ( var item in module.ModelWithWorkform )
					moduleRepository.WorkformAndModules( newestID, item.Workform_ID, item.Duration, item.Frequency, item.Workload );

				foreach ( var item in module.GradeType )
					moduleRepository.GradetypesAndModules( newestID, item.GradeDescription );

				foreach ( var item in module.WeekSchedule )
					moduleRepository.WeekSchedulesAndModules( newestID, item.Description, item.WeekNr );

				foreach ( var item in module.AssignmentCode )
					moduleRepository.AssignmentcodeAndModules( newestID, item.Description, item.EC );

				foreach ( var item in module.ModuleStudyPhasingBlock )
					moduleRepository.StudyBlockPhasingAndModules( id, item.Study_ID, item.Block_ID, item.Phasing_ID );

				module.Module_ID = newestID;

				moduleRepository.AddLinkingsModule( module );


				moduleRepository.Delete( id );
				return RedirectToAction( "Index" );
			}
			catch {

				foreach ( var item in module.ModelWithWorkform )
					item.Workform = workformRepository.Get( item.Workform_ID );
				foreach ( var item in module.GradeType )
					item.Module = moduleRepository.Get( item.Module_ID );
                foreach (var item in module.WeekSchedule)
                    item.Module = moduleRepository.Get(item.Module_ID);
                foreach (var item in module.AssignmentCode)
                    item.Module = moduleRepository.Get(item.Module_ID);
				foreach ( var item in module.Level )
					item.Competence = competenceRepository.Get( item.Competence_ID );
				foreach ( var item in module.ModuleStudyPhasingBlock )
					item.Module = moduleRepository.Get( item.Module_ID );

				return View( module );
			}
		}


		[Authorize( Roles = "toNewVersionModule" )]
		private int newVersion ( int id ) {
			Module newer = moduleRepository.GetNewVersion( id );
			return ( newer != null ) ? newer.Module_ID : -1;
		}

		private List<LearnLine> GetLearnLines ( Module module ) {
			List<LearnLine> learnLines = learnLineRepository.GetAll( );

			if ( module != null )
				foreach ( LearnLine lnl in module.LearnLine )
					learnLines.Remove( learnLines.Where( b => b.LearnLine_ID == lnl.LearnLine_ID ).First( ) );

			return learnLines.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Theme> GetThemes ( Module module ) {
			List<Theme> themes = themeRepository.GetAll( );

			if ( module != null )
				foreach ( Theme t in module.Theme )
					themes.Remove( themes.Where( b => b.Theme_ID == t.Theme_ID ).First( ) );

			return themes.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Workform> GetWorkForms ( Module module ) {
			List<Workform> workforms = workformRepository.GetAll( );

			if ( module != null )
				foreach ( ModelWithWorkform mwf in module.ModelWithWorkform )
					workforms.Remove( workforms.Where( a => a.Workform_ID == mwf.Workform_ID ).First( ) );

			return workforms.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Competence> GetCompetence ( Module module ) {
			List<Competence> competences = competenceRepository.GetAll( );
			if ( module != null )
				foreach ( Level lvl in module.Level )
					competences.Remove( competences.Where( b => b.Competence_ID == lvl.Competence_ID ).FirstOrDefault( ) );

			return competences.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<LearnGoal> GetLearnGoals ( Module module ) {
			List<LearnGoal> learngoals = learngoalRepository.GetAll( );

			if ( module != null )
				foreach ( LearnGoal t in module.LearnGoal )
					learngoals.Remove( learngoals.Where( b => b.LearnGoal_ID == t.LearnGoal_ID ).First( ) );

			return learngoals.Where( m => m.isDeleted == false ).ToList( );
		}

        private List<LearningTool> GetLearningTools(Module module)
        {
            List<LearningTool> learningtools = learningtoolRepository.GetAll();

            if (module != null)
                foreach (LearningTool lt in module.LearningTool)
                    learningtools.Remove(learningtools.Where(b => b.LearnTool_ID == lt.LearnTool_ID).First());

            return learningtools.Where(m => m.isDeleted == false).ToList();
        }

		private List<Study> GetStudy ( Module module ) {
			List<Study> studies = studyRepository.GetAll( );

			if ( module != null )
				foreach ( Study lnl in module.Study )
					studies.Remove( studies.Where( b => b.Study_ID == lnl.Study_ID ).First( ) );

			return studies.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Module> GetGradeTypes ( Module module ) {
			List<Module> gradetypes = moduleRepository.GetAll( );

			if ( module != null )
				foreach ( GradeType mwf in module.GradeType )
					gradetypes.Remove( gradetypes.Where( b => b.Module_ID == mwf.Module_ID ).FirstOrDefault( ) );

			return gradetypes.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Module> GetWeekschedule ( Module module ) {
			List<Module> weekschedules = moduleRepository.GetAll( );

			if ( module != null )
				foreach ( WeekSchedule mwf in module.WeekSchedule )
					weekschedules.Remove( weekschedules.Where( b => b.Module_ID == mwf.Module_ID ).FirstOrDefault( ) );

			return weekschedules.Where( m => m.isDeleted == false ).ToList( );
		}

		private List<Module> GetAssignmentcode ( Module module ) {
			List<Module> assignmentcodes = moduleRepository.GetAll( );

			if ( module != null )
				foreach ( AssignmentCode mwf in module.AssignmentCode )
					assignmentcodes.Remove( assignmentcodes.Where( b => b.Module_ID == mwf.Module_ID ).FirstOrDefault( ) );

			return assignmentcodes.Where( m => m.isDeleted == false ).ToList( );
		}

        private List<Block> GetBlock(Module module)
        {
            List<Block> blocks = blockRepository.GetAll();

            if (module != null)
                foreach (ModuleStudyPhasingBlock mwf in module.ModuleStudyPhasingBlock)
                    blocks.Remove(blocks.Where(b => b.Block_ID == mwf.Block_ID).FirstOrDefault());

            return blocks.Where(m => m.isDeleted == false).ToList();
        }

        private List<Phasing> GetPhasings(Module module)
        {
            List<Phasing> phasings = phasingRepository.GetAll();

            if (module != null)
                foreach (ModuleStudyPhasingBlock mwf in module.ModuleStudyPhasingBlock)
                    phasings.Remove(phasings.Where(b => b.Phasing_ID == mwf.Phasing_ID).FirstOrDefault());

            return phasings.Where(m => m.isDeleted == false).ToList();
        }

        public ActionResult GeneratePDF(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new { id = id, isPDF= true });
        }

        public ActionResult GeneratePDFList()
        {
            return new Rotativa.ActionAsPdf("Index", new { isPDF = true });
        }

	}
}
