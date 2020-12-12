using System;
using Convey.Types;

namespace UnicornParty.Models
{
    public class Unicorn : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public double CornLength { get; set; }
    }
}