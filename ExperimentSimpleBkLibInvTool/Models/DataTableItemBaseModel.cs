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
        protected List<SqlCmdParameter> _sqlCmdParameters;
        protected Dictionary<string, int> _parameterIndexByPublicName;
        protected Dictionary<string, int> _parameterIndexByDatabaseTableName;

        public bool IsValid { get { return _dataIsValid(); } }

        protected DataTableItemBaseModel()
        {
            _sqlCmdParameters = new List<SqlCmdParameter>();
            _parameterIndexByPublicName = new Dictionary<string, int>();
            _parameterIndexByDatabaseTableName = new Dictionary<string, int>();
        }

        public bool AddParameterToCommand(MySqlCommand cmd, string DBParameterName)
        {
            bool success = true;
            int tableIndex = getParameterIndexFromDBColumn(DBParameterName);
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

        public ParameterDirection GetParameterDirection(string DBParameterName)
        {
            ParameterDirection Direction = ParameterDirection.Input;
            int tableIndex = getParameterIndexFromDBColumn(DBParameterName);
            if (tableIndex >= 0)
            {
                Direction = _sqlCmdParameters[tableIndex].Direction;
            }
            return Direction;
        }

        public MySqlDbType GetParameterType(string DBParameterName)
        {
            MySqlDbType Type = MySqlDbType.String;
            int tableIndex = getParameterIndexFromDBColumn(DBParameterName);
            if (tableIndex >= 0)
            {
                Type = _sqlCmdParameters[tableIndex].Type;
            }
            return Type;
        }

        public void SetParameterValue(string UserColumnName, string value)
        {
            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].Value = value;
            }
        }

        public string GetParameterValue(string UserColumnName)
        {
            string ParameterValue = "Failure";

            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].Value;
            }

            return ParameterValue;
        }

        public void SetParameterValue(string UserColumnName, bool value)
        {
            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                _sqlCmdParameters[tableIndex].BValue = value;
            }
        }

        public bool GetParameterBValue(string UserColumnName)
        {
            bool ParameterValue = false;

            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                ParameterValue = _sqlCmdParameters[tableIndex].BValue;
            }

            return ParameterValue;
        }

        public bool GetParameterIsValid(string UserColumnName)
        {
            bool ParameterIsValid = false;

            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                ParameterIsValid = _sqlCmdParameters[tableIndex].IsValid;
            }

            return ParameterIsValid;
        }

        public bool GetParameterIsRequired(string UserColumnName)
        {
            bool ParameterIsRequired = false;

            int tableIndex = getParameterIndexFromPublicName(UserColumnName);
            if (tableIndex >= 0)
            {
                ParameterIsRequired = _sqlCmdParameters[tableIndex].IsRequired;
            }

            return ParameterIsRequired;
        }

        protected int getParameterIndexFromPublicName(string UserColumnName)
        {
            int ParaIndex = -1;

            if (_parameterIndexByPublicName.TryGetValue(UserColumnName, out int tableIndex))
            {
                ParaIndex = tableIndex;
            }
            else
            {
                string eMsg = "Programmer error: Parameter not found: " + UserColumnName;
                MessageBox.Show(eMsg);
            }

            return ParaIndex;
        }

        protected int getParameterIndexFromDBColumn(string DBParameterName)
        {
            int ParaIndex = -1;

            if (_parameterIndexByDatabaseTableName.TryGetValue(DBParameterName, out int tableIndex))
            {
                ParaIndex = tableIndex;
            }
            else
            {
                string eMsg = "Programmer error: Parameter not found: " + DBParameterName;
                MessageBox.Show(eMsg);
            }

            return ParaIndex;
        }

        protected void _addSqlCommandParameter(string PublicName, string DataBaseColumnName, string SBParamName, MySqlDbType Type, bool IsRequired = false, ParameterDirection Direction = ParameterDirection.Input, bool SkipInsert=false)
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

            SqlCmdParameter NewParameter = new SqlCmdParameter(PublicName, DataBaseColumnName, SBParamName, Type, IsRequired, Direction, SkipInsert);
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
