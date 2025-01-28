using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Samsara.Models;

namespace Well_AI.Advisor.API.Samsara.Services.IServices
{
    
   public interface IVechicleService
    {
         Task<VechicleModel> GetVechicleAsync();  
        Task<VechicleModel> GetVechicleAsyncByTenant(string tenantId);

        Task<VechicleModel> GetVechicleAsync(int limit,string direction,string [] parentTagIds,string [] tagIds);
        Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync();

        Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync(int limit, string direction, string[] parentTagIds, string[] tagIds);
        Task<VechicleFollowFeedLocationModel> GetFllowFeedOfVechicleLocationAsync();
        Task<VechicleFollowFeedLocationModel> GetFllowFeedOfVechicleLocationAsync(string[] tagIds, string direction, string[] parentTagIds, string[] vechicleIds);

        Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync(string StartTime, string EndTime);
        Task<VechicleStatusModel> GetMostRecentVechicleStatusAsync(string [] types);
        Task<BatteryMilliVoltStatusModel> GetMostRecentVechicleStatusForBatteryMilliVoltAsync(string types);

        Task<VechicleLocationModel> GetHistoricalVechicleLocationAsync(string StartTime,string EndTime, string[] tagIds, string direction, string[] parentTagIds, string[] vehicleIds);
        Task<VechicleLocationModel> GetHistoricalVechicleStatusAsync(string StartTime, string EndTime,string type);

        Task<VechicleStatusModel> GetFllowFeedOfVechicleStatusAsync(string types);

        Task<VechicleLocation> GetMostRecentVechicleLocationByVechicleIdAsync(string vechicleId);
        Task<VechicleLocation> GetMostRecentVechicleLocationByVechicleIdAsync(string vechicleId, string tenantId);
        Task<VechicleModel> GetVechicleAsync(string VechicleId);
       
    }
}
