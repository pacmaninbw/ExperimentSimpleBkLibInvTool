using System.Data;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using MySql.Data.MySqlClient;

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

        public uint ID
        {
            get { return GetKeyValue(); }
            set { SetKeyValue(value); }
        }

        public AuthorModel()
        {
            errorWasReported = false;

            InitializeSqlCommands();
        }

        public AuthorModel(string firstName, string lastName, string middleName=null, string yearOfBirth=null, string yearOfDeath=null, uint iD=0)
        {
            errorWasReported = false;

            InitializeSqlCommands();

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

            ID = iD;
        }

        private void InitializeSqlCommands()
        {
            _addSqlCommandParameter("ID", "idAuthors", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("Last Name", "LastName", "authorLastName", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("First Name", "FirstName", "authorFirstName", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Middle Name", "MiddleName", "authorMiddleName", MySqlDbType.String, false, ParameterDirection.Input);
            _addSqlCommandParameter("Year of Birth", "YearOfBirth", "dob", MySqlDbType.String, false, ParameterDirection.Input);
            _addSqlCommandParameter("Year of Death", "YearOfDeath", "dod", MySqlDbType.String, false, ParameterDirection.Input);
            _addSqlCommandParameter("Primary Key", "primaryKey", "primaryKey", MySqlDbType.UInt32, false, ParameterDirection.Output);
        }

        private void SetFirstName(string textBoxInput)
        {
            if (textBoxInput == null || textBoxInput.Length < 1)
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
            if (textBoxInput == null || textBoxInput.Length < 1)
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
