﻿using System.Windows;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddRatingsDlg.xaml
    /// </summary>
    public partial class AddRatingsDlg : Window
    {
        public AddRatingsDlg()
        {
            InitializeComponent();
            Ratings = new RatingsModel();
        }

        public RatingsModel Ratings { get; private set; }

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
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ratings = null;
            Close();
        }
    }
}
