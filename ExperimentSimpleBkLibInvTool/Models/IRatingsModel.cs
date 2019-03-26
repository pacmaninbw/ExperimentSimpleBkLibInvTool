namespace pacsw.BookInventory.Models
{
    public interface IRatingsModel
    {
        string AmazonRating { get; set; }
        string GoodReadsRating { get; set; }
        string MyRating { get; set; }
    }
}