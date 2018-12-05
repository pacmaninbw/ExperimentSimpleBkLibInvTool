using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

/*
 * This class represents a row of data in a data table. Generally it will be used
 * to add a row to a database table.
 */
namespace ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel
{
    public abstract class DataTableItemBaseModel
    {
        /*
         * To save memory and correctly change the proper command parameter, only the
         * _sqlCmdParameters list contains SqlCmdParameters and the dictionaries provide
         * indexes into the command parameters list. To maintain good performance the
         * dictionaries are used rather than using a find in the list.
         */
        private List<SqlCmdParameter> _sqlCmdParameters;
        private Dictionary<string, int> _parameterIndexByPublicName;
        private Dictionary<string, int> _parameterIndexByDatabaseTableName;

        public bool IsValid { get { return _dataIsValid(); } }

        protected DataTableItemBaseModel()
        {
            _sqlCmdParameters = new List<SqlCmdParameter>();
            _parameterIndexByPublicName = new Dictionary<string, int>();
            _parameterIndexByDatabaseTableName = new Dictionary<string, int>();
        }

        /*
         * Sometimes the number of parameters in the stored procedure count doesn't
         * match the nummber of columns in the table. This function can be overriden
         * in those cases. Two examples of this are the Series and Books.
         */
        public virtual bool AddParameterToCommand(MySqlCommand cmd, string ParameterName)
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

        public ParameterDirection GetParameterDirection(string ParameterName)
        {
            ParameterDirection Direction = ParameterDirection.Input;
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                Direction = _sqlCmdParameters[tableIndex].Direction;
            }
            return Direction;
        }

        public MySqlDbType GetParameterType(string ParameterName)
        {
            MySqlDbType Type = MySqlDbType.String;
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                Type = _sqlCmdParameters[tableIndex].Type;
            }
            return Type;
        }

        public void SetParameterValue(string ParameterName, string value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value;
            }
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

        public void SetParameterValue(string ParameterName, uint value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value.ToString();
                _sqlCmdParameters[tableIndex].KeyValue = value;
            }
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

        public void SetParameterValue(string ParameterName, int value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value.ToString();
            }
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

        public void SetParameterValue(string ParameterName, bool value)
        {
            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].BValue = value;
            }
        }

        public bool GetParameterBValue(string ParameterName)
        {
            bool ParameterValue = false;

            int tableIndex = getParameterIndex(ParameterName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].BValue;
            }

            return ParameterValue;
        }

        public uint GetKeyValue()
        {
            uint KeyValue = 0;

            int tableIndex = getParameterIndex("ID");
            if (tableIndex >= 0)
            {
                KeyValue = _sqlCmdParameters[tableIndex].KeyValue;
            }

            return KeyValue;
        }

        public void SetKeyValue(uint KeyValue)
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

        public bool GetParameterIsRequired(string ParameterName)
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

            if (_parameterIndexByPublicName.TryGetValue(parameterName, out tableIndex))
            {
                parameterIndex = tableIndex;
            }
            else if (_parameterIndexByDatabaseTableName.TryGetValue(parameterName, out tableIndex))
            {
                parameterIndex = tableIndex;
            }
            else
            {
                string eMsg = "Programmer error in getParameterIndex(): Parameter not found: " + parameterName;
                MessageBox.Show(eMsg);
            }

            return parameterIndex;
        }

        protected void _addSqlCommandParameter(string PublicName, string DataBaseColumnName, string StoredProcedureParamName, MySqlDbType Type,
            bool IsRequired = false, ParameterDirection Direction = ParameterDirection.Input, bool SkipInsert=false)
        {
            if (string.IsNullOrEmpty(PublicName))
            {
                string eMsg = "Programmer error: PublicName is null or empty in _addSqlCommandParameter";
                MessageBox.Show(eMsg);
                return;
            }

            if (string.IsNullOrEmpty(DataBaseColumnName))
            {
                string eMsg = "Programmer error: DataBaseColumnName is null or empty in _addSqlCommandParameter";
                MessageBox.Show(eMsg);
                return;
            }

            if (string.IsNullOrEmpty(StoredProcedureParamName))
            {
                string eMsg = "Programmer error: SBParamName is null or empty in _addSqlCommandParameter";
                MessageBox.Show(eMsg);
                return;
            }

            SqlCmdParameter NewParameter = new SqlCmdParameter(PublicName, DataBaseColumnName, StoredProcedureParamName, Type, IsRequired, Direction, SkipInsert);
            _parameterIndexByPublicName.Add(PublicName, _sqlCmdParameters.Count);
            _parameterIndexByDatabaseTableName.Add(DataBaseColumnName, _sqlCmdParameters.Count);
            _sqlCmdParameters.Add(NewParameter);
        }

        protected abstract bool _dataIsValid();

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
