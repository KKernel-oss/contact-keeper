using System;
using System.Collections.Generic;
using System.Text;

namespace Keeper.Models.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<DateTime> created { get; set; }
    }

    public class LoginDto
    {
        public string name { get; set; }
        public string password { get; set; }
    }
}
