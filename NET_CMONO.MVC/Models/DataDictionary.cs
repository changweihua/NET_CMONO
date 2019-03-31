namespace NET_CMONO.MVC.Models
{
    public enum SiteModule
    {
        META,
        //兼容性代码
        COMPATIE,
        STYLE,
        CC
    }


    public enum Page
    {
        HEADER,
        BODY,
        NAVIER,
        FOOTER,
        HOLDER,
        FLOATING_BAR,
        CC,
        CC_DONATE
    }

    public enum CC_LICENSE
    {
        PUBLIC,
        CC4_INTERNATIONAL,
        CC3_CHINESE
    }

    public enum ConfigEditionMode
    {
        SINGLE_LINE = 1,
        MULTI_LINE,
        DATE,
        TIME,
        DATETIME,
        FILE,
        COMBO,
        TREE,
        SELECT,
        RADIO,
        CHECKBOX,
        PASSWORD,
        PLAIN_TEXT
    }

    #region NCM

    public enum LayoutModule
    {
        FOOTER,
        NAV_RIGHT,
        NAV_LEFT
    }

    #endregion

}