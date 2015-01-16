using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyThemeRepository: IWorkformRepository
    {
        List<Theme> themes;

        public DummyThemeRepository()
        {
            FillList();
        }

        public List<Theme> GetAll()
        {
            return themes;
        }

        public Theme Get(int theme_id)
        {
            return themes.Where(x => x.Theme_ID == theme_id).First();
        }

        public Theme Create(Theme theme)
        {
            themes.Add(theme);
            return theme;
        }

        public void Delete(int theme_id)
        {
            Theme f = themes.Where(x => x.Theme_ID == theme_id).First();
            f.isDeleted = true;
            f.DeleteDate = DateTime.UtcNow;
        }

        public Theme Update(Theme theme)
        {
            Theme f = themes.Where(x => x.Theme_ID == theme.Theme_ID).First();

            f.Definition = theme.Definition;

            return f;
        }

        private void FillList()
        {
            themes = new List<Theme>();

            for(int i = 1; i <= 10; i++)
                themes.Add(new Theme{ Theme_ID = i, Definition  = "Theme " + i });
        }
    }
}
