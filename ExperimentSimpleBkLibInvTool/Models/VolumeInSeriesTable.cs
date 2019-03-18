using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pacsw.BookInventory.Models.DataTableModel;

namespace pacsw.BookInventory.Models.VolumeInSeriesModels
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
