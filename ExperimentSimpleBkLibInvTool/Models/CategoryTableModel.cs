using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Category
{
    public class CategoryTableModel : DictionaryTableModel
    {
        public DataTable CategoryTable
        {
            get { return DataTable; }
        }

        public CategoryTableModel()
        {
            _getTableStoredProcedureName = "getAllBookCategoriesWithKeys";
            _addItemStoredProcedureName = "addCategory";
            _firstParameterName = "categoryName";
            _lastParameterName = "primaryKey";

            InitializeDictionaries();
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
            AddItemToDicionary(Category);
        }
    }
}
