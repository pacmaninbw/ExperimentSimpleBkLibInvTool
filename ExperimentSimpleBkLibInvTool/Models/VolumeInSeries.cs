using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class VolumeInSeries : DataTableItemBaseModel
    {
        public VolumeInSeries(uint bookId = 0, int volumeNumber = 0, uint seriesId = 0) :
            base(((App)Application.Current).Model.VolumeInSeriesTable)
        {
            BookId = bookId;
            SeriesId = seriesId;
            VolumeNumber = volumeNumber;

            IsModified = false;       // Initialization is not modification.
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

        public override bool AddToDb() => ((App)Application.Current).Model.VolumeInSeriesTable.AddVolumeInSeries(this);
        public override bool DbUpdate() => ((App) Application.Current).Model.VolumeInSeriesTable.UpdateVolumeInSeries(this);

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
