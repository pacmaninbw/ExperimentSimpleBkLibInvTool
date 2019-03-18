using System;

namespace pacsw.BookInventory.Models.BookInfo.PuchaseInfo
{
    public interface IPuchaseInfoModel
    {
        string ListPrice { get; set; }

        string PaidPrice { get; set; }

        DateTime PurchaseDate { get; set; }

        string Vendor { get; set; }
    }
}