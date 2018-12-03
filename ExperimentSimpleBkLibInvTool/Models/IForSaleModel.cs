namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public interface IForSaleModel
    {
        double AskingPrice { get; set; }

        double EstimatedValue { get; set; }

        bool IsForSale { get; set; }
    }
}