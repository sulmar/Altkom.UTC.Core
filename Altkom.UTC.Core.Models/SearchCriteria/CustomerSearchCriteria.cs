using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.Models.SearchCriteria
{
    public abstract class SearchCriteria
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string FirstName { get; set; }
    }
}
