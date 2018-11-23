using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public class SeriesModel : ISeriesModel
    {
        private string title;
        private int authorId;
        private AuthorModel _author;

        public AuthorModel Author { get { return _author; }  }

        public string SeriesTitle { get { return title; } }

        public SeriesModel()
        {
            authorId = 0;
            title = null;
            _author = null;
        }

        public SeriesModel(AuthorModel author)
        {
            authorId = 0;
            _author = author;
            title = null;
        }

        public void SetAuthor(AuthorModel author)
        {
            _author = author;
        }

        public void  SetTitle(string seriesTitle)
        {
            title = seriesTitle;
        }

        public int getAuthorId()
        {
            return authorId;
        }

        public void setAuthorId(int AuthorId)
        {
            authorId = AuthorId;
        }
    }

    public class SeriesTableModel : CDataTableModel
    {
        public SeriesTableModel()
        {
            _getTableStoredProcedureName = "getAllBookCategoriesWithKeys";
            _addItemStoredProcedureName = "addAuthorSeries";
            _firstParameterName = "categoryName";
        }

        public DataTable Series
        {
            get
            {
                return DataTable;
            }
        }

        private void ShowAuthorNameError()
        {
            string errorMsg = "Add Series error: The first and last names of the author are required fields";
            MessageBox.Show(errorMsg);
        }

        public bool AddSeries(ISeriesModel SeriesData)
        {
            bool canInsertData = true;

            if (SeriesData.Author == null)
            {
                ShowAuthorNameError();
                canInsertData = false;
            }
            else
            {
                if (SeriesData.Author.FirstName == null || SeriesData.Author.FirstName.Length < 1)
                {
                    ShowAuthorNameError();
                    canInsertData = false;
                }
                else
                {
                    if (SeriesData.Author.LastName == null || SeriesData.Author.LastName.Length < 1)
                    {
                        ShowAuthorNameError();
                        canInsertData = false;
                    }
                    else
                    {
                        if (SeriesData.SeriesTitle == null || SeriesData.SeriesTitle.Length < 1)
                        {
                            string errorMsg = "Add Series error: Missing title of series";
                            MessageBox.Show(errorMsg);
                            canInsertData = false;
                        }
                        else
                        {
                            canInsertData = _dbAddSeries(SeriesData);
                        }
                    }
                }
            }


            return canInsertData;
        }
        private bool _dbAddSeries(ISeriesModel SeriesData)
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
                        cmd.Parameters.AddWithValue("authorFirst", SeriesData.Author.FirstName);
                        cmd.Parameters.AddWithValue("authorLast", SeriesData.Author.LastName);
                        cmd.Parameters.AddWithValue("seriesTitle", SeriesData.SeriesTitle);
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
