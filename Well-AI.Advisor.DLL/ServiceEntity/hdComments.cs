using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("hdComments")]
    public class hdComments
    {
        [Key]
        [Required]
        public int CommentID { get; set; }
        public int IssueID { get; set; }
        public DateTime CommentDate { get; set; }
        public string UserID { get; set; }
        public string Body { get; set; }
        public Boolean ForTechsOnly { get; set; }
        public Boolean IsSystem { get; set; }
        public string Recipients { get; set; }
        public string BouncedRecipients { get; set; }
        public string EmailHeaders { get; set; }
        public string BounceReason { get; set; }
        
    }
}
