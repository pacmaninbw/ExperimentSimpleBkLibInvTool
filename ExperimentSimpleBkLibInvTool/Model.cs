using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo;
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
        private AuthorTableModel _authors;
        private StatusTableModel _statuses;
        private ConditionsTableModel _conditions;

        public Model()
        {
            _seriesTable = new SeriesTableModel();
            _formats = new FormatTableModel();
            _categories = new CategoryTableModel();
            _books = new BookTableModel();
            _authors = new AuthorTableModel();
            _statuses = new StatusTableModel();
            _conditions = new ConditionsTableModel();
        }

        public FormatTableModel FormatTable
        {
            get
            {
                return _formats;
            }
        }

        public CategoryTableModel CategoryTable
        {
            get
            {
                return _categories;
            }
        }

        public SeriesTableModel SeriesTable
        {
            get
            {
                return _seriesTable;
            }
        }

        public BookTableModel BookTable
        {
            get
            {
                return _books;
            }
        }

        public AuthorTableModel AuthorTable
        {
            get
            {
                return _authors;
            }
        }

        public StatusTableModel StatusTable
        {
            get
            {
                return _statuses;
            }
        }

        public ConditionsTableModel ConditionsTable
        {
            get
            {
                return _conditions;
            }
        }
    }
}
