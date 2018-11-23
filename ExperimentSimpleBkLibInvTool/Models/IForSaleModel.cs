namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    interface IForSaleModel
    {
        double AskingPrice { get; set; }
        double EstimatedValue { get; set; }
        bool IsForSale { get; set; }
    }
}