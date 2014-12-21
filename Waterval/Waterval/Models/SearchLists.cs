using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Waterval.Models
{
    public class SearchLists
    {

        public List<Block> Blocks
        {
            get;
            set;
        }

        public List<LearnLine> LearnLines
        {
            get;
            set;
        }

        public List<Competence> Compentences
        {
            get;
            set;
        }

        public List<Theme> Themes
        {
            get;
            set;
        }

        public List<Module> Modules
        {
            get;
            set;
        }

    }
}