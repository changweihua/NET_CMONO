using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NET_CMONO.Model
{
    [Table("T_Article")]
    public class ArticleModel : IdentityKeyEntity<int>
    {
        public string ArticleTitle { get; set; }
        public string ArticleSummary { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleContent { get; set; }

        public string ArticleAuthor { get; set; }

        public DateTime? ArticlePublishDate { get; set; }

        // [ForeignKey("CreatedBy")]
        // public virtual EmployeeModel CreatedUser { get; set; }

        // [ForeignKey("UpdatedBy")]
        // public virtual EmployeeModel UpdatedUser { get; set; } 
    }
}