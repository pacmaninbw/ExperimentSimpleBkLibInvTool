using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

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
    }
}
