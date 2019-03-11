﻿namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Options
{
    public interface IConditionsAndOtherOptionsModel
    {
        string Condition { get; set; }

        string PhysicalCondition { get; set; }

        bool Read { get; set; }

        bool SignedByAuthor { get; set; }

        string Status { get; set; }
    }
}