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
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkStatusTable;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkConditionTable;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddBookDlg.xaml
    /// Build the book from the parts that the user enters, then save the book to the database.
    /// </summary>
    public partial class AddBookDlg : Window
    {
        private readonly App TheApp = Application.Current as App;

        private AuthorTableModel _authorTable;
        private DataRow[] _authors;
        private BookModel newBook;
        private OwnerShipModel owned;
        private ForSaleModel salesInfo;

        public IAuthorModel SelectedAuthor { get { return newBook.AuthorInfo; } set { newBook.AuthorInfo = value as AuthorModel; } }

        public AddBookDlg()
        {
            InitializeComponent();
            newBook = new BookModel();
            InitAuthorSelection();
            PopulateSeriesSelector();
            InitCategorySelection();
            InitStatusSelection();
            InitFormatSelection();
            InitConditionSelection();
            InitForSaleData();
            InitOwnerShip();
            Loaded += new RoutedEventHandler(bypassAuthorSelectionIfAuthorSelected);
        }

        private void Btn_AddBookSave_Click(object sender, RoutedEventArgs e)
        {
            bool hasErrors = false;
            AuthorModel author = newBook.AuthorInfo as AuthorModel;
            if (author == null || !author.IsValid)
            {
                HighLightAuthorAndReportError();
                hasErrors = true;
            }

            if (string.IsNullOrEmpty(newBook.Title))
            {
                MessageBox.Show("The title is required for adding a new book!");
                TB_BookTitle.Background = Brushes.Red;
                hasErrors = true;
            }

            if (string.IsNullOrEmpty(newBook.Genre))
            {
                LB_CategorySelector.Background = Brushes.Red;
                MessageBox.Show("A category is required for adding a new book!");
                hasErrors = true;
            }

            if (!newBook.IsValid)
            {
                hasErrors = true;
            }

            if (!hasErrors)
            {
                if (newBook.AddBookToLibrary())
                {
                    Close();
                }
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
                newBook.Title = TB_BookTitle.Text;
                TB_BookTitle.Background = Brushes.White;
            }
        }

        #region AuthorSelection

        private void InitAuthorSelection()
        {
            _authorTable = TheApp.Model.AuthorTable;
            _authors = _authorTable.FindAuthors("", "");    // Show all authors to beging with.
            AddRowsToListBox();
        }

        private void SetAuthorNameValues()
        {
            AuthorModel selectedAuthor = newBook.AuthorInfo as AuthorModel;
            TB_SelectAuthorFirstName.Text = selectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = selectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = selectedAuthor.MiddleName;
            TB_SelectAuthorLastName.Background = Brushes.White;
            TB_SelectAuthorFirstName.Background = Brushes.White;
            AuthorSelectorLB.Background = Brushes.White;
            PopulateSeriesSelector();   // if the author has any series list them so they can be selected.
        }

        private void SelectAuthorClosed(object sender, EventArgs e)
        {
            SelectAuthorDlg authorSelector = (SelectAuthorDlg)sender;
            newBook.AuthorInfo = authorSelector.SelectedAuthor;
            if (newBook.AuthorInfo != null)
            {
                SetAuthorNameValues();
            }
            else
            {
                HighLightAuthorAndReportError();
            }
        }

        private void bypassAuthorSelectionIfAuthorSelected(object sender, RoutedEventArgs e)
        {
            if (newBook.AuthorInfo == null)
            {
                SelectAuthorDlg selectAuthor = new SelectAuthorDlg();
                selectAuthor.Closed += new EventHandler(SelectAuthorClosed);
                selectAuthor.Show();
            }
            else
            {
                SetAuthorNameValues();
            }
        }

        private void TB_SelectAuthorLastName_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAuthorDlg selectAuthor = new SelectAuthorDlg();
            selectAuthor.Closed += new EventHandler(SelectAuthorClosed);
            selectAuthor.TB_SelectAuthorLastName.Text = TB_SelectAuthorLastName.Text;
            selectAuthor.Show();
        }

        private void TB_SelectAuthorFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAuthorDlg selectAuthor = new SelectAuthorDlg();
            selectAuthor.Closed += new EventHandler(SelectAuthorClosed);
            selectAuthor.TB_SelectAuthorFirstName.Text = TB_SelectAuthorFirstName.Text;
            selectAuthor.Show();
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorModel selectedAuthor = _authorTable.ConvertDataRowToAuthor(_authors[AuthorSelectorLB.SelectedIndex]);
            newBook.AuthorInfo = selectedAuthor;

            TB_SelectAuthorFirstName.Text = selectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = selectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = selectedAuthor.MiddleName;
            TB_SelectAuthorLastName.Background = Brushes.White;
            TB_SelectAuthorFirstName.Background = Brushes.White;
            AuthorSelectorLB.Background = Brushes.White;
            PopulateSeriesSelector();   // if the author has any series list them so they can be selected.
        }

        private void HighLightAuthorAndReportError()
        {
            MessageBox.Show("The author is required for adding a new book!");
            TB_SelectAuthorLastName.Background = Brushes.Red;
            TB_SelectAuthorFirstName.Background = Brushes.Red;
            AuthorSelectorLB.Background = Brushes.Red;
//            FocusManager.SetFocusedElement(this, TB_SelectAuthorLastName);
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

        private void BTN_AddPublishingInfo_Click(object sender, RoutedEventArgs e)
        {
            AddPublishingInformation addPublishinginfoDlg = new AddPublishingInformation();
            addPublishinginfoDlg.Closed += new EventHandler(GetPublishingInfoFromDialog);
            addPublishinginfoDlg.Show();
        }

        private void GetPublishingInfoFromDialog(object sender, EventArgs e)
        {
            AddPublishingInformation addPublishinginfoDlg = sender as AddPublishingInformation;
            newBook.PublishInfo = addPublishinginfoDlg.PublishInfo;
        }

        #endregion

        #region Purchase Info

        private void GetPurchasingInfoFromDialog(object sender, EventArgs e)
        {
            PurchasingDialog addPurchasingDlg = sender as PurchasingDialog;
            newBook.PuchaseInfo = addPurchasingDlg.PurchaseInfo;
        }

        private void BTN_AddPurchaseInfo_Click(object sender, RoutedEventArgs e)
        {
            PurchasingDialog purchaseInfoDlg = new PurchasingDialog();
            purchaseInfoDlg.Closed += new EventHandler(GetPurchasingInfoFromDialog);
            purchaseInfoDlg.Show();
        }

        #endregion

        #region Ratings

        private void BTN_AddRatings_Click(object sender, RoutedEventArgs e)
        {
            AddRatingsDlg addRatingsDlg = new AddRatingsDlg();
            addRatingsDlg.Closed += new EventHandler(GetRatingsFromAddRatings);
            addRatingsDlg.Show();
        }

        private void GetRatingsFromAddRatings(object sender, EventArgs e)
        {
            AddRatingsDlg addRatingsDlg = (AddRatingsDlg)sender;
            newBook.Ratings = addRatingsDlg.Ratings;
        }

        #endregion

        #region Category Selection

        private void InitCategorySelection()
        {
            List<string> categories = TheApp.Model.CategoryTable.ListBoxSelectionList();
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
            LB_CategorySelector.Background = Brushes.White;
            newBook.Genre = LB_CategorySelector.SelectedValue.ToString();
        }

        #endregion

        #region Series Selection

        private void LB_SeriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorModel selectedAuthor = newBook.AuthorInfo as AuthorModel;

            if (selectedAuthor != null && selectedAuthor.IsValid)
            {
                newBook.SeriesInfo = new SeriesModel(selectedAuthor, LB_SeriesSelector.SelectedValue.ToString());
            }
        }

        private void PopulateSeriesSelector()
        {
            AuthorModel selectedAuthor = newBook.AuthorInfo as AuthorModel;

            LB_SeriesSelector.Items.Clear();
            if (selectedAuthor == null || !selectedAuthor.IsValid)
            {
                LB_SeriesSelector.Items.Add("No Series to select.");
            }
            else
            {
                List<string> seriesTitles = TheApp.Model.SeriesTable.SeriesSelectionListCreator(selectedAuthor);
                if (seriesTitles.Count < 1)
                {
                    LB_SeriesSelector.Items.Add("No Series to select.");
                }
                else
                {
                    LB_SeriesSelector.DataContext = seriesTitles;
                    foreach (string title in seriesTitles)
                    {
                        LB_SeriesSelector.Items.Add(title);
                    }
                }
            }
        }

        #endregion

        #region Format Selection

        private void InitFormatSelection()
        {
            List<string> formats = TheApp.Model.FormatTable.ListBoxSelectionList();
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
            newBook.Format = LB_FormatSelector.SelectedValue.ToString();
        }

        #endregion

        #region Status Selection

        private void InitStatusSelection()
        {
            List<string> statuses = TheApp.Model.StatusTable.ListBoxSelectionList();
            LB_StatusSelector.DataContext = statuses;
            LB_StatusSelector.Items.Clear();
            foreach (string status in statuses)
            {
                LB_StatusSelector.Items.Add(status);
            }
        }

        private void LB_StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newBook.Status = LB_StatusSelector.SelectedValue.ToString();
        }

        #endregion

        #region Condition Selection

        private void InitConditionSelection()
        {
            List<string> conditions = TheApp.Model.ConditionsTable.ListBoxSelectionList();
            LB_ConditionSelector.DataContext = conditions;
            LB_ConditionSelector.Items.Clear();
            foreach (string condition in conditions)
            {
                LB_ConditionSelector.Items.Add(condition);
            }
        }

        private void LB_ConditionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newBook.Condition = LB_ConditionSelector.SelectedValue.ToString();
        }

        #endregion

        #region For Sale 
        private void InitForSaleData()
        {
            salesInfo = new ForSaleModel();
        }

        private void ChkBx_IsForSale_Click(object sender, RoutedEventArgs e)
        {
            salesInfo.IsForSale = ChkBx_IsForSale.IsChecked.Value;
        }

        private void TB_AskingPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            salesInfo.AskingPrice = TB_AskingPrice.Text;
        }

        private void TB_EstimatedValue_LostFocus(object sender, RoutedEventArgs e)
        {
            salesInfo.EstimatedValue = TB_EstimatedValue.Text;
        }

        #endregion

        #region Owned

        private void InitOwnerShip()
        {
            owned = new OwnerShipModel(false);
        }

        private void ChkBx_BookIsOwned_Click(object sender, RoutedEventArgs e)
        {
            owned.IsOwned = ChkBx_BookIsOwned.IsChecked.Value;
        }

        private void ChkBx_Wishlisted_Click(object sender, RoutedEventArgs e)
        {
            owned.IsWishListed = ChkBx_Wishlisted.IsChecked.Value;
        }

        #endregion
    }
}
