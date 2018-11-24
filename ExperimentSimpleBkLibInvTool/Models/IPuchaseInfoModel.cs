using System;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo
{
    public interface IPuchaseInfoModel
    {
        double ListPrice { get; set; }
        double PaidPrice { get; set; }
        DateTime PurchaseDate { get; set; }
        string Vendor { get; set; }

        int getBookId();
    }
}