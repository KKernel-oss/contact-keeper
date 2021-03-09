using System;
using System.Collections.Generic;
using System.Text;

namespace Keeper.Models.Models
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginDto
    {
        public string name { get; set; }
        public string password { get; set; }
    }
}
