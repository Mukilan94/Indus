
using System.Collections.Generic;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class UserRole
    {
        
        ////public int Id;
        ////public string Title;

        /*TODO refactor: change these many separate permissions to array of permissions List<UserPermissions>, 
         * which are initialized from database for specific role*/
        public bool Reading;
        public bool ManageSite;
        public bool CompanyReading;
        public bool CompanyManaging;
        public bool Administration;
        public bool ProjectReading;
        public bool ProjectManage;

        // required prop
        public string Id;
        public string Title;
        /// <summary>
        /// Permissions
        /// </summary>
        public List<UserAction> Permissions;

    }

    public class UserPermission
    {
       
        //public int Id;
        //public string Title;

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
        // required prop
        public int? Id;
        public string Title;
        /// <summary>
        /// Components
        /// </summary>
        public List<UserAction> Components;


    }

    public class UserAction
    {       
        public int Id;
        public string Title;
        public bool IsActive;
    }

}
