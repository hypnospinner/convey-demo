using System;
using Convey.Types;

namespace UnicornParty.Models
{
    public enum PartyStatus
    {
        ORDERED,
        PAYED,
        FINISHED
    }

    public class Party : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UnicornId { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public PartyStatus Status { get; set; }
    }
}