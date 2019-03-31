using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Menu")]
    public class MenuModel : IdentityKeyEntity<int>
    {

        public string MenuName { get; set; }

        public string MenuLink { get; set; }

        public string MenuController { get; set; }

        public string MenuAction { get; set; }

        public string MenuGroup { get; set; }

        // [ForeignKey("CreatedBy")]
        // public virtual EmployeeModel CreatedUser { get; set; }

        // [ForeignKey("UpdatedBy")]
        // public virtual EmployeeModel UpdatedUser { get; set; }
    }
}