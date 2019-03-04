using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public class SeriesTableModel : CDataTableModel
    {
        private int seriesTitleIndex;

        public SeriesTableModel() : base("series", "getAllSeriesData", "addAuthorSeries")
        {
            seriesTitleIndex = GetDBColumnData("SeriesName").Ordinal_Posistion;
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

        // This method could be rewritten to use the stored procedure getAllSeriesByThisAuthor
        // This is better because it doesn't require changing _getTableStoredProcedureName and
        // then changing it back. It also refreshes the series list if necessary.
        public List<string> SeriesSelectionListCreator(AuthorModel author)
        {
            List<string> seriesSelectionList = new List<string>();
            if (author == null || !author.IsValid)
            {
                return seriesSelectionList;
            }

            DataTable currentSeriesList = Series;
            string filterString = "LastName = '" + author.LastName + "' AND FirstName = '" + author.FirstName + "'";
            DataRow[] seriesTitleList = currentSeriesList.Select(filterString);

            foreach (DataRow row in seriesTitleList)
            {
                seriesSelectionList.Add(row[seriesTitleIndex].ToString());
            }

            return seriesSelectionList;
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
