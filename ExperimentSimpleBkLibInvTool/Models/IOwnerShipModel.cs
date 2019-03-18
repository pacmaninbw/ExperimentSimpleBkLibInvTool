namespace pacsw.BookInventory.Models
{
    public interface IOwnerShipModel
    {
        bool IsOwned { get; set; }
        bool IsWishListed { get; set; }
    }
}