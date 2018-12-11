using System;
using System.Collections.Generic;
using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public class SeriesTableModel : CDataTableModel
    {
        public SeriesTableModel()
        {
            _getTableStoredProcedureName = "getAllSeriesData";
            _addItemStoredProcedureName = "addAuthorSeries";
            _lastParameterName = null;
            InitializeDataTable();
        }

        public DataTable Series
        {
            get { return DataTable; }
        }

        public bool AddSeries(ISeriesModel iSeriesData)
        {
            SeriesModel seriesModel = (SeriesModel)iSeriesData;
            return addItem(seriesModel);
        }

        public bool AddSeries(SeriesModel seriesModel)
        {
            return addItem(seriesModel);
        }

        private const int authorLastNameIndex = 0;
        private const int authorFirstNameIndex = 1;
        private const int seriesTitleIndex = 2;

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
    }
}
