using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("UserWell")]

    public class UserWell
    {
        [Key]
        [StringLength(40)]
        public string Id { get; set; }

        [StringLength(40)]
        public string UserId { get; set; }

        [StringLength(40)]
        public string WellId { get; set; }
    }
}

