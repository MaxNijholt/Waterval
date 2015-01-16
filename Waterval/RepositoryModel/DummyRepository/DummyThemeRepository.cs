﻿using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyThemeRepository: IThemeRepository
    {
        List<Theme> testThemes;

        public DummyThemeRepository()
        {
            FillList();
        }

        public List<Theme> GetAll()
        {
            return testThemes;
        }

        public Theme Get(int theme_id)
        {
            return testThemes.Where(x => x.Theme_ID == theme_id).First();
        }

        public Theme Create(Theme theme)
        {
            testThemes.Add(theme);
            return theme;
        }

        public void Delete(int theme_id)
        {
            Theme f = testThemes.Where(x => x.Theme_ID == theme_id).First();
            f.isDeleted = true;
            f.DeleteDate = DateTime.UtcNow;
        }

        public Theme Update(Theme theme)
        {
            Theme f = testThemes.Where(x => x.Theme_ID == theme.Theme_ID).First();

            f.Definition = theme.Definition;

            return f;
        }

        private void FillList()
        {
            testThemes = new List<Theme>();

            for(int i = 1; i <= 10; i++)
                testThemes.Add(new Theme{ Theme_ID = i, Definition  = "Theme " + i });
        }
    }
}
