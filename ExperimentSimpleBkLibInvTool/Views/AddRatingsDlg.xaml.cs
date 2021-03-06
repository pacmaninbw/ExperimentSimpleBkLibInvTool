﻿using System.Windows;
using System.ComponentModel;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddRatingsDlg.xaml
    /// </summary>
    public partial class AddRatingsDlg : Window
    {
        private bool _saveClicked;

        public AddRatingsDlg()
        {
            InitializeComponent();
            Ratings = null;
            Cancelled = false;
            Loaded += new RoutedEventHandler(LoadPreviousValues);
            _saveClicked = false;
            Closing += AddRatingsDlg_Closing;
        }

        public RatingsModel Ratings { get; set; }

        public bool Cancelled { get; private set; }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            if (Ratings == null)
            {
                Ratings = new RatingsModel();
            }
            else
            {
                TB_MyRating.Text = Ratings.MyRating;
                TB_AmazonRating.Text = Ratings.AmazonRating;
                TB_GoodReadsRating.Text = Ratings.GoodReadsRating;

            }
        }

        private void TB_MyRating_LostFocus(object sender, RoutedEventArgs e)
        {
            Ratings.MyRating = TB_MyRating.Text;
        }

        private void TB_AmazonRating_LostFocus(object sender, RoutedEventArgs e)
        {
            Ratings.AmazonRating = TB_AmazonRating.Text;
        }

        private void TB_GoodReadsRating_LostFocus(object sender, RoutedEventArgs e)
        {
            Ratings.GoodReadsRating = TB_GoodReadsRating.Text;
        }

        private void Btn_AddRatingsSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Ratings.IsValid)
            {
                MessageBox.Show("One or more of the Rating values is not correct", "Ratings Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _saveClicked = true;
                Close();
            }
        }

        private void Btn_AddRatingsCancel_Click(object sender, RoutedEventArgs e)
        {
            Ratings = null;
            Cancelled = true;
            Close();
        }

        private void AddRatingsDlg_Closing(object sender, CancelEventArgs e)
        {
            if (!_saveClicked)
            {
                Ratings = null;
                Cancelled = true;
            }
        }
    }
}
