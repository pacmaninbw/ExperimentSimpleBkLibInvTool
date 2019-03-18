using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class VolumeInSeries : DataTableItemBaseModel
    {
        public VolumeInSeries() :
            base(((App)Application.Current).Model.VolumeInSeriesTable)
        {
            SeriesId = 0;
            VolumeNumber = 0;
        }

        public VolumeInSeries(int volumeNumber, uint seriesId = 0) :
            base(((App)Application.Current).Model.VolumeInSeriesTable)
        {
            SeriesId = seriesId;
            VolumeNumber = volumeNumber;
        }

        public int VolumeNumber
        {
            get { return GetParameterIValue("Volume Number"); }
            set { SetParameterValue("Volume Number", value); }
        }

        public uint SeriesId
        {
            get { return GetParameterKValue("Series Id"); }
            set { SetParameterValue("Series Id", value); }
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.VolumeInSeriesTable.AddVolumeInSeries(this);
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
