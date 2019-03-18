namespace pacsw.BookInventory.Models.BookInfo
{
    public interface IBookInfoModel
    {
        uint GenreId { get; set; }

        uint SeriesId { get; set; }

        uint TitleId { get; set; }

        uint FormatId { get; set; }

        uint AuthorId { get; set; }

        uint BookID { get; }
    }
}