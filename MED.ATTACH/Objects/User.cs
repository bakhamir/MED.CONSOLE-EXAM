using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.ATTACH.Objects
{
    public class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public string rights { get; set; }
        public Organisation organisation { get; set; }
    }
}
