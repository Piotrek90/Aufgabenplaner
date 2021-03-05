using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenplaner
{
    public class Aufgabe
    {
        public int Id { get; set; }
        public string Kurzbezeichnung { get; set; }
        public string Aufgabenbeschreibung { get; set; }
        public bool Erledigt { get; set; }
    }
}
