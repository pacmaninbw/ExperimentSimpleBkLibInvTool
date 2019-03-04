using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public interface IBookModel
    {
        IAuthorModel AuthorInfo { get; set; }

        string Category { get; set; }

        string Condition { get; set; }

        string FirstName { get; set; }

        string Format { get; set; }

        IForSaleModel ForSale { get; set; }

        string LastName { get; set; }

        IOwnerShipModel Owned { get; set; }

        IPublishInfoModel PublishInfo { get; set; }

        IPuchaseInfoModel PuchaseInfo { get; set; }

        IRatingsModel Ratings { get; set; }

        ISeriesModel SeriesInfo { get; set; }

        IBookInfoModel Book { get; set; }

        string Status { get; set; }

        string Title { get; set; }

        bool AddBookToLibrary();
    }
}