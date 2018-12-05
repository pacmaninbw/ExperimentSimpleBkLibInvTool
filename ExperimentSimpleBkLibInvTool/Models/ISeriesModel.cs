using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public interface ISeriesModel
    {
        AuthorModel Author { get; set; }

        uint AuthorId { get; }

        string Title { get; set; }
    }
}