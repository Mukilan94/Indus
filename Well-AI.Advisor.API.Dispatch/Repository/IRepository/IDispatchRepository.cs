using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Dispatch.Models;


namespace WellAI.Advisor.API.Dispatch.Repository.IRepository
{
    public interface IDispatchRepository
    {

        public Task<DispatchRoutesResponse> GetDispatchRoutes(DispatchRoutesRequest Request);
        public Task<RouteAcceptedResponse> RouteAccepted(RouteAcceptedRequest request);
        public Task<RecordDestinationChangesResponse> RecordDestinationChanges(RecordDestinationChangesRequest request);
    }
}
