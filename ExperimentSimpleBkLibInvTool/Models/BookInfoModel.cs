using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookInfoModel : DataTableItemBaseModel, IBookInfoModel
    {
        public BookInfoModel(uint genre = 0, uint titleId = 0, uint authorId = 0, uint seriesId = 0, uint formatId = 0)
            : base(((App)Application.Current).Model.BookInfoTable)
        {
            GenreId = genre;
            TitleId = titleId;
            AuthorId = AuthorId;
            SeriesId = seriesId;
            FormatId = formatId;
        }

        public uint BookID { get { return GetParameterKValue("Book Key"); } }

        public uint GenreId {
            get { return GetParameterKValue("Genre Id"); }
            set { SetParameterValue("Genre Id", value); }
        }

        public uint SeriesId
        {
            get { return GetParameterKValue("Series Id"); }
            set { SetParameterValue("Series Id", value); }
        }

        public uint FormatId {
            get { return GetParameterKValue("Format Id"); }
            set { SetParameterValue("Format Id", value); }
        }

        public uint AuthorId {
            get { return GetParameterKValue("Author Id"); }
            set { SetParameterValue("Author Id", value); }
        }

        public uint TitleId {
            get { return GetParameterKValue("Title Id"); }
            set { SetParameterValue("Title Id", value); }
        }

        public override bool AddToDb()
        {
           return ((App)Application.Current).Model.BookInfoTable.AddBookInfo(this);
        }

        protected override bool _dataIsValid()
        {
            // Check the parameter types
            bool isValid = _defaultIsValid();

            if (!isValid)
            {
                return isValid;
            }

            // Check for required fields
            if (GetParameterKValue("Author Id") < 1)
            {
                MessageBox.Show("Author has not been selected", "Missing Author selection", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (GetParameterKValue("Title Id") < 1)
            {
                MessageBox.Show("The title has not been entered", "Missing title", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            return isValid;
        }

    }
}
