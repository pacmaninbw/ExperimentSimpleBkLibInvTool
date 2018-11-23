using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Authorstab
    {
        public Authorstab()
        {
            Bookinfo = new HashSet<Bookinfo>();
            Series = new HashSet<Series>();
        }

        public int IdAuthors { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string YearOfBirth { get; set; }

        public string YearOfDeath { get; set; }

        public ICollection<Bookinfo> Bookinfo { get; set; }

        public ICollection<Series> Series { get; set; }
    }
}
