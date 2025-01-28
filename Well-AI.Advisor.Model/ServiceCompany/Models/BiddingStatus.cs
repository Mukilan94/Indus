using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class BiddingStatus
    {

        public BiddingStatus(int? new1, int? hashsa, int? current, int? bidding, int? intreasted, int? abandoned, int? lost, int? won)
        {
            New = new1;
            HasHSA = hashsa;
            Current = current;
            Bidding = bidding;
            Intreasted = intreasted;
            Abandoned = abandoned;
            Lost = lost;
            Won = won;
        }
        public int? New { get; set; }
        public int? HasHSA { get; set; }
        public int? Current { get; set; }
        public int? Bidding { get; set; }
        public int? Intreasted { get; set; }
        public int? Abandoned { get; set; }
        public int? Lost { get; set; }
        public int? Won { get; set; }
    }
}


