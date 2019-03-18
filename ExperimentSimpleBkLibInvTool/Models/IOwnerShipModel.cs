namespace pacsw.BookInventory.Models.BookInfo.Ownned
{
    public interface IOwnerShipModel
    {
        bool IsOwned { get; set; }
        bool IsWishListed { get; set; }
    }
}