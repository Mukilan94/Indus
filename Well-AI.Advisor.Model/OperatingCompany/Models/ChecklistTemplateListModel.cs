using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class ChecklistTemplateListModel
    {
        public int count { get; set; }
        public ChecklistTemplateModel checklist { get; set; }
    }

    public class ChecklistTemplateTaskListModel
    {
        public int count { get; set; }
        public List<ChecklistTaskTemplateModel> checklist { get; set; }
    }
}
