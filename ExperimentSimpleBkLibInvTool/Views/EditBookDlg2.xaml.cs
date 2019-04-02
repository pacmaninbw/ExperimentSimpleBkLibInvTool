﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for EditBookDlg2.xaml
    /// </summary>
    public partial class EditBookDlg2 : Window
    {
        private OwnerShipModel owned;
        private ConditionsAndOtherOptionsModel options;

        private bool ownedIsDirty;
        private bool optionsIsDirty;

        public BookModel ThisBook { get; set; }

        public EditBookDlg2()
        {
            InitializeComponent();
            InitConditionSelection();
            InitStatusSelection();
            Loaded += new RoutedEventHandler(LoadPreviousValues);
            // The owned and options data is edited in the main editor and requires different handling
            // from the other data that is edited in it's own popup editor.
            owned = null;
            options = null;
            ownedIsDirty = false;
            optionsIsDirty = false;
        }

        private void Btn_AddBookSave_Click(object sender, RoutedEventArgs e)
        {
            if (ownedIsDirty)
            {
                ThisBook.Owned = owned;
            }

            if (optionsIsDirty)
            {
                ThisBook.ConditionsAndOptions = options;
            }

            if (ThisBook.IsValid)
            {
                if (ThisBook.UpdateBookWithEdits())
                {
                    Close();
                }
            }
        }

        private void Btn_AddBookCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            TB_BookTitle.Text = ThisBook.Title;
            SetAuthorNameValues();

            owned = (OwnerShipModel)ThisBook.Owned;
            if (owned != null)
            {
                LoadPreviousOwnedValues();
            }

            options = (ConditionsAndOtherOptionsModel)ThisBook.ConditionsAndOptions;
            if (options != null)
            {
                LoadPreviousOptionValues();
            }
        }

        private void LoadPreviousOwnedValues()
        {
            ChkBx_BookIsOwned.IsChecked = owned.IsOwned;
            ChkBx_Wishlisted.IsChecked = owned.IsWishListed;
        }

        private void LoadPreviousOptionValues()
        {
            ChkBx_SignedByAuthor.IsChecked = options.SignedByAuthor;
            ChkBx_BookWasRead.IsChecked = options.Read;
            TXTBX_PhyscalDescription.Text = options.PhysicalCondition;
        }

        private void SetAuthorNameValues()
        {
            AuthorModel selectedAuthor = ThisBook.AuthorInfo as AuthorModel;
            TB_SelectAuthorFirstName.Text = selectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = selectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = selectedAuthor.MiddleName;
            PopulateSeriesSelector();
        }

        #region Publishing Info

        private void BTN_AddPublishingInfo_Click(object sender, RoutedEventArgs e)
        {
            AddPublishingInformation addPublishinginfoDlg = new AddPublishingInformation();
            addPublishinginfoDlg.Closed += new EventHandler(GetPublishingInfoFromDialog);
            addPublishinginfoDlg.PublishInfo = (PublishInfoModel)ThisBook.PublishInfo;
            addPublishinginfoDlg.Show();
        }

        private void GetPublishingInfoFromDialog(object sender, EventArgs e)
        {
            AddPublishingInformation addPublishinginfoDlg = sender as AddPublishingInformation;
            if (!addPublishinginfoDlg.Cancelled)
            {
                ThisBook.PublishInfo = addPublishinginfoDlg.PublishInfo;
            }
        }

        #endregion

        #region Purchase Info

        private void GetPurchasingInfoFromDialog(object sender, EventArgs e)
        {
            PurchasingDialog addPurchasingDlg = sender as PurchasingDialog;
            if (!addPurchasingDlg.Cancelled)
            {
                ThisBook.PuchaseInfo = addPurchasingDlg.PurchaseInfo;
            }
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
            addRatingsDlg.Ratings = ThisBook.Ratings as RatingsModel;
            addRatingsDlg.Show();
        }

        private void GetRatingsFromAddRatings(object sender, EventArgs e)
        {
            AddRatingsDlg addRatingsDlg = (AddRatingsDlg)sender;
            if (!addRatingsDlg.Cancelled)
            {
                ThisBook.Ratings = addRatingsDlg.Ratings;
            }
        }

        #endregion

        #region Series Selection

        private void LB_SeriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorModel selectedAuthor = ThisBook.AuthorInfo as AuthorModel;

            if (selectedAuthor != null && selectedAuthor.IsValid)
            {
                ThisBook.SeriesInfo = new SeriesModel(selectedAuthor, LB_SeriesSelector.SelectedValue.ToString());
            }
        }

        private void PopulateSeriesSelector()
        {
            AuthorModel selectedAuthor = ThisBook.AuthorInfo as AuthorModel;

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

        #region Owned and Options

        private void CreateOwnedIfDoesntExist()
        {
            ownedIsDirty = true;
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
            optionsIsDirty = true;
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
                ThisBook.VolumeNumber = volumeInSeries;
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
            addSalesInfoDlg.SalesInfo = (ForSaleModel)ThisBook.ForSale;
            addSalesInfoDlg.Show();
        }

        private void GetSalesInfoFromDialog(object sender, EventArgs e)
        {
            AddSalesInfoDlg addSalesInfoDlg = sender as AddSalesInfoDlg;
            if (!addSalesInfoDlg.Cancelled)
            {
                ThisBook.ForSale = addSalesInfoDlg.SalesInfo;
            }
        }

        #endregion

        #region Summary

        private void BTN_Synopsis_Click(object sender, RoutedEventArgs e)
        {
            AddBookSummary addSummaryDlg = new AddBookSummary();
            addSummaryDlg.Closed += new EventHandler(GetSynopsisFromDialog);
            addSummaryDlg.Summary = ThisBook.Summary;
            addSummaryDlg.Show();
        }

        private void GetSynopsisFromDialog(object sender, EventArgs e)
        {
            AddBookSummary addSummaryDlg = sender as AddBookSummary;
            if (!addSummaryDlg.Cancelled)
            {
                ThisBook.Summary = addSummaryDlg.Summary;
            }
        }

        #endregion
    }
}