namespace pacsw.BookInventory.Models.BookInfo.PublishInfo
{
    public interface IPublishInfoModel
    {
        string CopyRight { get; set; }

        string Edition { get; set; }

        string ISBNumber { get; set; }

        bool OutOfPrint { get; set; }

        string Printing { get; set; }

        string Publisher { get; set; }
    }
}