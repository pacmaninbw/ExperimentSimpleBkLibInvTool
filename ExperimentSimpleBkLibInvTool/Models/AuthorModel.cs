using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorModel : IAuthorModel
    {
        private string lastName;
        private string firstName;
        private string middleName;
        private string yearOfBirth;
        private string yearOfDeath;
        private bool errorWasReported;

        public string FirstName { get { return firstName; } }

        public string MiddleName { get { return middleName; } }

        public string LastName { get { return lastName; } }

        public string YearOfBirth { get { return yearOfBirth; } }

        public string YearOfDeath { get { return yearOfDeath; } }

        public bool IsValid { get { return _isValidData(); } }

        public AuthorModel()
        {
            lastName = null;
            firstName = null;
            middleName = null;
            yearOfBirth = null;
            yearOfDeath = null;
            errorWasReported = false;
        }

        public void SetFirstName(string textBoxInput)
        {
            if (textBoxInput == null || textBoxInput.Length < 1)
            {
                string errorMsg = "The first name of the author is a required field!";
                MessageBox.Show(errorMsg);
                errorWasReported = true;
            }
            else
            {
                firstName = textBoxInput;
            }
        }

        public void SetMiddleName(string textBoxInput)
        {
            middleName = textBoxInput;
        }

        public void SetLastName(string textBoxInput)
        {
            if (textBoxInput == null || textBoxInput.Length < 1)
            {
                string errorMsg = "The last name of the author is a required field!";
                MessageBox.Show(errorMsg);
                errorWasReported = true;
            }
            else
            {
                lastName = textBoxInput;
            }
        }

        public void SetYearOfBirth(string textBoxInput)
        {
            yearOfBirth = textBoxInput;
        }

        public void SetYearOfDeath(string textBoxInput)
        {
            yearOfDeath = textBoxInput;
        }

        protected bool _isValidData()
        {
            bool isValid = true;

            if (firstName == null || firstName.Length < 1)
            {
                isValid = false;
            }

            if (lastName == null || lastName.Length < 1)
            {
                isValid = false;
            }

            if (!isValid && !errorWasReported)
            {
                string errorMsg = "Add Series error: The first and last names of the author are required fields";
                MessageBox.Show(errorMsg);
            }

            return isValid;
        }
    }
}
