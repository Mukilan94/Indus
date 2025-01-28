using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Samsara.Data;
using Well_AI.Advisor.API.Samsara.Models;
using Well_AI.Advisor.API.Samsara.Services.IServices;

namespace Well_AI.Advisor.API.Samsara.Services
{
    public class VechicleService : IVechicleService
    {
        private const string BaseUrl = "https://api.samsara.com/fleet/vehicles";
        private readonly HttpClient _client;
        private readonly IAuthenticationService _authenticationService;
        WellAIAdvisiorApiSamsaraContext _db = new WellAIAdvisiorApiSamsaraContext();

        public VechicleService(HttpClient client, IAuthenticationService authenticationService)
        {
            _client = client;
            _authenticationService = authenticationService;
        }

        #region Get Vechicle 
        public async Task<VechicleModel> GetVechicleAsync()
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);
                var httpResponse = await _client.GetAsync(BaseUrl);

                if     (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();

                VechicleModel _res = JsonConvert.DeserializeObject<VechicleModel>(content);
                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetVechicleAsync", null);
                throw;
            }
        }

        public async Task<VechicleModel> GetVechicleAsyncByTenant(string tenantId)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi(tenantId);
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.SamsaraApiKey);
                var httpResponse = await _client.GetAsync(BaseUrl);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();

                VechicleModel _res = JsonConvert.DeserializeObject<VechicleModel>(content);
                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetVechicleAsyncByTenant", tenantId);
                throw;
            }
        }


        public async Task<VechicleModel> GetVechicleAsync(int limit, string direction, string[] parentTagIds, string[] tagIds)
        {
            VechicleModel _res = new VechicleModel();
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var _parentTagIds = String.Join(", ", parentTagIds);
                var _tagIds = String.Join(",", tagIds);
                var httpResponse = await _client.GetAsync(BaseUrl + "?limit=" + limit + "&" + direction + "&parentTagIds=" + _parentTagIds + "&tagIds=" + _tagIds);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                _res = JsonConvert.DeserializeObject<VechicleModel>(content);

                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetVechicleAsync", null);
                throw;
            }


        }

        #endregion

        #region Get Vechicle Location
        public async Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync()
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations");

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                VechicleLocationModel _res = JsonConvert.DeserializeObject<VechicleLocationModel>(content);

                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetMostRecentVechicleLocationAsync", null);
                throw;
            }
        }

        public async Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync(int limit, string direction, string[] parentTagIds, string[] tagIds)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var _parentTagIds = String.Join(", ", parentTagIds);
                var _tagIds = String.Join(",", tagIds);
               
                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations?limit=" + limit + "&" + direction + "&parentTagIds=" + _parentTagIds + "&tagIds=" + _tagIds);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                VechicleLocationModel _res = JsonConvert.DeserializeObject<VechicleLocationModel>(content);

                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetMostRecentVechicleLocationAsync", null);
                throw;
            }
        }

        public async Task<VechicleLocation> GetMostRecentVechicleLocationByVechicleIdAsync(string vechicleId)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);
                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations?vehicleIds=" + vechicleId);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var _resOut = JsonConvert.DeserializeObject<VechicleLocationModel>(content);
                var _res=_resOut.data.FirstOrDefault().location;
                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetMostRecentVechicleLocationByVechicleIdAsync", null);
                throw;
            }
        }

        public async Task<VechicleLocation> GetMostRecentVechicleLocationByVechicleIdAsync(string vechicleId, string tenantId)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi(tenantId);
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.SamsaraApiKey);
                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations?vehicleIds=" + vechicleId);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var _resOut = JsonConvert.DeserializeObject<VechicleLocationModel>(content);
                var _res=_resOut.data.FirstOrDefault().location;
                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetMostRecentVechicleLocationByVechicleIdAsync", tenantId);
                throw;
            }
        }
        #endregion

        #region Follow a feed of vechicle location

        public async Task<VechicleFollowFeedLocationModel> GetFllowFeedOfVechicleLocationAsync()
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations/feed");

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                VechicleFollowFeedLocationModel _res = JsonConvert.DeserializeObject<VechicleFollowFeedLocationModel>(content);

                return _res;
            }
            catch(Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetFllowFeedOfVechicleLocationAsync", null);
                throw;
            }
        }

        public async Task<VechicleFollowFeedLocationModel> GetFllowFeedOfVechicleLocationAsync(string[] tagIds, string direction, string[] parentTagIds, string[] vehicleIds)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var _parentTagIds = String.Join(", ", parentTagIds);
                var _tagIds = String.Join(",", tagIds);
                var _vehicleIds = String.Join(", ", vehicleIds);
                

                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations/feed?" + direction + "&parentTagIds=" + _parentTagIds + "&tagIds=" + _tagIds + "&vehicleIds=" + _vehicleIds);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                VechicleFollowFeedLocationModel _res = JsonConvert.DeserializeObject<VechicleFollowFeedLocationModel>(content);

                return _res;
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetFllowFeedOfVechicleLocationAsync", null);
                throw;
            }
        }

        #endregion

        #region Get historical vehicle locations
        public async Task<VechicleLocationModel> GetHistoricalVechicleLocationAsync(string startTime, string endTime, string[] tagIds, string direction, string[] parentTagIds, string[] vehicleIds)
        {
            try
            {
                var apiKey = _authenticationService.AuthenticateApi();
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Value);

                var _parentTagIds = String.Join(", ", parentTagIds);
                var _tagIds = String.Join(",", tagIds);
                var _vehicleIds = String.Join(", ", vehicleIds);
                var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/locations/history?startTime=" + startTime + "&endTime=" + endTime + "&tagIds=" + _tagIds + "&" + direction + "&parentTagIds=" + _parentTagIds + "&vehicleIds=" + _vehicleIds);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Data");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                VechicleLocationModel _res = JsonConvert.DeserializeObject<VechicleLocationModel>(content);

                return _res;
            }
            catch(Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetHistoricalVechicleLocationAsync", null);
                throw;
            }
        }

        #endregion

        #region Retrieve a vehicle
        public async Task<VechicleModel> GetVechicleAsync(string vechicleId)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);
            try
            {
               
                Uri uri = new Uri("https://api.samsara.com/fleet/vehicles?id=" +vechicleId);
                HttpResponseMessage response = await _client.GetAsync(uri);
                var jsonString = await response.Content.ReadAsStringAsync();
                var _Data = JsonConvert.DeserializeObject<VechicleModel>(jsonString);
                return _Data.data.Where(x => x.id == vechicleId).Select(x=>new VechicleModel { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CusstomErrorHandler Error = new CusstomErrorHandler(_db);
                Error.WriteError(ex, "VechicleService GetVechicleAsync", null);
                throw new Exception("Cannot retrieve Data");
            }          
        }
        #endregion

        public async Task<VechicleStatusModel> GetMostRecentVechicleStatusAsync(string[] types)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);


            var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/stats?types=" + types);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Data");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            VechicleStatusModel _res = JsonConvert.DeserializeObject<VechicleStatusModel>(content);

            return _res;
        }

        public async Task<VechicleStatusModel> GetFllowFeedOfVechicleStatusAsync(string types)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);

            var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/stats/feed?types=" + types);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Data");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            VechicleStatusModel _res = JsonConvert.DeserializeObject<VechicleStatusModel>(content);

            return _res;
        }

        public async Task<VechicleLocationModel> GetHistoricalVechicleLocationStatusAsync(string type)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);

            var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/stats?types=" + type);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Data");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            VechicleLocationModel _res = JsonConvert.DeserializeObject<VechicleLocationModel>(content);

            return _res;
        }

        public async Task<VechicleLocationModel> GetHistoricalVechicleStatusAsync(string StartTime, string EndTime, string type)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);

            var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/stats/history?startTime=" + StartTime + "&endTime=" + EndTime + "&types=" + type);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Data");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            VechicleLocationModel _res = JsonConvert.DeserializeObject<VechicleLocationModel>(content);

            return _res;
        }

        public Task<VechicleLocationModel> GetMostRecentVechicleLocationAsync(string StartTime, string EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<VechicleLocationModel> GetFllowFeedOfVechicleStatusAsync()
        {
            throw new NotImplementedException();
        } 

        public async Task<BatteryMilliVoltStatusModel> GetMostRecentVechicleStatusForBatteryMilliVoltAsync(string types)
        {
            var apiKey = _authenticationService.AuthenticateApi();
            _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey.Value);

            var httpResponse = await _client.GetAsync("https://api.samsara.com/fleet/vehicles/stats?types=" + types);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Data");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            BatteryMilliVoltStatusModel _res = JsonConvert.DeserializeObject<BatteryMilliVoltStatusModel>(content);

            return _res;
        }         
    }
}
