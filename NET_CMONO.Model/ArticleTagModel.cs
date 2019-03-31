using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_ArticleTag")]
    public class ArticleTagModel : IdentityKeyEntity<int>
    {
        public string TagName { get; set; }

        public string TagGroup { get; set; }
    }
}