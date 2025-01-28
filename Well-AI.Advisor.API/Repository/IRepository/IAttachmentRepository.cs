using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;
using WellAI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IAttachmentRepository
    {
        ICollection<Attachment> GetAttachmentDetails();
        Attachment GetAttachmentDetail(string Uid);
        bool AttachmentExists(string Uid);
        bool UploadAttachment(Attachment attachment);
        bool UpdateAttachment(Attachment attachment);
        bool DeleteAttachment(Attachment attachment);
        bool Save();

    }
}
