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
			dbContext.Theme.Add(theme);
			dbContext.SaveChanges();
			return theme;
		}
		public DomainModel.Models.Theme Update(DomainModel.Models.Theme update)
		{
			Theme theme = dbContext.Theme.SingleOrDefault(b => b.Theme_ID == update.Theme_ID);
			if(theme == null)
				return null;

			theme.Definition = update.Definition;
			theme.Title = update.Title;
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
