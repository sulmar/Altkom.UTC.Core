using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.Models
{
    public class Customer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
    }


    public enum Gender
    {
        Female,
        Male
    }
}
