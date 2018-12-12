using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookTableModel : CDataTableModel
    {
        public DataTable BookTable
        {
            get { return DataTable; }
        }

        public BookTableModel()
        {
            _getTableStoredProcedureName = "getallbooks";
            _addItemStoredProcedureName = "addBookToLibrary";
            InitializeDataTable();
        }

        public bool AddBook(BookInfoModel NewBook)
        {
            return addItem(NewBook);
            return true;
        }

        private bool _dbAddBookToLibrary(BookInfoModel NewBook)
        {
            bool AddSeriesSuccess = true;
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
                        cmd.Parameters.AddWithValue("authorFirst", NewBook.AuthorInfo.FirstName);
                        cmd.Parameters.AddWithValue("authorLast", NewBook.AuthorInfo.LastName);
                        cmd.Parameters.AddWithValue("seriesTitle", NewBook.SeriesInfo.Title);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    AddSeriesSuccess = false;
                }
            }
            return AddSeriesSuccess;
        }
    }
}
