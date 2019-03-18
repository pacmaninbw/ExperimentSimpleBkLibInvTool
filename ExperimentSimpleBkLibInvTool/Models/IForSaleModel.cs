namespace pacsw.BookInventory.Models.BookInfo.ForSale
{
    public interface IForSaleModel
    {
        string AskingPrice { get; set; }

        string EstimatedValue { get; set; }

        bool IsForSale { get; set; }
    }
}