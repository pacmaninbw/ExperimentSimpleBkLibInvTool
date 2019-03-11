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

        public uint GetSeriesKey(IAuthorModel author, string seriesTitle)
        {
            return GetSeriesKey(author as AuthorModel, seriesTitle);
        }

        public uint GetSeriesKey(AuthorModel author, string seriesTitle)
        {
            uint key = 0;

            if (author != null && author.IsValid)
            {
                DataTable seriesList = Series;
                string filterString = "LastName = '" + author.LastName + "' AND FirstName = '" + author.FirstName + "' AND SeriesName = '" + seriesTitle + "'";
                DataRow[] seriesTitleList = seriesList.Select(filterString);
                if (seriesTitleList.Length > 0)
                {
                    key = seriesTitleList[0].Field<uint>(seriesKeyIndex);
                }
            }

            return key;
        }

        public string GetSeriesTitle(uint seriesId)
        {
            string title = string.Empty;

            if (seriesId > 0)
            {
                DataTable seriesList = Series;
                string filterString = "idSeries = '" + seriesId.ToString() + "'";
                DataRow[] seriesTitleList = seriesList.Select(filterString);

                if (seriesTitleList.Length > 0)
                {
                    title = seriesTitleList[0].Field<string>(seriesTitleIndex);
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
