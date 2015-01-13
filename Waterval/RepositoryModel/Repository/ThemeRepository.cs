using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
	public class ThemeRepository : IThemeRepository
	{ 
		Project_WatervalEntities dbContext;

        public ThemeRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }
        public List<DomainModel.Models.Theme> GetAll()
        {
            return dbContext.Theme.Where(b => b.isDeleted == false).ToList();
        }
		public DomainModel.Models.Theme Get(int theme_id)
		{
			return dbContext.Theme.Find(theme_id);
		}
		public DomainModel.Models.Theme Create(DomainModel.Models.Theme theme)
		{
            AddLinkingsTheme(theme);
			dbContext.Theme.Add(theme);
			dbContext.SaveChanges();
			return theme;
		}


        public void AddLinkingsTheme(Theme theme)
        {

            List<Module> modules = new List<Module>();

            for (int index = 0; index < theme.Module.Count; index++)
            {
                modules.Add(dbContext.Module.Find(theme.Module.ElementAt(index).Module_ID));
            }

            theme.Module = modules;

        }

        public void UpdateLinkingsTheme(Theme theme)
        {

            for (int index = 0; index < theme.Module.Count; index++)
            {
                theme.Module.Remove(dbContext.Module.Find(theme.Module.ElementAt(index).Module_ID));
            }
        }


		public DomainModel.Models.Theme Update(DomainModel.Models.Theme update)
		{
			Theme theme = dbContext.Theme.SingleOrDefault(b => b.Theme_ID == update.Theme_ID);
			if(theme == null)
				return null;

			theme.Definition = update.Definition;
			theme.Title = update.Title;

            if (theme.Module.Count == 0 && update.Module.Count > 0)
            {
                AddLinkingsTheme(update);
                theme.Module = update.Module;
            }
            else
                UpdateLinkingsTheme(theme);

			dbContext.SaveChanges();
			return theme;
		}
		public void Delete(int theme_id)
		{
			Theme theme = dbContext.Theme.SingleOrDefault(c => c.Theme_ID == theme_id);
			theme.isDeleted = true;
			theme.DeleteDate = DateTime.UtcNow;
			dbContext.SaveChanges();
		}
	}
}
