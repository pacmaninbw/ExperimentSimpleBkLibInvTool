using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorModel : IAuthorModel
    {
        private string lastName;
        private string firstName;
        private string middleName;
        private string yearOfBirth;
        private string yearOfDeath;

        public string FirstName { get { return firstName; } }

        public string MiddleName { get { return middleName; } }

        public string LastName { get { return lastName; } }

        public string YearOfBirth { get { return yearOfBirth; } }

        public string YearOfDeath { get { return yearOfDeath; } }

        public AuthorModel()
        {
            lastName = null;
            firstName = null;
            middleName = null;
            yearOfBirth = null;
            yearOfDeath = null;
        }

        public void SetFirstName(string textBoxInput)
        {
            firstName = textBoxInput;
        }

        public void SetMiddleName(string textBoxInput)
        {
            middleName = textBoxInput;
        }

        public void SetLastName(string textBoxInput)
        {
            lastName = textBoxInput;
        }

        public void SetYearOfBirth(string textBoxInput)
        {
            yearOfBirth = textBoxInput;
        }

        public void SetYearOfDeath(string textBoxInput)
        {
            yearOfDeath = textBoxInput;
        }
    }
}
