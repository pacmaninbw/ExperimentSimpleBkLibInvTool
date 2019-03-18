using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class CategoryTableModel : DictionaryTableModel
    {
        public DataTable CategoryTable { get { return DataTable; } }

        public CategoryTableModel() : base("bookcategories", "getAllBookCategoriesWithKeys", "addCategory")
        {
        }

        public string CategoryTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint CategoryKey(string CategoryTitle)
        {
            return NameToKey(CategoryTitle);
        }

        public void AddCategory(CategoryModel Category)
        {
            AddItemToDictionary(Category);
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Name", GetDBColumnData("CategoryName"), parameters["@categoryName"]);
            _addSqlCommandParameter("Primary Key", GetDBColumnData("idBookCategories"), parameters["@primaryKey"]);
        }
    }
}
