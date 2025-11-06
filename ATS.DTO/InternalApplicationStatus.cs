using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{

    public enum InternalApplicationStatus
    {

        Unset = 0,


        New_Applicant = 1,


        Not_in_Consideration = 2,


        Attempted_1 = 3,


        Attempted_2 = 4,


        Attempted_3 = 5,


        Attempted_4 = 6,


        Attempted_5 = 7,


        Attempted_6 = 8,


        Unable_to_Contact = 9,


        Declined_Availability = 10,


        Declined_Pay_Scale = 11,


        Declined_Location = 12,


        Declined_Not_eligible_for_rehire = 13,


        Rescheduled = 14,


        First_Round_Interview = 15,


        No_Call_No_Show = 16,


        Checked_In = 17,


        CSI_Interview = 18,


        CSI_Failed = 19,


        Offer_Given = 20,


        Offer_Declined = 21,


        Hired = 22,


        Not_Hired = 24,


        Failed_Testing = 25,


        Declined_Position = 26,


        Medical_exam_pending = 27,


        Medical_exam_in_progress = 28,


        Medical_exam_pass = 29,


        Medical_exam_fail = 30,


        Background_check_pending = 31,


        Background_check_in_progress = 32,


        Background_check_pass = 33,


        Background_check_fail = 34,


        Rehire_Eligibility_Check_Pending = 35,


        Applicant_Assessment__Complete_Poor_Fit = 36,


        Applicant_Assessment__Complete_Good_Fit = 37,


        Applicant_Assessment__Complete_Great_Fit = 38

    }
}
