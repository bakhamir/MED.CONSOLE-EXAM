using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.ATTACH.Objects
{
    public class UserRequests : Organisation
    {
        public DateTime CreationDate { get; set; }
        public Patient Patient { get; set; }
        public string status { get; set; }

        public UserRequests(Organisation organisation, Patient patient)
        { 
            this.Name= organisation.Name;
            this.Patient= patient;
        }
    }
}
