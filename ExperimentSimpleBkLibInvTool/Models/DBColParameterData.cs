using System.Data;

namespace pacsw.BookInventory.Models
{
    public class DbColumnParameterData
    {
        public DbColumnParameterData(DataRow ColumnData)
        {
            bool parseWorked = true;
            ColumnName = ColumnData[0].ToString();
            parseWorked = int.TryParse(ColumnData[1].ToString(), out int ordinalPosition);
            Ordinal_Posistion = ordinalPosition;
            IsNullable = true;
        }

        public DbColumnParameterData(string columnName, int ordinal_Posistion, bool isNullable)
        {
            ColumnName = columnName;
            Ordinal_Posistion = ordinal_Posistion;
            IsNullable = isNullable;
        }

        public string ColumnName { get; private set; }

        public int Ordinal_Posistion { get; private set; }

        public bool IsNullable { get; private set; }

        public int IndexBasedOnOrdinal { get { return Ordinal_Posistion - 1; } }
    }
}
