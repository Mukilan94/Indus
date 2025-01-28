
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class UserRole
    {
        [ScaffoldColumn(false)]
        public int Id;
        public string Title;

        /*TODO refactor: change these many separate permissions to array of permissions List<UserPermissions>, 
         * which are initialized from database for specific role*/
        public bool Reading;
        public bool ManageSite;
        public bool CompanyReading;
        public bool CompanyManaging;
        public bool Administration;
        public bool ProjectReading;
        public bool ProjectManage;
        
    }

    public class UserPermission
    {
        [ScaffoldColumn(false)]
        public int Id;
        public string Title;

        /*TODO refactor: change these many separate actions to array of actions List<UserAction>, 
         * which are initialized from database for specific Permission*/
        public bool ViewDashboard;
        public bool ViewActivityView;
        public bool ViewUpcomingProjects;
        public bool EditUpcomingProjects;
        public bool ViewOngoingProjects;
        public bool ViewTracker;
        public bool ViewWellMetrics;
        public bool ViewDataManager;
        public bool ViewTicketHistory;
        public bool CanCommunicate;
        public bool DoingDocumentManager;
        public bool EditPaymentMethods;
        public bool ViewBillingHistory;
        public bool ManageUsers;
        public bool ManagePermissions;
        public bool ManageRoles;
    }

    public class UserAction
    {
        [ScaffoldColumn(false)]
        public int Id;
        public string Title;
        public bool IsActive;
    }
}
