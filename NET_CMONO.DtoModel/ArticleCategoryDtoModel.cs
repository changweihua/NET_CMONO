using System;
using System.ComponentModel.DataAnnotations;

namespace NET_CMONO.DtoModel
{
    #region Edition  DTO Model

    public class ArticleCategoryEditModel
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryRemark { get; set; }

        public string CategoryIcon { get; set; }

        public int CategoryProject { get; set; }

        public int CategoryArticle { get; set; }

    }

    #endregion

    [Flags]
    public enum ArticleCategoryFor
    {
        PROJECT = 1,
        ARTICLE
    }

}