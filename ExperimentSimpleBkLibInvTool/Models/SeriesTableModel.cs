using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;
using pacsw.BookInventory.Models.Author;

namespace pacsw.BookInventory.Models.Series
{
    public class SeriesTableModel : CDataTableModel
    {
        private int seriesTitleIndex;
        private int seriesKeyIndex;
        private int seriesAuthorKeyIndex;

        public SeriesTableModel() : base("series", "getAllSeriesData", "addAuthorSeries")
        {
            seriesTitleIndex = GetDBColumnData("SeriesName").IndexBasedOnOrdinal;
            seriesKeyIndex = GetDBColumnData("idSeries").IndexBasedOnOrdinal;
            seriesAuthorKeyIndex = GetDBColumnData("AuthorOfSeries").IndexBasedOnOrdinal;
        }

        public DataTable Series { get { return DataTable; } }

        public bool AddSeries(ISeriesModel iSeriesData)
        {
            SeriesModel seriesModel = (SeriesModel)iSeriesData;
            return addItem(seriesModel);
        }

        public bool AddSeries(SeriesModel seriesModel)
        {
            return addItem(seriesModel);
        }

        public List<string> SeriesSelectionListCreator(AuthorModel author)
        {
            List<string> seriesSelectionList = new List<string>();

            if (author != null && author.IsValid)
            {
                DataTable currentSeriesList = Series;
                string filterString = "LastName = '" + author.LastName + "' AND FirstName = '" + author.FirstName + "'";
                DataRow[] seriesTitleList = currentSeriesList.Select(filterString);

                foreach (DataRow row in seriesTitleList)
                {
                    seriesSelectionList.Add(row[seriesTitleIndex].ToString());
                }

            }

            return seriesSelectionList;
        }

        public uint GetSeriesKey(AuthorModel author, string seriesTitle)
        {
            uint key = 0;

            if (author != null && author.IsValid)
            {
                string SqlQuery = "SELECT series.idSeries FROM series WHERE series.SeriesName = '" + seriesTitle + "' AND series.AuthorOfSeries = '" + author.AuthorId.ToString() + "';";

                using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                {
                    int ResultCount = 0;
                    DataTable Dt = new DataTable();
                    try
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = SqlQuery;
                            cmd.ExecuteNonQuery();
                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            ResultCount = sda.Fill(Dt);
                            if (ResultCount > 0)
                            {
                                key = Dt.Rows[0].Field<uint>(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = "Database Error: " + ex.Message;
                        MessageBox.Show(errorMsg);
                        key = 0;
                    }
                }
            }

            return key;
        }

        public string GetSeriesTitle(uint seriesId)
        {
            string title = string.Empty;

            if (seriesId > 0)
            {
                string SqlQuery = "SELECT series.SeriesName FROM series WHERE series.idSeries = '" + seriesId + "';";

                using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                {
                    int ResultCount = 0;
                    DataTable Dt = new DataTable();
                    try
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = SqlQuery;
                            cmd.ExecuteNonQuery();
                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            ResultCount = sda.Fill(Dt);
                            if (ResultCount > 0)
                            {
                                title = Dt.Rows[0].Field<string>(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = "Database Error: " + ex.Message;
                        MessageBox.Show(errorMsg);
                    }
                }
            }

                return title;
        }

        protected override void InitializeSqlCommandParameters()
        {
            AuthorTableModel authorTable = ((App)Application.Current).Model.AuthorTable;
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("First Name", authorTable.GetDBColumnData("FirstName"), parameters["@authorFirst"]);
            _addSqlCommandParameter("Last Name", authorTable.GetDBColumnData("LastName"), parameters["@authorLast"]);
            _addSqlCommandParameter("Series Title", GetDBColumnData("SeriesName"), parameters["@seriesTitle"]);
        }
    }
}
