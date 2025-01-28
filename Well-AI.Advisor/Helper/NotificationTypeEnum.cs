using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Helper
{
    public enum NotificationTypeEnum
    {


        [Display(Name = ("Missed Video Call"))]
        MissedCall = 0,
        [Display(Name = ( "Message"))]
        Message = 1,
        [Display(Name = ("Support Ticket"))]
        Support = 2,
        [Display(Name = ("Bid"))]
        Bids = 3,
        [Display(Name = ("Prediction"))]
        Prediction = 4,
        [Display(Name = ("Bid Request"))]
        BidRequest = 5,
        [Display(Name = ("Service Request"))]
        ServiceRequest = 6
    }
}
