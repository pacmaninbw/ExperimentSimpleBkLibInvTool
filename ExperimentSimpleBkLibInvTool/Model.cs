using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkStatusTable;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkConditionTable;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC
{
    public class Model
    {
        private SeriesTableModel _seriesTable;
        private CategoryTableModel _categories;
        private FormatTableModel _formats;
        private BookTableModel _books;
        private BookInfoTableModel _bookInfoTable;
        private AuthorTableModel _authors;
        private StatusTableModel _statuses;
        private ConditionsTableModel _conditions;
        private RatingsTableModel _ratings;
        private ForSaleTableModel _forSale;
        private OwnerShipTableModel _ownerShip;
        private PurchaseInfoTableModel _purchase;
        private PublishInfoTableModel _publishInfo;

        public Model()
        {
            _formats = new FormatTableModel();
            _categories = new CategoryTableModel();
            _authors = new AuthorTableModel();
            _statuses = new StatusTableModel();
            _conditions = new ConditionsTableModel();
            _ratings = new RatingsTableModel();
            _forSale = new ForSaleTableModel();
            _ownerShip = new OwnerShipTableModel();
            _publishInfo = new PublishInfoTableModel();
            _purchase = new PurchaseInfoTableModel();
        }

        public void InitilizeTablesWithDependencies()
        {
            _seriesTable = new SeriesTableModel();
            _bookInfoTable = new BookInfoTableModel();
            _books = new BookTableModel();
        }

        public FormatTableModel FormatTable { get { return _formats; } }

        public CategoryTableModel CategoryTable { get { return _categories; } }

        public SeriesTableModel SeriesTable { get { return _seriesTable; } }

        public BookTableModel BookTable { get { return _books; } }

        public AuthorTableModel AuthorTable { get { return _authors; } }

        public StatusTableModel StatusTable { get { return _statuses; } }

        public ConditionsTableModel ConditionsTable { get { return _conditions; } }

        public RatingsTableModel RatingsTable { get { return _ratings; } }

        public ForSaleTableModel ForSaleTable { get { return _forSale; } }

        public OwnerShipTableModel OwnerShip { get { return _ownerShip; } }

        public PublishInfoTableModel PublishingData { get { return _publishInfo; } }

        public PurchaseInfoTableModel PurchaseData { get { return _purchase; } }

        public BookInfoTableModel BookInfoTable { get { return _bookInfoTable; } }
    }
}
