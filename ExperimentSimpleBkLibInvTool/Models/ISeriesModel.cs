namespace pacsw.BookInventory.Models
{
    public interface ISeriesModel
    {
        AuthorModel Author { get; set; }

        uint AuthorId { get; }

        string Title { get; set; }
    }
}