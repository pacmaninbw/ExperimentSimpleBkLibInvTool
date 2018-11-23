using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public interface ISeriesModel
    {
        AuthorModel Author { get; }
        string SeriesTitle { get; }

        void SetAuthor(AuthorModel author);
        void SetTitle(string seriesTitle);
    }
}