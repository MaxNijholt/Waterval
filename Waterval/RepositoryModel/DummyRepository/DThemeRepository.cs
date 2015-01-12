using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
	public class DThemeRepository : IThemeRepository
	{
		public List<DomainModel.Models.Theme> GetAll()
		{
            List<Theme> fakethemes = new List<Theme>()
            {
            };
            return fakethemes;

        }

        public IQueryable<Theme> Theme
        {
            get { return (IQueryable<Theme>)GetAll(); }
        }


        public DomainModel.Models.Theme Get(int theme_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Theme Create(DomainModel.Models.Theme theme)
        {
            throw new NotImplementedException();
        }

        public void Delete(int theme_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Theme Update(DomainModel.Models.Theme theme)
        {
            throw new NotImplementedException();
        }
	}
}
