using System;

namespace Altkom.UTC.Core.Models
{
    public class Device : Base
    {
        public string Name { get; set; }
        public string Firmware { get; set; }
        public bool IsActive { get; set; }
        public string Color { get; set; }
        public Customer Customer { get; set; }
    }
}
