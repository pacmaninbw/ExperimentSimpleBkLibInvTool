﻿namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public interface IAuthorModel
    {
        string FirstName { get; }
        bool IsValid { get; }
        string LastName { get; }
        string MiddleName { get; }
        string YearOfBirth { get; }
        string YearOfDeath { get; }

        void SetFirstName(string textBoxInput);
        void SetLastName(string textBoxInput);
        void SetMiddleName(string textBoxInput);
        void SetYearOfBirth(string textBoxInput);
        void SetYearOfDeath(string textBoxInput);
    }
}