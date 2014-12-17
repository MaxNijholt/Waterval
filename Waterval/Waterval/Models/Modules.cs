using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Waterval.Models
{
    public class Modules
    {
        public string Titel { get; set; }

        public string Beschrijving { get; set; }
        public string Vakcode { get; set; }

        public string Studiebelasting { get; set; }

        public string Werkvorm { get; set; }

        public string Ingangsniveau { get; set; }

        public string Vakcoordinator { get; set; }

        public string Leerdoelen { get; set; }

        public string Beoordelingen { get; set; }

        public string BeschrijvingLang { get; set; }
        public string Voorkennis { get; set; }
        public Modules(string title, string beschrijving, string vakcode, string studiebelasting, string werkvorm, string ingangsniveau,
            string vakcoordinator,
           string leerdoelen,
            string beoordelingen,string beschrijvinglang, string voorkennis)
        {

            Titel = title;
            Beschrijving = beschrijving;
            Vakcode = vakcode;
            Studiebelasting = studiebelasting;
            Werkvorm = werkvorm;
            Ingangsniveau = ingangsniveau;
            Vakcoordinator = vakcoordinator;
            Leerdoelen = leerdoelen;
            Beoordelingen = beoordelingen;
            BeschrijvingLang = beschrijvinglang;
            Voorkennis = voorkennis;
        }
    }
}