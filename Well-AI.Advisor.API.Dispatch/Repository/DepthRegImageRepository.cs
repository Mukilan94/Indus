using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Data;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository.IRepository;

namespace Well_AI.Advisor.API.Repository
{
    public class DepthRegImageRepository : IDepthRegImageRepository
    {
        private readonly WellAIAdvisiorContext _db;

        public DepthRegImageRepository(WellAIAdvisiorContext db)
        {
            _db = db;
        }
        //public bool ConvCoreExists(string uid)
        //{
        //    bool value = _db.ConvCores.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
        //    return value;
        //}

        //public bool CreateConvCore(ConvCore convCore)
        //{
        //    _db.ConvCores.Add(convCore);
        //    return Save();
        //}

        //public bool DeleteConvCore(ConvCore convCore)
        //{
        //    _db.ConvCores.Remove(convCore);
        //    return Save();
        //}

        //public ConvCore GetConvCoreDetail(string Uid)
        //{
        //    return _db.ConvCores.FirstOrDefault(x=>x.Uid==Uid);
            
        //}

        //public ICollection<ConvCore> GetConvCoreDetails()
        //{
        //    return _db.ConvCores.OrderBy(x => x.NameWell).ToList();
        //}

        //public bool Save()
        //{
        //    return _db.SaveChanges() >= 0 ? true : false;
        //}

        //public bool UpdateConvCore(ConvCore convCore)
        //{
        //    _db.ConvCores.Update(convCore);
        //    return Save();
        //}
    }
}
