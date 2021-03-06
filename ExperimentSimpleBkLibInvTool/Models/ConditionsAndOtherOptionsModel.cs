﻿using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class ConditionsAndOtherOptionsModel : DataTableItemBaseModel
    {
        private readonly Model TheModel = ((App)Application.Current).Model;


        public ConditionsAndOtherOptionsModel() :
            base(((App)Application.Current).Model.ConditionsAndOptions)
        {
        }

        public ConditionsAndOtherOptionsModel(uint bookId, uint conditionId, uint statusId, string physicalDescription, bool signedByAuthor, bool isRead) :
            base(((App)Application.Current).Model.ConditionsAndOptions)
        {
            BookId = bookId;
            SetParameterValue("Condition Id", conditionId);
            SetParameterValue("Status Id", statusId);
            PhysicalCondition = physicalDescription;
            SignedByAuthor = signedByAuthor;
            Read = isRead;

            IsModified = false;       // Initialization is not modification.
        }

        public string Condition {
            get { return TheModel.ConditionsTable.ConditionTitle(GetParameterKValue("Condition Id")); }
            set { SetParameterValue("Condition Id", TheModel.ConditionsTable.ConditionKey(value)); }
        }

        public uint ConditionKey { get { return GetParameterKValue("Condition Id"); } }

        public string Status {
            get { return TheModel.StatusTable.StatusTitle(GetParameterKValue("Status Id")); }
            set { SetParameterValue("Status Id", TheModel.StatusTable.StatusKey(value)); }
        }

        public string PhysicalCondition {
            get { return GetParameterValue("Physical Description"); }
            set { SetParameterValue("Physical Description", value); }
        }

        public bool SignedByAuthor {
            get { return GetParameterBValue("Autographed"); }
            set { SetParameterValue("Autographed", value); }
        }

        public bool Read {
            get { return GetParameterBValue("Read"); }
            set { SetParameterValue("Read", value); }
        }

        public override bool AddToDb() => ((App)Application.Current).Model.ConditionsAndOptions.AddConditionsAndOptions(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.ConditionsAndOptions.UpdateConditionsAndOptions(this);

        public void Copy(ConditionsAndOtherOptionsModel original)
        {
            Condition = original.Condition;
            Status = original.Status;
            PhysicalCondition = original.PhysicalCondition;
            SignedByAuthor = original.SignedByAuthor;
            Read = original.Read;
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
