using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class SeriesModel : DataTableItemBaseModel
    {
        private AuthorModel _author;
        private uint _authorId;

        public AuthorModel Author {
            get { return _author; }
            set {
                _author = value;
                _authorId = _author.AuthorId;
                SetParameterValue("First Name", _author.FirstName);
                SetParameterValue("Last Name", _author.LastName);
            }
        }

        public string Title {
            get { return GetParameterValue("Series Title"); }
            set { SetParameterValue("Series Title", value); }
        }

        public uint AuthorId { get { return _authorId; } }

        public SeriesModel(AuthorModel author = null, string title = null)
            : base(((App)Application.Current).Model.SeriesTable)
        {
            Author = author;
            Title = title;
            IsModified = false;       // Initialization is not modification.
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;
            if (_author == null)
            {
                dataIsValid = false;
            }
            else
            {
                dataIsValid = _author.IsValid;
                if (!GetParameterIsValid("Series Title"))
                {
                    string errorMsg = "Add Series error: Missing title of series.";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }
            }
            return dataIsValid;
        }

        public override bool AddToDb() => ((App)Application.Current).Model.SeriesTable.AddSeries(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.SeriesTable.UpdateSeries(this);
    }
}
