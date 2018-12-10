using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkStatusTable;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkConditionTable;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddBookDlg.xaml
    /// </summary>
    public partial class AddBookDlg : Window
    {
        private readonly App TheApp = Application.Current as App;

        private AuthorTableModel _authorTable;
        private BookTableModel myLibrary;
        DataRow[] _authors;
        private AuthorModel selectedAuthor;
        private BookInfoModel newBook;
        private PublishInfoModel publishInfo;
        private PuchaseInfoModel purchaseInfo;
        private CategoryModel category;
        private FormatModel formatModel;
        private RatingsModel ratings;
        private OwnerShipModel owned;
        private ForSaleModel salesInfo;
        private SeriesModel seriesInfo;
        private string title;
        private string bookStatus;
        private string bookCondition;

        public AddBookDlg()
        {
            InitializeComponent();
            InitAuthorSelection();
            InitPublishingInfo();
            InitPuchaseInfo();
        }

        private void Btn_AddBookSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAuthor != null && selectedAuthor.IsValid && !string.IsNullOrEmpty(title))
            {
                newBook = new BookInfoModel(selectedAuthor, title, category, purchaseInfo, publishInfo, owned, salesInfo, seriesInfo, formatModel, ratings, bookStatus, bookCondition);
                myLibrary = TheApp.Model.BookTable;
                if (myLibrary.AddBook(newBook))
                {
                    Close();
                }
            }
            if (selectedAuthor == null || !selectedAuthor.IsValid)
            {
                HighLightAuthorAndReportError();
            }
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("The title is required for adding a new book!");
                TB_BookTitle.Background = Brushes.Red;
            }
        }

        private void Btn_AddBookCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TB_BookTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_BookTitle.Text))
            {
                title = TB_BookTitle.Text;
                TB_BookTitle.Background = Brushes.White;
            }
        }

        #region AuthorSelection

        private void InitAuthorSelection()
        {
            selectedAuthor = null;
            _authorTable = TheApp.Model.AuthorTable;
            _authors = _authorTable.FindAuthors("", "");    // Show all authors to beging with.
            AddRowsToListBox();
        }


        private void TB_SelectAuthorLastName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = _authorTable.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void TB_SelectAuthorFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = _authorTable.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAuthor = _authorTable.ConvertDataRowToAuthor(_authors[AuthorSelectorLB.SelectedIndex]);
            TB_SelectAuthorFirstName.Text = selectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = selectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = selectedAuthor.MiddleName;
            TB_SelectAuthorLastName.Background = Brushes.White;
            TB_SelectAuthorFirstName.Background = Brushes.White;
            AuthorSelectorLB.Background = Brushes.White;
        }

        private void HighLightAuthorAndReportError()
        {
            MessageBox.Show("The author is required for adding a new book!");
            TB_SelectAuthorLastName.Background = Brushes.Red;
            TB_SelectAuthorFirstName.Background = Brushes.Red;
            AuthorSelectorLB.Background = Brushes.Red;
            FocusManager.SetFocusedElement(this, TB_SelectAuthorLastName);
        }

        private void AddRowsToListBox()
        {
            List<string> authorNames = _authorTable.AuthorNamesForSelector(_authors);
            if (authorNames.Count < 1)
            {
                string mbMsg = "There are no authors with that name, would you like to add an author?";
                MessageBoxResult messageBoxResult = MessageBox.Show(mbMsg, "Add Author Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
                    AddAuthorControl.Show();
                }
            }
            else
            {
                AuthorSelectorLB.Items.Clear();
                AuthorSelectorLB.DataContext = authorNames;
                foreach (string author in authorNames)
                {
                    AuthorSelectorLB.Items.Add(author);
                }
            }
        }



        #endregion

        #region Publishing Info
        private void InitPublishingInfo()
        {
            publishInfo = new PublishInfoModel();
        }

        private void TB_Copyright_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region Purchase Info
        private void InitPuchaseInfo()
        {
            purchaseInfo = new PuchaseInfoModel();
        }

        #endregion
    }
}
