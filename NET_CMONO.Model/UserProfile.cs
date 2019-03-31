using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_UserProfile")]
    public class UserProfileModel : IdentityKeyEntity<int>
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Avator { get; set; }
    }
}