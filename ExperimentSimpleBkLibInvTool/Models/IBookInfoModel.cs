namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public interface IBookInfoModel
    {
        string Genre { get; set; }

        string Series { get; set; }

        string Title { get; set; }

        string Format { get; set; }

        uint AuthorId { get; }

        uint BookID { get; }

        uint CategoryId { get; }

        uint FormatId { get; }

        uint SeriesKey { get; }

        uint TitleId { get; }
    }
}