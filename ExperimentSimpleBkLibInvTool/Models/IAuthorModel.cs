namespace pacsw.BookInventory.Models
{
    public interface IAuthorModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string YearOfBirth { get; set; }
        string YearOfDeath { get; set; }
    }
}