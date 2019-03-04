using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

/*
 * There is a tight coupling between each model and the table it belongs to. This
 * is due to the models ability to add parameters to the tables call to the stored
 * procedure. This is only true when a model can be added to a table.
 * 
 * This class represents a row of data in a data table. Generally it will be used
 * to add a row to a database table.
 */
namespace ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel
{
    public abstract class DataTableItemBaseModel
    {
        private uint _bookId;
        /*
         * To save memory and correctly change the proper command parameter, only the
         * _sqlCmdParameters list contains SqlCmdParameters and the dictionaries provide
         * indexes into the command parameters list. To maintain good performance the
         * dictionaries are used rather than using a find in the list.
         */
        private List<SqlCmdParameter> _sqlCmdParameters;
        private Dictionary<string, int> _parameterIndexByPublicName;
        private Dictionary<string, int> _parameterIndexByDatabaseTableName;
        private Dictionary<string, int> _parameterIndexByParameterName;

        public bool IsValid { get { return _dataIsValid(); } }

        public uint KeyValue { get { return _bookId; } }

        public uint getBookID()
        {
            return _bookId;
        }

        public void setBookId(uint BookId)
        {
            _bookId = BookId;
        }

        protected abstract bool _dataIsValid();

        protected DataTableItemBaseModel(CDataTableModel DBInterfaceModel)
        {
            _bookId = 0;
            _sqlCmdParameters = new List<SqlCmdParameter>();
            List<SqlCmdParameter> sqlCmdParameters = DBInterfaceModel.SQLCommandParameters;
            foreach (SqlCmdParameter parameter in sqlCmdParameters)
            {
                SqlCmdParameter p = new SqlCmdParameter(parameter);
                _sqlCmdParameters.Add(p);
            }

            _parameterIndexByPublicName = new Dictionary<string, int>(DBInterfaceModel.PublicNameParameterIndex);
            _parameterIndexByParameterName = new Dictionary<string, int>(DBInterfaceModel.ParametersIndexByStoredProcedureName);
            _parameterIndexByDatabaseTableName = new Dictionary<string, int>();
            _parameterIndexByDatabaseTableName = new Dictionary<string, int>(DBInterfaceModel.ParametersIndexByDbColumnName);
        }

        /*
         * Sometimes the number of parameters in the stored procedure count doesn't
         * match the nummber of columns in the table. This function can be overriden
         * in those cases. Two examples of this are the Series and Books.
         */
        public bool AddParameterToCommand(MySqlCommand cmd, string ParameterName)
        {
            bool success = true;
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                MySqlParameterCollection parameters = cmd.Parameters;
                success = _sqlCmdParameters[tableIndex].AddParameterToCommand(cmd);
            }
            else
            {
                success = false;
            }
            return success;
        }

        public string GetParameterValue(string ParameterName)
        {
            string ParameterValue = "Failure";

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].Value;
            }

            return ParameterValue;
        }

        public uint GetParameterKValue(string ParameterName)
        {
            uint ParameterValue = 0;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].KeyValue;
            }

            return ParameterValue;
        }

        public int GetParameterIValue(string ParameterName)
        {
            int ParameterValue = -1;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterValue = Convert.ToInt32(_sqlCmdParameters[tableIndex].Value);
            }

            return ParameterValue;
        }

        protected ParameterDirection GetParameterDirection(string ParameterName)
        {
            ParameterDirection Direction = ParameterDirection.Input;
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                Direction = _sqlCmdParameters[tableIndex].Direction;
            }
            return Direction;
        }

        protected MySqlDbType GetParameterType(string ParameterName)
        {
            MySqlDbType Type = MySqlDbType.String;
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                Type = _sqlCmdParameters[tableIndex].Type;
            }
            return Type;
        }

        protected void SetParameterValue(string ParameterName, string value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value;
            }
        }

        protected void SetParameterValue(string ParameterName, uint value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value.ToString();
                _sqlCmdParameters[tableIndex].KeyValue = value;
            }
        }

        protected void SetParameterValue(string ParameterName, int value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value.ToString();
            }
        }

        protected void SetParameterValue(string ParameterName, bool value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].BValue = value;
            }
        }

        protected bool GetParameterBValue(string ParameterName)
        {
            bool ParameterValue = false;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].BValue;
            }

            return ParameterValue;
        }

        protected uint GetKeyValue()
        {
            uint KeyValue = 0;

            int tableIndex = getParameterIndex("ID");
            if (tableIndex >= 0)
            {
                KeyValue = _sqlCmdParameters[tableIndex].KeyValue;
            }

            return KeyValue;
        }

        protected void SetKeyValue(uint KeyValue)
        {
            int tableIndex = getParameterIndex("ID");
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].KeyValue = KeyValue;
            }
        }

        public bool GetParameterIsValid(string ParameterName)
        {
            bool ParameterIsValid = false;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterIsValid = _sqlCmdParameters[tableIndex].IsValid;
            }

            return ParameterIsValid;
        }

        protected bool GetParameterIsRequired(string ParameterName)
        {
            bool ParameterIsRequired = false;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterIsRequired = _sqlCmdParameters[tableIndex].IsRequired;
            }

            return ParameterIsRequired;
        }

        private int getParameterIndex(string parameterName)
        {
            int parameterIndex = -1;
            int tableIndex;

            if (_parameterIndexByParameterName.TryGetValue(parameterName, out tableIndex))
            {
                parameterIndex = tableIndex;
            }
            else if (_parameterIndexByPublicName.TryGetValue(parameterName, out tableIndex)) 
            {
                parameterIndex = tableIndex;
            }
            else if (_parameterIndexByDatabaseTableName.TryGetValue(parameterName, out tableIndex))
            {
                parameterIndex = tableIndex;
            }
#if DEBUG
            // ASSERT
            else
            {
                string eMsg = "Programmer error in getParameterIndex(): Parameter not found: " + parameterName;
                MessageBox.Show(eMsg, "Programmer Error:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
#endif

            return parameterIndex;
        }

        protected bool _defaultIsValid()
        {
            bool isValid = true;

            foreach (SqlCmdParameter parameter in _sqlCmdParameters)
            {
                isValid = parameter.IsValid;
                if (parameter.Direction == ParameterDirection.Input && !isValid)
                {
                    return isValid;
                }
            }

            return isValid;
        }
    }
}
