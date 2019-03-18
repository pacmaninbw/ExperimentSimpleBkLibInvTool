namespace pacsw.BookInventory.Models
{
    public interface IForSaleModel
    {
        string AskingPrice { get; set; }

        string EstimatedValue { get; set; }

        bool IsForSale { get; set; }
    }
}