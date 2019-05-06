using System;

namespace Altkom.UTC.Core.Models
{
    public class Device : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firmware { get; set; }
        public bool IsActive { get; set; }
        public string Color { get; set; }
    }
}
