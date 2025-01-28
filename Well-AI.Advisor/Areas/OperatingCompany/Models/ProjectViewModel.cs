using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class ProjectViewModel
    {
        [ScaffoldColumn(false)]
        public int ProjectID
        {
            get;
            set;
        }

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }

        public DateTime? StartDate
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }

        public string ContactCity
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }

    public class FieldTicket
    {
        [ScaffoldColumn(false)]
        public int fdId
        {
            get;
            set;
        }
        public int ProjectID
        {
            get;
            set;
        }
        public string Ticket
        {
            get;
            set;
        }
        public string Invoice
        {
            get;
            set;
        }

        public string Rig { get; set; }
        public string Lease { get; set; }
        public string PoAfe { get; set; }
        public string County { get; set; }
        public string BillTo { get; set; }

        public string ItemsDescription { get; set; }
        public List<TicketFieldItem> Items;
        public double Subtotal { get; set; }
        public double SalesTaxCount { get; set; }
        public double SalesTaxValue { get; set; }
        public double Total { get; set; }

        public DateTime? Date
        {
            get;
            set;
        }

        public double Amount
        {
            get;
            set;
        }
    }

    public class TicketFieldItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}
