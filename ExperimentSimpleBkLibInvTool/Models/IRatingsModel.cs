namespace pacsw.BookInventory.Models
{
    public interface IRatingsModel
    {
        string AmazonRating { get; set; }
        string GoodReadsRating { get; set; }
        uint ID { get; }
        string MyRating { get; set; }
    }
}