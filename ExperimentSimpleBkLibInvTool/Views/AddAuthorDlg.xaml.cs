using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddAuthorDlg.xaml
    /// </summary>
    public partial class AddAuthorDlg : Window
    {
        public AddAuthorDlg()
        {
            InitializeComponent();
        }

        private void Btn_AddAuthorSave_Click(object sender, RoutedEventArgs e)
        {
            AuthorModel newAuthor = new AuthorModel();
            newAuthor.SetFirstName(TxtBx_AuthorFirstName.Text);
            newAuthor.SetLastName(TxtBx_AuthorLastName.Text);
            newAuthor.SetMiddleName(TxtBx_AuthorMiddleName.Text);
            newAuthor.SetYearOfBirth(TxtBx_AuthorYearOfBirth.Text);
            newAuthor.SetYearOfDeath(TxtBx_AuthorYearOfDeath.Text);

            if (newAuthor.IsValid)
            {
                AuthorTableModel authorTableModel = ((App)Application.Current).Model.AuthorTable;
                if (authorTableModel.AddAuthor(newAuthor))
                {
                    Close();
                }
            }
        }
    }
}
