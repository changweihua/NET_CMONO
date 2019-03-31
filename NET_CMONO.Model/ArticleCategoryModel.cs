using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_ArticleCategory")]
    public class ArticleCategoryModel : IdentityKeyEntity<int>
    {
        public string CategoryName { get; set; }

        public string CategoryGroup { get; set; }

         public string CategoryRemark { get; set; }

        public string CategoryIcon { get; set; }
    }
}