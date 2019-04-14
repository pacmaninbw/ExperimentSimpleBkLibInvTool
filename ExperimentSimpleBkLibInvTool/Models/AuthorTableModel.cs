using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class AuthorTableModel : CDataTableModel
    {
        private int AuthorIDColumnIndex;
        private int LastNameColumnIndex;
        private int FirstNameColumnIndex;
        private int MiddleNameColumnIndex;
        private int DobColumnIndex;
        private int DodColumnIntex;

        public DataTable AuthorTable { get { return DataTable; } }

        public AuthorTableModel() : base("authorstab", "getAllAuthorsData", "addAuthor")
        {
            AuthorIDColumnIndex = GetDBColumnData("idAuthors").IndexBasedOnOrdinal;
            LastNameColumnIndex = GetDBColumnData("LastName").IndexBasedOnOrdinal;
            FirstNameColumnIndex = GetDBColumnData("FirstName").IndexBasedOnOrdinal;
            MiddleNameColumnIndex = GetDBColumnData("MiddleName").IndexBasedOnOrdinal;
            DobColumnIndex = GetDBColumnData("YearOfBirth").IndexBasedOnOrdinal;
            DodColumnIntex = GetDBColumnData("YearOfDeath").IndexBasedOnOrdinal;
        }

        public bool AddAuthor(AuthorModel NewAuthor) => addItem(NewAuthor);
        public bool UpdateAuthor(AuthorModel NewAuthor) => updateItem(NewAuthor);

        #region Author Selector tool support

        public DataRow[] FindAuthors(string lastName, string firstname=null)
        {
            DataTable dt = AuthorTable;
            string filterString = "LastName LIKE '" + lastName + "*'";
            DataRow[] authors = dt.Select(filterString);

            return authors;
        }

        public AuthorModel GetAuthor(string lastName, string firstName)
        {
            DataTable dt = AuthorTable;
            string filterString = "LastName LIKE '" + lastName + "*' AND FirstName LIKE '" + firstName + "*'";
            DataRow[] authors = dt.Select(filterString);

            return ConvertDataRowToAuthor(authors[0]);
        }

        public uint AuthorKey(AuthorModel author)
        {
            uint key = author.AuthorId;
            if (key < 1)
            {
                DataTable dt = AuthorTable;
                string filterString = "LastName = '" + author.LastName + "' AND FirstName = '" + author.FirstName + "' AND MiddleName Like '" + author.MiddleName + "'";
                DataRow[] authors = dt.Select(filterString);
                if (authors.Length > 0)
                {
                    if (!uint.TryParse(authors[0][AuthorIDColumnIndex].ToString(), out key))
                    {
                        key = 0;
                    }
                }
                else
                {
                    key = 0;
                }
            }

            return key;
        }

        public AuthorModel GetAuthorFromId(uint key)
        {
            AuthorModel author = null;
            DataTable dt = AuthorTable;
            string filterString = "idAuthors = '" + key.ToString() + "'";
            DataRow[] authors = dt.Select(filterString);

            if (authors.Length > 0)
            {
                author = ConvertDataRowToAuthor(authors[0]);
            }

            return author;
        }

        // Keeping all internal information about columns and rows encapsulated.
        public AuthorModel ConvertDataRowToAuthor(DataRow AuthorInfo)
        {
            AuthorModel author = new AuthorModel(AuthorInfo[AuthorIDColumnIndex].ToString(), AuthorInfo[FirstNameColumnIndex].ToString(), AuthorInfo[LastNameColumnIndex].ToString(), AuthorInfo[MiddleNameColumnIndex].ToString(),
                AuthorInfo[DobColumnIndex].ToString(), AuthorInfo[DodColumnIntex].ToString());

            return author;
        }

        public List<string> AuthorNamesForSelector(DataRow[] AuthorDataRows)
        {
            List<string> authorNames = new List<string>();
            foreach (DataRow author in AuthorDataRows)
            {
                string LastFirstMiddle = author[LastNameColumnIndex].ToString() + ", " + author[FirstNameColumnIndex].ToString() + " " + author[MiddleNameColumnIndex].ToString();
                authorNames.Add(LastFirstMiddle);
            }

            return authorNames;
        }

        public string AuthorNamesCombinedString(DataRow author)
        {
            string LastFirstMiddle = author[LastNameColumnIndex].ToString() + ", " + author[FirstNameColumnIndex].ToString() + " " + author[MiddleNameColumnIndex].ToString();

            return LastFirstMiddle;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Last Name", GetDBColumnData("LastName"), parameters["@authorLastName"]);
            _addSqlCommandParameter("First Name", GetDBColumnData("FirstName"), parameters["@authorFirstName"]);
            _addSqlCommandParameter("Middle Name", GetDBColumnData("MiddleName"), parameters["@authorMiddleName"]);
            _addSqlCommandParameter("Year of Birth", GetDBColumnData("YearOfBirth"), parameters["@dob"]);
            _addSqlCommandParameter("Year of Death", GetDBColumnData("YearOfDeath"), parameters["@dod"]);
            _addSqlCommandParameter("ID", GetDBColumnData("idAuthors"), parameters["@primaryKey"]);
        }

        #endregion
    }
}
