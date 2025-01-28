
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Areas.OperatingCompany.Models
{
    public class ActivityViewService
    {
        private ISession _session;

        public ISession Session { get { return _session; } }

        public ActivityViewService()
        {
            _session = WellAIAppContext.Current.Session;
        }

        /*public Task<List<ActivityViewModel>> GetAll()
        {
            var result = d
        }

        public async Task Insert(ActivityViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {
                    using (var db = GetContext())
                    {
                        if (string.IsNullOrEmpty(task.Title))
                        {
                            task.Title = "";
                        }

                        var entity = task.ToEntity();

                        db.Tasks.Add(entity);
                        db.SaveChanges();

                        task.TaskID = entity.TaskID;
                    }
            }
        }

        public async Task Update(ActivityViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {
                using (var db = GetContext())
                {
                    if (string.IsNullOrEmpty(task.Title))
                    {
                        task.Title = "";
                    }

                    var entity = task.ToEntity();
                    db.Tasks.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public async Task Delete(ActivityViewModel task, ModelStateDictionary modelState)
        {
            using (var db = GetContext())
            {
                var entity = task.ToEntity();
                db.Tasks.Attach(entity);

                var recurrenceExceptions = db.Tasks.Where(t => t.RecurrenceID == task.TaskID);

                foreach (var recurrenceException in recurrenceExceptions)
                {
                    db.Tasks.Remove(recurrenceException);
                }

                db.Tasks.Remove(entity);
                db.SaveChanges();
            }
        }*/

        private bool ValidateModel(ActivityViewModel task, ModelStateDictionary modelState)
        {
            if (task.Start > task.End)
            {
                modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
                return false;
            }

            return true;
        }
    }
}
