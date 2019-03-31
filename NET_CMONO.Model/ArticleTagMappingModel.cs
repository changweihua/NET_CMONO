using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Article_Tag")]
    public class ArticleTagMappingModel : IdentityKeyEntity<int>
    {
        public int ArticleTagID { get; set; }
        public int ArticleID { get; set; }
    }
}