namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo
{
    public interface IPublishInfoModel
    {
        string CopyRight { get; set; }
        int Edition { get; set; }
        string ISBNumber { get; set; }
        bool OutOfPrint { get; set; }
        int Printing { get; set; }
        string Publisher { get; set; }
    }
}