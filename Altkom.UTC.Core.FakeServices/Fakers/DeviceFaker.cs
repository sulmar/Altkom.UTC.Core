using Altkom.UTC.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.FakeServices.Fakers
{
    public class DeviceFaker : Faker<Device>
    {
        public DeviceFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Lorem.Word());
            RuleFor(p => p.Firmware, f => f.System.Version().ToString());
            RuleFor(p => p.IsActive, f => f.Random.Bool(0.8f));
            Ignore(p => p.Color);
            Ignore(p => p.Customer);
        }
    }
}
