namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings
{
    public interface IRatingsModel
    {
        string AmazonRating { get; set; }
        string GoodReadsRating { get; set; }
        uint ID { get; }
        string MyRating { get; set; }
    }
}