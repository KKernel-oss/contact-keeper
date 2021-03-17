using System;
using System.Collections.Generic;
using System.Text;

namespace Keeper.Models.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ContactType ContactType { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int Owner { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
    }

    public enum ContactType { Personal, Professional }
}
