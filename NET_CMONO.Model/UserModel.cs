using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_User")]
    public class UserModel : IdentityKeyEntity<int>
    {
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }

        [ForeignKey("UserID")]
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
    }
}