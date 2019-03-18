using System.Windows;
using pacsw.BookInventory.Models.Author;

namespace pacsw.BookInventory.Views
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
            newAuthor.FirstName = TxtBx_AuthorFirstName.Text;
            newAuthor.LastName = TxtBx_AuthorLastName.Text;
            newAuthor.MiddleName = TxtBx_AuthorMiddleName.Text;
            newAuthor.YearOfBirth = TxtBx_AuthorYearOfBirth.Text;
            newAuthor.YearOfDeath = TxtBx_AuthorYearOfDeath.Text;

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
