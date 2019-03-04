namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public interface IBookInfoModel
    {
        uint AuthorId { get; }

        uint BookID { get; }

        uint CategoryId { get; }

        string Format { get; set; }

        uint FormatId { get; }

        string Genre { get; set; }

        uint SeriesKey { get; }

        string Title { get; set; }

        uint TitleId { get; }
    }
}