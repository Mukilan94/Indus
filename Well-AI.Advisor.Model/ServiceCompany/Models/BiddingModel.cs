using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class BiddingModel
    {
        [Display(Name = "Catagory ID")]
        public string Cat_id { get; set; }

        [Display(Name = "Catagory Name")]
        public string Cat_name { get; set; }

        [Display(Name = "Item ID")]
        public string Item_id { get; set; }

        [Display(Name = "Item Name")]
        public string Item_name { get; set; }

        [Display(Name = "Item Description")]
        public string Item_description { get; set; }

        [Display(Name = "Item Amount")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public decimal Item_amount { get; set; }


        [Display(Name = "Bidding Start Date")]
        [DataType(DataType.Date)]
        public DateTime Item_date_open { get; set; }

        [Display(Name = "Bidding Start Date")]
        [DataType(DataType.Date)]
        public DateTime Item_date_close { get; set; }

        [Display(Name = "Bidding Date")]
        [DataType(DataType.Date)]
        public DateTime Item_date_bid { get; set; }

        [Display(Name = "Item Location")]
        public string Item_location { get; set; }

        [Display(Name = "Item Seller")]
        public string Item_seller { get; set; }

        public int Status { get; set; }

        public List<BiddingStatus> BiddingStatus { get; set; }
    }


}
