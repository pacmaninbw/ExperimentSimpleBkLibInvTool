using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorTableModel : CDataTableModel
    {
        public AuthorTableModel()
        {
            _getTableStoredProcedureName = "getAllAuthorsData";
            _addItemStoredProcedureName = "addAuthor";
            _firstParameterName = "categoryName";
            InitializeDataTable();
        }

        public DataTable AuthorTable
        {
            get { return DataTable; }
        }

        public bool AddAuthor(AuthorModel NewAuthor)
        {
            bool canAddAuthorToLibrary = true;

            canAddAuthorToLibrary = NewAuthor.IsValid;
            if (canAddAuthorToLibrary)
            {
                canAddAuthorToLibrary = (_dbAddBookToLibrary(NewAuthor) > 0);
            }

            return canAddAuthorToLibrary;
        }

        private uint _dbAddBookToLibrary(AuthorModel NewAuthor)
        {
            uint NewKey = 0;
            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = _addItemStoredProcedureName;
                        cmd.Parameters.AddWithValue("authorLastName", NewAuthor.LastName);
                        cmd.Parameters.AddWithValue("authorFirstName", NewAuthor.FirstName);
                        cmd.Parameters.AddWithValue("authorMiddleName", NewAuthor.MiddleName);
                        cmd.Parameters.AddWithValue("dob", NewAuthor.YearOfBirth);
                        cmd.Parameters.AddWithValue("dod", NewAuthor.YearOfDeath);
                        cmd.Parameters.Add(new MySqlParameter(_secondParameterName, MySqlDbType.UInt32));
                        cmd.Parameters[_secondParameterName].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        NewKey = (uint)cmd.Parameters[_secondParameterName].Value;
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    NewKey = 0;
                }
            }
            return NewKey;
        }
    }
}
