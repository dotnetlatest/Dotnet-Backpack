using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpack.WebApi.Security
{
    public class User
    {
        public string CompoundUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Organization Organization { get; set; }
    }
}
