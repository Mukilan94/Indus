using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IObjectGroupRepository
    {
        ICollection<ObjectGroup> GetObjectGroupDetails();
        ObjectGroup GetObjectGroupDetail(string uid);
        bool ObjectGroupExists(string Uid);
        bool CreateObjectGroup(ObjectGroup objectGroup);
        bool UpdateObjectGroup(ObjectGroup objectGroup);
        bool DeleteObjectGroup(ObjectGroup objectGroup);
        bool Save();
    }
}
