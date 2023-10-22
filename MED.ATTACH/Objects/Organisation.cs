using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.ATTACH.Objects
{
    public class Organisation
    {
        public string Name { get; set; }
        public List<Patient> Patients { get; set; }
        public Organisation ConvertToOrganisation(string value)
        {
            Organisation organisation = new Organisation();
            organisation.Name = value;
            return organisation;
        }
    }
}
