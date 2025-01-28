using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WellAI.Advisor.Function.MailQueue.Data;
using WellAI.Advisor.Function.MailQueue.Models;
using WellAI.Advisor.Function.MailQueue.Repository.IRepository;
using Microsoft.Extensions.Logging;

namespace WellAI.Advisor.Function.MailQueue
{
    //Phase II Changes - 03/15/2021
    public class CommonRepository: ICommonRepository
    {
        private readonly WellAIDataContext _db;
        public CommonRepository(WellAIDataContext db)
        {
            _db = db;
        }

        public void ResetUserSessions(ILogger log)
        {
            try
            {
                log.LogInformation($"ResetUserSessions method start : {DateTime.Now}");
                var userSessions = (from usr in _db.UserSession
                                    select new UserSessionViewModel
                                    {
                                        SessionId = usr.SessionId,
                                        UserId = usr.UserId,
                                        UserName = usr.UserName,
                                        SessionInterval = (int)((TimeSpan)(DateTime.Now - usr.SessionTimeStamp)).Minutes
                                    }).ToList();

                if (userSessions != null)
                {
                    log.LogInformation($"User Sessions above 4 mins : {DateTime.Now}");
                   
                    var userSessionIds = (from usr in userSessions
                                          where (int)usr.SessionInterval > 4
                                          select usr.SessionId
                                         ).ToList();


                    if (userSessionIds != null)
                    {
                        log.LogInformation($"Update User Sessions : {DateTime.Now}");
                        if (userSessionIds.Count > 0)
                        {
                            var resetSessions = _db.UserSession.Where(x => userSessionIds.Contains(x.SessionId)).ToList();

                            if (resetSessions != null)
                            {
                                foreach (var session in resetSessions)
                                {
                                    _db.UserSession.Remove(session);
                                    _db.SaveChanges();
                                }
                            }
                        }
                    }
                    
                }

                //Clear (Staff Users list) session
                log.LogInformation($"Reset StaffUserSessions method start : {DateTime.Now}");
                var staffUserSessions = (from usr in _db.UserSession
                                    select new StaffUserSessionViewModel
                                    {
                                        SessionId = usr.SessionId,
                                        UserId = usr.UserId,
                                        UserName = usr.UserName,
                                        SessionInterval = (int)((TimeSpan)(DateTime.Now - usr.SessionTimeStamp)).Minutes
                                    }).ToList();

                if (staffUserSessions != null)
                {
                    log.LogInformation($"Staff User Sessions above 4 mins : {DateTime.Now}");

                    var staffUserSessionIds = (from usr in staffUserSessions
                                               where (int)usr.SessionInterval > 4
                                                 select usr.SessionId
                                              ).ToList();


                    if (staffUserSessionIds != null)
                    {
                        log.LogInformation($"Update Staff User Sessions : {DateTime.Now}");
                        if (staffUserSessionIds.Count > 0)
                        {
                            var resetStaffSessions = _db.StaffUserSession.Where(x => staffUserSessionIds.Contains(x.SessionId)).ToList();

                            if (resetStaffSessions != null)
                            {
                                foreach (var session in resetStaffSessions)
                                {
                                    _db.StaffUserSession.Remove(session);
                                    _db.SaveChanges();
                                }
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                log.LogInformation($"ResetUserSessions error : {ex.Message}");
            }
        }
    }
}
