using pacsw.BookInventory.Models.Author;

namespace pacsw.BookInventory.Models.Series
{
    public interface ISeriesModel
    {
        AuthorModel Author { get; set; }

        uint AuthorId { get; }

        string Title { get; set; }
    }
}