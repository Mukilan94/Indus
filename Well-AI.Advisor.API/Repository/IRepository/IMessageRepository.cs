using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IMessageRepository
    {
        ICollection<Message> GetMessageDetails();
        Message GetMessageDetail(string uid);
        bool MessageExists(string Uid);
        bool CreateMessage(Message message);
        bool UpdateMessage(Message message);
        bool DeleteMessage(Message message);
        bool Save();
    }
}
