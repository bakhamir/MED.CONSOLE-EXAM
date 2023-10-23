using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.ATTACH.Objects
{
    public class UserRequests : Organisation
    {
        public ObjectId Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public Patient Patient { get; set; }
        public string status { get; set; }

        UserRequests() { }

        public UserRequests(Organisation organisation, Patient patient)
        { 
            this.Name= organisation.Name;
            this.Patient= patient;
            this.status = "unwatched";
        }
    }

}
