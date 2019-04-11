using System.Data;
using MySql.Data.MySqlClient;


namespace pacsw.BookInventory.Models
{
    public class PublishInfoTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int ISBNumberColumnIndex;
        private int CopyrightColumnIndex;
        private int EditionColumnIndex;
        private int PrintingColumnIndex;
        private int PublisherColumnIndex;
        private int OutOfPrintColumnIndex;

        public PublishInfoTableModel() : base("publishinginfo", "getPublishingInfo", "addPublishingInfo")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKPubI").IndexBasedOnOrdinal;
            ISBNumberColumnIndex = GetDBColumnData("ISBNumber").IndexBasedOnOrdinal;
            CopyrightColumnIndex = GetDBColumnData("Copyright").IndexBasedOnOrdinal;
            EditionColumnIndex = GetDBColumnData("Edition").IndexBasedOnOrdinal;
            PrintingColumnIndex = GetDBColumnData("Printing").IndexBasedOnOrdinal;
            PublisherColumnIndex = GetDBColumnData("Publisher").IndexBasedOnOrdinal;
            OutOfPrintColumnIndex = GetDBColumnData("OutOfPrint").IndexBasedOnOrdinal;
        }

        public DataTable PublishInfoTable => DataTable;

        public bool AddPublishingInfo(IPublishInfoModel PublishingData, uint bookId = 0)
        {
            PublishInfoModel publishInfoModel = (PublishInfoModel)PublishingData;
            return (publishInfoModel.BookId > 0) ? addItem(publishInfoModel) : false;
        }

        public PublishInfoModel GetPublishInfo(uint bookId)
        {
            DataRow rawPublishingData = GetRawData(bookId);
            return (rawPublishingData != null) ? ConvertDataRowToPublishInfoModel(rawPublishingData) : null;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKPubI"), parameters["@bookKey"]);
            _addSqlCommandParameter("Copyright", GetDBColumnData("Copyright"), parameters["@copyright"]);
            _addSqlCommandParameter("ISB Number", GetDBColumnData("ISBNumber"), parameters["@iSBNumber"]);
            _addSqlCommandParameter("Edition", GetDBColumnData("Edition"), parameters["@edition"]);
            _addSqlCommandParameter("Printing", GetDBColumnData("Printing"), parameters["@printing"]);
            _addSqlCommandParameter("Publisher", GetDBColumnData("Publisher"), parameters["@publisher"]);
            _addSqlCommandParameter("Out of Print", GetDBColumnData("OutOfPrint"), parameters["@outOfPrint"]);
        }

        private PublishInfoModel ConvertDataRowToPublishInfoModel(DataRow rawPublishingData)
        {
            uint bookId = uint.Parse(rawPublishingData[BookIDColumnIndex].ToString());
            string iSBNumber = rawPublishingData[ISBNumberColumnIndex].ToString();
            string copyRight = rawPublishingData[CopyrightColumnIndex].ToString();
            string publisher = rawPublishingData[PublisherColumnIndex].ToString();
            string printing = rawPublishingData[PrintingColumnIndex].ToString();
            string edition = rawPublishingData[EditionColumnIndex].ToString();
            bool outOfPrint = int.Parse(rawPublishingData[OutOfPrintColumnIndex].ToString()) > 0;

            return new PublishInfoModel(bookId, iSBNumber, copyRight, publisher, printing, edition, outOfPrint);
        }
    }
}
