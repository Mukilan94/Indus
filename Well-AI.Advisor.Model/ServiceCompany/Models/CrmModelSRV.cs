using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class CrmModelSRV
    {
        public int CompanyID { get; set; }
        public int InstanceID { get; set; }
        public string UserID { get; set; }

        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string TenantID{ get; set; }
        public string ProviderId { get; set; }
    }

    public class CrmModelSRVContact
    {
        public int ContactID { get; set; }
        public int InstanceID { get; set; }
        public int CompanyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title{ get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Fax { get; set; }
        public string Comments { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string TenantID { get; set; }

        public string ContactName { get; set; }
    }
    public class CrmModelSRVComments
    {
        public int CommentId { get; set; }
        public int ContactId { get; set; }
        public int CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime CommentDate { get; set; }
        public int? UserId { get; set; }

        public string Body { get; set; }
        public int CommentType { get; set; }
        public int InstanceId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactName { get; set; }

    }

    public class CRMCompany
    {
        public int CRMCompanyID { get; set; }
        public string Name { get; set; }
    }

    public class CRMContactModel
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
    }
}
