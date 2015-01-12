using DomainModel.Models;
using System.Collections.Generic;

namespace RepositoryModel.IRepository
{
	public interface IThemeRepository
	{
		List<Theme> GetAll();
		Theme Get(int theme_id);
		Theme Create(Theme theme);
		void Delete(int theme_id);
		Theme Update(Theme theme);
	}
}
