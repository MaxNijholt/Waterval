using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryModel.DummyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests
{
    [TestClass]
    public class ThemeTest
    {
        DummyThemeRepository themeRep;

        [TestMethod]
        public void GetAll()
        {
            themeRep = new DummyThemeRepository();

            List<Theme> th = themeRep.GetAll();

            int result = th.Count;

            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetByID()
        {
            themeRep = new DummyThemeRepository();

            Theme test = new Theme { Theme_ID = 4, Definition = "Theme 4" };
            Theme gTheme = themeRep.Get(4);


            Assert.AreEqual(test.Theme_ID, gTheme.Theme_ID);
            Assert.AreEqual(test.Definition, gTheme.Definition);
        }

        [TestMethod]
        public void Create()
        {
            themeRep = new DummyThemeRepository();

            themeRep.Create( new Theme { Theme_ID = 11, Definition = "Workform 11" });

            List<Theme> themes = themeRep.GetAll();

            Assert.AreEqual(11, themes.Count);
        }

        [TestMethod]
        public void Delete()
        {
            themeRep = new DummyThemeRepository();

            themeRep.Delete(3);

            List<Theme> themes = themeRep.GetAll().Where(x => x.isDeleted == false).ToList();

            Assert.AreEqual(9, themes.Count);

            Assert.AreEqual(1, themeRep.GetAll().Where(x => x.isDeleted == true).Count());

            Assert.AreEqual(themeRep.Get(3).DeleteDate, DateTime.UtcNow);
            Assert.AreEqual(themeRep.Get(3).isDeleted, true);
        }

        [TestMethod]
        public void Edit()
        {
            themeRep = new DummyThemeRepository();

            Theme test = new Theme { Theme_ID = 4, Definition = "Themaangepast 4" };

            themeRep.Update(test);

            Theme gTheme = themeRep.Get(4);

            Assert.AreEqual(test.Theme_ID, gTheme.Workform_ID);
            Assert.AreEqual(test.DeleteDate, gTheme.DeleteDate);
            Assert.AreEqual(test.isDeleted, gTheme.isDeleted);
            Assert.AreEqual(test.Definition, gTheme.Description);
        }
    }
}
