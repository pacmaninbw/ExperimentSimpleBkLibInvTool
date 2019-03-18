using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class VolumeInSeriesTable : CDataTableModel
    {
        public VolumeInSeriesTable() :
            base("volumeinseries", "getVolumeInSeriesByBook", "insertOrUpdateVolumeInSeries")
        {
        }

        public bool AddVolumeInSeries(VolumeInSeries volumeInSeries)
        {
            if (volumeInSeries.BookId > 0)
            {
                return addItem(volumeInSeries);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKvs"), parameters["@bookKey"]);
            _addSqlCommandParameter("Volume Number", GetDBColumnData("VolumeNumber"), parameters["@volumeNumber"]);
            _addSqlCommandParameter("Series Id", GetDBColumnData("SeriesFK"), parameters["@seriesKey"]);
        }
    }
}
