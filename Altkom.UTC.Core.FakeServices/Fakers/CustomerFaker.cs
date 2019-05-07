using Altkom.UTC.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.FakeServices.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
            RuleFor(p => p.Birthday, f => f.Person.DateOfBirth);
            RuleFor(p => p.Email, f => f.Person.Email);
            Ignore(p => p.IsDeleted);
        }
    }
}
