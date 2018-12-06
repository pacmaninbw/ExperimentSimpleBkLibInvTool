using System;
using System.Collections.Generic;
using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorTableModel : CDataTableModel
    {
        // {TODO} This is a hack, there should be a way to get this info from the Datatable.
        const int IDColumnIndex = 0;
        const int LastNameColumnIndex = 1;
        const int FirstNameColumnIndex = 2;
        const int MiddleNameColumnIndex = 3;
        const int DobColumnIndex = 4;
        const int DodColumnIntex = 5;

        #region Database support

        public AuthorTableModel()
        {
            _getTableStoredProcedureName = "getAllAuthorsData";
            _addItemStoredProcedureName = "addAuthor";
            _lastParameterName = "primaryKey";
            InitializeDataTable();
        }

        public DataTable AuthorTable
        {
            get { return DataTable; }
        }

        public bool AddAuthor(AuthorModel NewAuthor)
        {
            return addItem(NewAuthor);
        }

        #endregion

        #region Author Selector tool support

        public DataRow[] FindAuthors(string lastName, string firstname=null)
        {
            DataTable dt = AuthorTable;
            string filterString = "LastName LIKE '" + lastName + "*'";
            DataRow[] authors = dt.Select(filterString);

            return authors;
        }

        // Keeping all internal information about columns and rows encapsulated.
        public AuthorModel ConvertDataRowToAuthor(DataRow AuthorInfo)
        {
            AuthorModel author = new AuthorModel(AuthorInfo[FirstNameColumnIndex].ToString(), AuthorInfo[LastNameColumnIndex].ToString(), AuthorInfo[MiddleNameColumnIndex].ToString(),
                AuthorInfo[DobColumnIndex].ToString(), AuthorInfo[DodColumnIntex].ToString(), Convert.ToUInt32(AuthorInfo[IDColumnIndex].ToString())); ;

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

        #endregion
    }
}
