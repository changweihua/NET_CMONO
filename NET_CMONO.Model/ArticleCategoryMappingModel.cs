using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Article_Category")]
    public class ArticleCategoryMappingModel : IdentityKeyEntity<int>
    {
        public int ArticleCategoryID { get; set; }
        public int ArticleID { get; set; }
    }
}