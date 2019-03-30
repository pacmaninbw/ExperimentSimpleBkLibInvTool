using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class VolumeInSeriesTable : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int SeriesIdColumnIndex;
        private int VolumeNumberColumnIndex;

        public VolumeInSeriesTable() :
            base("volumeinseries", "getVolumeInSeriesByBook", "insertOrUpdateVolumeInSeries")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKvs").IndexBasedOnOrdinal;
            SeriesIdColumnIndex = GetDBColumnData("SeriesFK").IndexBasedOnOrdinal;
            VolumeNumberColumnIndex = GetDBColumnData("VolumeNumber").IndexBasedOnOrdinal;
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

        public VolumeInSeries GetVolumneInSersData(uint bookId)
        {
            VolumeInSeries volumeInSeries = null;
            DataRow rawVolumeInSeries = GetRawData(bookId);

            if (rawVolumeInSeries != null)
            {
                volumeInSeries = ConvertDataRowToVolumeInSeries(rawVolumeInSeries);
            }

            return volumeInSeries;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKvs"), parameters["@bookKey"]);
            _addSqlCommandParameter("Volume Number", GetDBColumnData("VolumeNumber"), parameters["@volumeNumber"]);
            _addSqlCommandParameter("Series Id", GetDBColumnData("SeriesFK"), parameters["@seriesKey"]);
        }

        private VolumeInSeries ConvertDataRowToVolumeInSeries(DataRow rawVolueInSeriesData)
        {
            uint bookId = uint.Parse(rawVolueInSeriesData[BookIDColumnIndex].ToString());
            uint seriesId = uint.Parse(rawVolueInSeriesData[SeriesIdColumnIndex].ToString());
            int volumeInSeries = int.Parse(rawVolueInSeriesData[VolumeNumberColumnIndex].ToString());

            return new VolumeInSeries(bookId, volumeInSeries, seriesId);
        }
    }
}
