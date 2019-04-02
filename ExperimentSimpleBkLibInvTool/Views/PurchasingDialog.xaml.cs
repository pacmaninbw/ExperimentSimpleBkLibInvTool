﻿using System.Windows;
using System.Windows.Controls;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for PurchasingDialog.xaml
    /// </summary>
    public partial class PurchasingDialog : Window
    {
        public PurchasingDialog()
        {
            InitializeComponent();
            PurchaseInfo = null;
            Cancelled = false;
            Loaded += new RoutedEventHandler(LoadPreviousValues);
        }

        public PuchaseInfoModel PurchaseInfo { get; set; }

        public bool Cancelled { get; private set; }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            if (PurchaseInfo == null)
            {
                PurchaseInfo = new PuchaseInfoModel();
            }
            else
            {
                TB_Vendor.Text = PurchaseInfo.Vendor;
                TB_ListPrice.Text = PurchaseInfo.ListPrice;
                TB_PaidPrice.Text = PurchaseInfo.PaidPrice;
            }
        }

        private void BTN_PurchaseInfoSave_Click(object sender, RoutedEventArgs e)
        {
            if (PurchaseInfo.IsValid)
            {
                Close();
            }
        }

        private void BTN_PurchaseInfoCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancelled = true;
            PurchaseInfo = null;
            Close();
        }

        private void TB_Vendor_LostFocus(object sender, RoutedEventArgs e)
        {
            PurchaseInfo.Vendor = TB_Vendor.Text;
        }

        private void TB_ListPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            PurchaseInfo.ListPrice = TB_ListPrice.Text;
        }

        private void TB_PaidPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            PurchaseInfo.PaidPrice = TB_PaidPrice.Text;
        }

        private void DP_DatePurchased_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            PurchaseInfo.PurchaseDate = DP_DatePurchased.SelectedDate.Value.Date;
        }
    }
}
