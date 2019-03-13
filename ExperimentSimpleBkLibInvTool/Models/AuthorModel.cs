using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorModel : DataTableItemBaseModel, IAuthorModel
    {
        private bool errorWasReported;

        public string FirstName {
            get { return GetParameterValue("First Name"); }
            set { SetFirstName(value); }
        }

        public string MiddleName {
            get { return GetParameterValue("Middle Name"); }
            set { SetParameterValue("Middle Name", value); }
        }

        public string LastName {
            get { return GetParameterValue("Last Name"); }
            set { SetLastName(value); }
        }

        public string YearOfBirth {
            get { return GetParameterValue("Year of Birth"); }
            set { SetParameterValue("Year of Birth", value); }
        }

        public string YearOfDeath {
            get { return GetParameterValue("Year of Death"); }
            set { SetParameterValue("Year of Death", value); }
        }

        public uint AuthorId {
            get { return GetParameterKValue("ID"); }
            private set { SetParameterValue("ID", value); }
        }

        public AuthorModel()
            : base(((App)Application.Current).Model.AuthorTable)
        {
            errorWasReported = false;
            AuthorId = 0;
        }

        public AuthorModel(string firstName, string lastName, string middleName=null, string yearOfBirth=null, string yearOfDeath=null)
            : base(((App)Application.Current).Model.AuthorTable)
        {
            errorWasReported = false;
            AuthorId = 0;

            FirstName = firstName;
            LastName = lastName;

            if (!string.IsNullOrEmpty(middleName))
            {
                MiddleName = middleName;
            }

            if (!string.IsNullOrEmpty(yearOfBirth))
            {
                YearOfBirth = yearOfBirth;
            }

            if (!string.IsNullOrEmpty(yearOfDeath))
            {
                YearOfDeath = yearOfDeath;
            }
        }

        public AuthorModel(string idAuthor, string firstName, string lastName, string middleName = null, string yearOfBirth = null, string yearOfDeath = null)
            : base(((App)Application.Current).Model.AuthorTable)
        {
            errorWasReported = false;

            uint IdAuthor;
            uint.TryParse(idAuthor, out IdAuthor);
            AuthorId = IdAuthor;

            FirstName = firstName;
            LastName = lastName;

            if (!string.IsNullOrEmpty(middleName))
            {
                MiddleName = middleName;
            }

            if (!string.IsNullOrEmpty(yearOfBirth))
            {
                YearOfBirth = yearOfBirth;
            }

            if (!string.IsNullOrEmpty(yearOfDeath))
            {
                YearOfDeath = yearOfDeath;
            }
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.AuthorTable.AddAuthor(this);
        }

        private void SetFirstName(string textBoxInput)
        {
            if (string.IsNullOrEmpty(textBoxInput))
            {
                string errorMsg = "The first name of the author is a required field!";
                MessageBox.Show(errorMsg);
                errorWasReported = true;
            }
            else
            {
                SetParameterValue("First Name", textBoxInput);
            }
        }

        private void SetLastName(string textBoxInput)
        {
            if (string.IsNullOrEmpty(textBoxInput))
            {
                string errorMsg = "The last name of the author is a required field!";
                MessageBox.Show(errorMsg);
                errorWasReported = true;
            }
            else
            {
                SetParameterValue("Last Name", textBoxInput);
            }
        }

        protected override bool _dataIsValid()
        {
            bool isValid = _defaultIsValid();

            if (isValid)
            {
                return isValid;
            }

            isValid = GetParameterIsValid("First Name");
            if (isValid)
            {
                isValid = GetParameterIsValid("Last Name");
            }

            if (!isValid && !errorWasReported)
            {
                string errorMsg = "Add Series error: The first and last names of the author are required fields";
                MessageBox.Show(errorMsg);
            }

            return isValid;
        }
    }
}
