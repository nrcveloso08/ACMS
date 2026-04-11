namespace ATS.Web.Extensions
{
    public class AuthorizationRoles
    {
        public const string JiraPortalUser = "KITT-JiraUsers, 247users";

        /*TO DO: change 247users to AD group name once the appropriate AD group has been set-up.
        * For the interim all authenticated users on master realm are authorized to access all sections on KITT
        */
        public const string KittAdmin = "KITT-ConfigAdmin";
        public const string ATSConfigAdmin = "ATS-ConfigAdmin";
        public const string BaxterUser = "247users";
        public const string Recruitment = "247users; ATS-RequisitionRequest-User; ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ServiceDesk = "247users";
        public const string ReportUser = "247users";
        public const string SIDDReportUser = "247users";


        public const string ATSUsers = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users; ATS-Prescreening-Manager; ATS-Prescreening-User; ATS-Ops-Users";
        public const string ATS_HR_Users = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users";

        public const string ATS_Dashboard_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users; ATS-Prescreening-Manager; ATS-Prescreening-User";
        public const string ATS_Dashboard_Write = "ATS-Prescreening-Manager; ATS-Prescreening-User; ATS-Recruiter-Manager; ATS-Recruiter-User";

        public const string ATS_ObeyaBoard_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users; ATS-Inventory-Users; ATS-Training-Users";
        public const string ATS_Obeya_Widget_Write = "ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ATS_Obeya_Wave_Note_Write = "ATS-Recruiter-Manager'";

        public const string ATS_CSISchedules_Read = "ATS-Ops-Users; ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ATS_CSISchedules_Write = "ATS-Ops-Users; ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ATS_CSI_Applicants_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-Ops-Users";

        public const string ATS_CandidateSearch = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users; ATS-Prescreening-Manager";

        public const string ATS_HiringRoster_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users";
        public const string ATS_CSI_Result_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ATS_HiringRoster_Write = "ATS-Recruiter-Manager; ATS-Recruiter-User";

        public const string ATS_ApplicantDashboard = "ATS-Prescreening-Manager; ATS-Prescreening-User; ATS-Recruiter-Manager; ATS-Recruiter-User";
        public const string ATS_ApplicantDetails_Read = "ATS-Recruiter-Manager; ATS-Recruiter-User; ATS-HR-Users; ATS-Prescreening-Manager";
        public const string ATS_Reports = "ATS-Reports-Reader; ATS-Prescreening-Manager; ATS-Prescreening-User";
        public const string ATS_Config = "ATS-Prescreening-Manager; ATS-Recruiter-Manager";
        public const string ATS_FCRA = "ATS-FCRA-Users";

        public const string Commboard_Portal = "commboard-users";
        public const string KITT_IR_Users = "RS-IR-Control-Manager";

        public const string IVR_Allocator_Users = "247users";

        public const string Requisition_Request = "KITT-Admin-RequisitionRequest";

        public const string KITT_Admin_Channel = "KITT-Admin-Channel";

        public const string KITT_SuperPunch_Users = "KITT-SuperPunch-Users";
        public const string KITT_Recruiting_Users = "KITT-Recruiting-Users";
        public const string KITT_Telecom_Users = "KITT-Telecom-Users";

        public const string KITT_IT_Users = "KITT-IT-Users";

        public const string KITT_User = "247users";
    }
}
