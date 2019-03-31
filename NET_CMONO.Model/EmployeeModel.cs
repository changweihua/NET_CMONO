using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Employee")]
    public class EmployeeModel : StaffKeyEntity<string>
    {
        public int Id { get; set; }
        [Display(Name = "ADName")]
        public string ADName { get; set; }
        [Display(Name = "中文名")]
        public string LocalName { get; set; }
        [Display(Name = "英文名")]
        public string UserName { get; set; }
        [Display(Name = "手机号码")]
        public string CellPhone { get; set; }
        [Display(Name = "座机")]
        public string TelePhone { get; set; }
        [Display(Name = "邮箱")]
        public string ContactEmail { get; set; }
        [Display(Name = "聘用日期")]
        public DateTime? HireDate { get; set; }
        [Display(Name = "入职时间")]
        public DateTime? PromotionDate { get; set; }
        [Display(Name = "离职时间")]
        public DateTime? DimissionDate { get; set; }
        [Display(Name = "雇佣形式")]
        public int EmployeeStatus { get; set; }
        [Display(Name = "雇佣形式")]
        public int EmployeeType { get; set; }
        [Display(Name = "性别")]
        public int Gender { get; set; }
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }
    }

}