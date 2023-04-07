using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVOG
{
    public class PartialClass
    {
        public Ticket ticket { get; set; }

        public int count { get; set; }

        public int order { get; set; }

    }

    public partial class Surdoperevodchik
    {
        public string FIO
        {
            get
            {
                return SurnameSurdo + " " + NameSurdo + " " + FatherlandSurdo;
            }
        }
    }
}
