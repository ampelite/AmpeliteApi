using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class HrEmployee
    {
        public int Id { get; set; }
        public int SEmpId { get; set; }
        public string SEmpOrgLevel2 { get; set; }
        public string SEmpDepartment { get; set; }
        public string SEmpNamePrefix { get; set; }
        public string SEmpFirstName { get; set; }
        public string SEmpLastName { get; set; }
        public string SEmpEngNamePrefix { get; set; }
        public string SEmpEngFirstName { get; set; }
        public string SEmpEngLastName { get; set; }
        public string SEmpNickName { get; set; }
        public string SEmpRegisterAddr1 { get; set; }
        public int? SEmpRegisterZip { get; set; }
        public string SEmpRegisterPhone { get; set; }
        public string SEmpPresentAddr1 { get; set; }
        public int? SEmpPresentZip { get; set; }
        public string SEmpPresentPhone { get; set; }
        public string SEmpMobilePhone { get; set; }
        public string SEmpEmail { get; set; }
        public string SEmpUserName { get; set; }
        public string SEmpPassword { get; set; }
        public string SEmpGender { get; set; }
        public string SEmpRace { get; set; }
        public string SEmpNationality { get; set; }
        public string SEmpReligion { get; set; }
        public int? NEmpHeight { get; set; }
        public int? NEmpWeight { get; set; }
        public DateTime? TEmpBirthDate { get; set; }
        public long? SEmpIdcardNumber { get; set; }
        public string SEmpIdcardIssuePlace { get; set; }
        public DateTime? TEmpIdcardIssueDate { get; set; }
        public DateTime? TEmpIdcardExpireDate { get; set; }
        public int? SEmpTimeCardId { get; set; }
        public int? DEmpLeaveQuota1 { get; set; }
        public int? DEmpLeaveQuota2 { get; set; }
        public int? DEmpLeaveQuota3 { get; set; }
        public int? DEmpLeaveQuota4 { get; set; }
        public int? DEmpLeaveQuota5 { get; set; }
        public int? DEmpLeaveQuota6 { get; set; }
        public int? DEmpLeaveQuota7 { get; set; }
        public int? DEmpLeaveQuota8 { get; set; }
        public int? DEmpLeaveQuota9 { get; set; }
        public int? DEmpLeaveQuota10 { get; set; }
        public int? IEmpWorkStatus { get; set; }
        public DateTime? TEmpStartWorkDate { get; set; }
        public DateTime? TEmpStartPermanentWorkDate { get; set; }
        public DateTime? TEmpQuitWorkDate { get; set; }
        public int? IEmpQuitWorkType { get; set; }
        public int? NEmpPayPeriod { get; set; }
        public int? IYtdlastClosePeriod { get; set; }
        public DateTime? TYtdlastCloseDate { get; set; }
        public byte[] EmpImage { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
