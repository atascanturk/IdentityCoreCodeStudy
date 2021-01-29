using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class RoleAssignViewModel
    {

        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }
    }
}
