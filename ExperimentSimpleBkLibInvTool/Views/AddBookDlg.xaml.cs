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
        private CategoryTableModel categoryTable;
        private FormatTableModel formatTable;
        private StatusTableModel statusTable;
        private ConditionsTableModel conditionsTable;
        private SeriesTableModel seriesTable;
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
            InitCategorySelection();
            InitPublishingInfo();
            InitPuchaseInfo();
            InitStatusSelection();
            InitFormatSelection();
            InitConditionSelection();
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
            if (string.IsNullOrEmpty(category.Category))
            {
                LB_CategorySelector.Background = Brushes.Red;
                MessageBox.Show("A category is required for adding a new book!");
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
            publishInfo.CopyRight = TB_Copyright.Text;
        }

        private void TB_ISBNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            publishInfo.ISBNumber = TB_ISBNumber.Text;
        }

        private void TB_Publisher_LostFocus(object sender, RoutedEventArgs e)
        {
            publishInfo.Publisher = TB_Publisher.Text;
        }

        private void TB_Edition_LostFocus(object sender, RoutedEventArgs e)
        {
            publishInfo.Edition = TB_Edition.Text;
        }

        private void TB_Printing_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CB_OutofPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Purchase Info
        private void InitPuchaseInfo()
        {
            purchaseInfo = new PuchaseInfoModel();
        }

        private void TB_Vendor_LostFocus(object sender, RoutedEventArgs e)
        {
            purchaseInfo.Vendor = TB_Vendor.Text;
        }

        private void TB_ListPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            purchaseInfo.ListPrice = TB_ListPrice.Text;
        }

        private void TB_PaidPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            purchaseInfo.PaidPrice = TB_PaidPrice.Text;
        }

        private void DP_DatePurchased_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            purchaseInfo.PurchaseDate = DP_DatePurchased.SelectedDate.Value.Date;
        }

        #endregion

        #region Ratings

        private void TB_MyRating_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ratings == null)
            {
                ratings = new RatingsModel();
            }

            ratings.MyRating = TB_MyRating.Text;
        }

        private void TB_AmazonRating_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ratings == null)
            {
                ratings = new RatingsModel();
            }

            ratings.AmazonRating = TB_AmazonRating.Text;
        }

        private void TB_GoodReadsRating_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ratings == null)
            {
                ratings = new RatingsModel();
            }

            ratings.GoodReadsRating = TB_GoodReadsRating.Text;
        }

        #endregion

        #region Category Selection

        private void InitCategorySelection()
        {
            categoryTable = TheApp.Model.CategoryTable;
            category = new CategoryModel();
            List<string> categories = categoryTable.ListBoxSelectionList();
            LB_CategorySelector.DataContext = categories;
            if (categories.Count < 1)
            {
                string mbMsg = "There are no categories, would you like to add one?";
                MessageBoxResult messageBoxResult = MessageBox.Show(mbMsg, "Add Category Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddCategoryDlg AddAuthorControl = new AddCategoryDlg();
                    AddAuthorControl.Show();
                    return;
                }
            }
            LB_CategorySelector.Items.Clear();
            foreach (string category in categories)
            {
                LB_CategorySelector.Items.Add(category);
            }
        }

        private void LB_CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_CategorySelector.Background = Brushes.Wheat;
            category.Category = (string)LB_CategorySelector.SelectedValue;
            category.Key = categoryTable.CategoryKey(category.Category);
        }

        #endregion

        #region Series Selection

        private void InitSeriesSelection()
        {
            seriesTable = TheApp.Model.SeriesTable;
        }

        private void LB_SeriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Format Selection

        private void InitFormatSelection()
        {
            formatTable = TheApp.Model.FormatTable;
            List<string> formats = formatTable.ListBoxSelectionList();
            LB_FormatSelector.DataContext = formats;
            if (formats.Count < 1)
            {
                string mbMsg = "There are no formats, would you like to add one?";
                MessageBoxResult messageBoxResult = MessageBox.Show(mbMsg, "Add Format Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddFormatDlg AddformatControl = new AddFormatDlg();
                    AddformatControl.Show();
                    return;
                }
            }
            LB_FormatSelector.Items.Clear();
            foreach (string format in formats)
            {
                LB_FormatSelector.Items.Add(format);
            }

        }

        private void LB_FormatSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            formatModel = new FormatModel();
            formatModel.Format = LB_FormatSelector.SelectedValue.ToString();
            formatModel.Key = formatTable.FormatKey(formatModel.Format);
        }

        #endregion

        #region Status Selection

        private void InitStatusSelection()
        {
            statusTable = TheApp.Model.StatusTable;
            List<string> statuses = statusTable.ListBoxSelectionList();
            LB_StatusSelector.DataContext = statuses;
            LB_StatusSelector.Items.Clear();
            foreach (string status in statuses)
            {
                LB_StatusSelector.Items.Add(status);
            }
        }

        private void LB_StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bookStatus = LB_StatusSelector.SelectedValue.ToString();
        }

        #endregion

        #region Condition Selection

        private void InitConditionSelection()
        {
            conditionsTable  = TheApp.Model.ConditionsTable;
            List<string> conditions = conditionsTable.ListBoxSelectionList();
            LB_ConditionSelector.DataContext = conditions;
            LB_ConditionSelector.Items.Clear();
            foreach (string condition in conditions)
            {
                LB_ConditionSelector.Items.Add(condition);
            }
        }

        private void LB_ConditionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bookCondition = LB_ConditionSelector.SelectedValue.ToString();
        }

        #endregion

        #region For Sale 


        #endregion

        #region Owned


        #endregion

    }
}
