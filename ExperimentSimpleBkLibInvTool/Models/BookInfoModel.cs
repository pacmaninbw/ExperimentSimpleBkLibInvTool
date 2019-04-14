using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class BookInfoModel : DataTableItemBaseModel
    {
        public BookInfoModel(uint genre = 0, uint titleId = 0, uint authorId = 0, uint seriesId = 0, uint formatId = 0)
            : base(((App)Application.Current).Model.BookInfoTable)
        {
            BookID = 0;
            GenreId = genre;
            TitleId = titleId;
            AuthorId = authorId;
            SeriesId = seriesId;
            FormatId = formatId;
            Modified = false;       // Initialization is not modification.
        }

        public BookInfoModel(uint bookId, uint genre = 0, uint titleId = 0, uint authorId = 0, uint seriesId = 0, uint formatId = 0)
            : base(((App)Application.Current).Model.BookInfoTable)
        {
            BookID = bookId;
            GenreId = genre;
            TitleId = titleId;
            AuthorId = authorId;
            SeriesId = seriesId;
            FormatId = formatId;
            Modified = false;       // Initialization is not modification.
        }

        public uint BookID {
            get { return GetParameterKValue("ID"); }
            private set { SetParameterValue("ID", value); }
        }

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
            bool wasAdded = ((App)Application.Current).Model.BookInfoTable.AddBookInfo(this);
            BookID = ((App)Application.Current).Model.BookInfoTable.NewKeyValue;

            return wasAdded;
        }

        public override bool DbUpdate()
        {
            throw new System.NotImplementedException();
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
            if (AuthorId < 1)
            {
                MessageBox.Show("Author has not been selected", "Missing Author selection", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (TitleId < 1)
            {
                MessageBox.Show("The title has not been entered", "Missing title", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (GenreId < 1)
            {
                MessageBox.Show("Genre has not been selected", "Missing Genre selection", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (FormatId < 1)
            {
                MessageBox.Show("Format has not been selected", "Missing Format selection", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            return isValid;
        }

    }
}
