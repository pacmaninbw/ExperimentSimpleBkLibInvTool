﻿using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddFormatDlg.xaml
    /// </summary>
    public partial class AddFormatDlg : Window
    {
        public AddFormatDlg()
        {
            InitializeComponent();
        }

        private void Btn_AddFormatSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBx_FormatName.Text))
            {
                MessageBox.Show("Please enter a category name before clicking the Save button.");
            }
            else
            {
                FormatModel format = new FormatModel(TxtBx_FormatName.Text);
                FormatTableModel formats = ((App)Application.Current).Model.FormatModel;
                formats.AddFormat(format);
                Close();
            }
        }
    }
}