using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddBookDlg.xaml
    /// Build the book from the parts that the user enters, then save the book to the database.
    /// </summary>
    public partial class AddBookDlg : Window
    {
        private BookModel newBook;
        private OwnerShipModel owned;
        private ConditionsAndOtherOptionsModel options;
        private bool _genreSelected;
        private bool _formatSelected;

        public AuthorModel SelectedAuthor { get { return newBook.AuthorInfo; } set { newBook.AuthorInfo = value; } }

        public AddBookDlg()
        {
            InitializeComponent();
            newBook = new BookModel(false);
            PopulateSeriesSelector();
            InitCategorySelection();
            InitStatusSelection();
            InitFormatSelection();
            InitConditionSelection();
            Loaded += new RoutedEventHandler(bypassAuthorSelectionIfAuthorSelected);
            _formatSelected = false;
            _genreSelected = false;
            owned = null;
            options = null;
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

            if (!_genreSelected || string.IsNullOrEmpty(newBook.Genre))
            {
                LB_CategorySelector.Background = Brushes.Red;
                MessageBox.Show("A Genre is required for adding a new book!");
                hasErrors = true;
            }

            if (!_formatSelected || string.IsNullOrEmpty(newBook.Format))
            {
                LB_FormatSelector.Background = Brushes.Red;
                MessageBox.Show("A Format is required for adding a new book!");
                hasErrors = true;
            }

            if (owned != null)
            {
                newBook.Owned = owned;
            }

            if (options != null)
            {
                newBook.ConditionsAndOptions = options;
            }

            if (hasErrors)
            {
                // prevent duplicate error reporting
                return;
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

        private void SetAuthorNameValues()
        {
            AuthorModel selectedAuthor = newBook.AuthorInfo as AuthorModel;
            TB_SelectAuthorFirstName.Text = selectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = selectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = selectedAuthor.MiddleName;
            TB_SelectAuthorLastName.Background = Brushes.White;
            TB_SelectAuthorFirstName.Background = Brushes.White;
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

        private void HighLightAuthorAndReportError()
        {
            MessageBox.Show("The author is required for adding a new book!");
            TB_SelectAuthorLastName.Background = Brushes.Red;
            TB_SelectAuthorFirstName.Background = Brushes.Red;
//            FocusManager.SetFocusedElement(this, TB_SelectAuthorLastName);
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
            List<string> categories = ((App)Application.Current).Model.CategoryTable.ListBoxSelectionList();
            LB_CategorySelector.DataContext = categories;
            if (categories.Count < 1)
            {
                string mbMsg = "There are no genres, please add one?";
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
            _genreSelected = true;
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
                List<string> seriesTitles = ((App)Application.Current).Model.SeriesTable.SeriesSelectionListCreator(selectedAuthor);
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
            List<string> formats = ((App)Application.Current).Model.FormatTable.ListBoxSelectionList();
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
            LB_FormatSelector.Background = Brushes.White;
            newBook.Format = LB_FormatSelector.SelectedValue.ToString();
            _formatSelected = true;
        }

#endregion

#region Owned and Options

        private void CreateOwnedIfDoesntExist()
        {
            if (owned == null)
            {
                owned = new OwnerShipModel(false);
            }
        }

        private void ChkBx_BookIsOwned_Click(object sender, RoutedEventArgs e)
        {
            CreateOwnedIfDoesntExist();
            owned.IsOwned = ChkBx_BookIsOwned.IsChecked.Value;
        }

        private void ChkBx_Wishlisted_Click(object sender, RoutedEventArgs e)
        {
            CreateOwnedIfDoesntExist();
            owned.IsWishListed = ChkBx_Wishlisted.IsChecked.Value;
        }

        private void ChkBx_BookWasRead_Click(object sender, RoutedEventArgs e)
        {
            CreateOptionsIfDoesntExist();
            options.Read = ChkBx_BookWasRead.IsChecked.Value;
        }

        private void ChkBx_SignedByAuthor_Click(object sender, RoutedEventArgs e)
        {
            CreateOptionsIfDoesntExist();
            options.SignedByAuthor = ChkBx_SignedByAuthor.IsChecked.Value;
        }

        private void TXTBX_PhyscalDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            CreateOptionsIfDoesntExist();
            options.PhysicalCondition = TXTBX_PhyscalDescription.Text;
        }

        private void InitConditionSelection()
        {
            List<string> conditions = ((App)Application.Current).Model.ConditionsTable.ListBoxSelectionList();
            LB_ConditionSelector.DataContext = conditions;
            LB_ConditionSelector.Items.Clear();
            foreach (string condition in conditions)
            {
                LB_ConditionSelector.Items.Add(condition);
            }
        }

        private void LB_ConditionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateOptionsIfDoesntExist();
            options.Condition = LB_ConditionSelector.SelectedValue.ToString();
        }

        private void CreateOptionsIfDoesntExist()
        {
            if (options == null)
            {
                options = new ConditionsAndOtherOptionsModel();
            }
        }

        private void InitStatusSelection()
        {
            List<string> statuses = ((App)Application.Current).Model.StatusTable.ListBoxSelectionList();
            LB_StatusSelector.DataContext = statuses;
            LB_StatusSelector.Items.Clear();
            foreach (string status in statuses)
            {
                LB_StatusSelector.Items.Add(status);
            }
        }

        private void LB_StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateOptionsIfDoesntExist();
            options.Status = LB_StatusSelector.SelectedValue.ToString();
        }

        private void TXTBX_VolumeInSeries_LostFocus(object sender, RoutedEventArgs e)
        {
            VolumeInSeries volumeInSeries = new VolumeInSeries();
            int tmpVolumeNumber;
            if (int.TryParse(TXTBX_VolumeInSeries.Text, out tmpVolumeNumber))
            {
                volumeInSeries.VolumeNumber = tmpVolumeNumber;
                TXTBX_VolumeInSeries.Background = Brushes.White;
                newBook.VolumeNumber = volumeInSeries;
            }
            else
            {
                MessageBox.Show("Please Enter a number.", "Volume Number Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TXTBX_VolumeInSeries.Background = Brushes.Red;
            }
        }

        #endregion

        #region ForSale Info

        private void BTN_AddSalesInfo_Click(object sender, RoutedEventArgs e)
        {
            AddSalesInfoDlg addSalesInfoDlg = new AddSalesInfoDlg();
            addSalesInfoDlg.Closed += new EventHandler(GetSalesInfoFromDialog);
            addSalesInfoDlg.Show();
        }

        private void GetSalesInfoFromDialog(object sender, EventArgs e)
        {
            AddSalesInfoDlg addSalesInfoDlg = sender as AddSalesInfoDlg;
            newBook.ForSale = addSalesInfoDlg.SalesInfo;
        }

        #endregion

        private void BTN_Synopsis_Click(object sender, RoutedEventArgs e)
        {
            AddBookSummary addSummaryDlg = new AddBookSummary();
            addSummaryDlg.Closed += new EventHandler(GetSynopsisFromDialog);
            addSummaryDlg.Show();
        }
        private void GetSynopsisFromDialog(object sender, EventArgs e)
        {
            AddBookSummary addSummaryDlg = sender as AddBookSummary;
            newBook.Summary = addSummaryDlg.Summary;
        }

    }
}
