using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.Models
{
    public class User : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
