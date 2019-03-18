using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

/*
 * This class is used to generate SQL command parameters to a call of a 
 * stored procedure.
 * 
 * This class is a data value for a single column in a single row of data.
 * Incoming data will generally be user input and there will be 2 forms of input, either
 * a string from a text field or a boolean value from a checkbox.
 * 
 * During the creation of the SQL command parameter the data will be returned as the proprer
 * type for the stored procedure. The coversion from input string to the expected SQL type
 * will occur during the input phase as an additional check on the validity of the input.
 */
namespace pacsw.BookInventory.Models
{
    public class SqlCmdParameter 
    {
        protected string _publicName;           // The name the user knows this field by
        protected string _dataBaseColumnName;
        protected string _storedProcedureParameterName;
        protected ParameterDirection _direction;
        protected int _valueInt;
        protected string _value;                // used for input as the basis of the conversion, and storage for string parameters.
        protected double _valueDouble;
        protected uint _valueKey;
        protected bool _isRequired;             // Is this field required to have a value in the database? This is used in the validity check
        protected MySqlDbType _type;
        protected bool _isValueSet;             // internal, used in the validity check
        protected bool _skipInsertOfPrimaryKey;

        public SqlCmdParameter(string PublicName, string DataBaseColumnName, string SBParamName, MySqlDbType Type, bool IsRequired = false, ParameterDirection Direction=ParameterDirection.Input, bool SkipInserOfPrimaryKey=false)
        {
            if (string.IsNullOrEmpty(PublicName))
            {
                ArgumentNullException ex = new ArgumentNullException("PublicName");
                throw ex;
            }

            if (string.IsNullOrEmpty(SBParamName))
            {
                ArgumentNullException ex = new ArgumentNullException("SBParamName");
                throw ex;
            }

            switch (Type)
            {
                case MySqlDbType.Int16:
                case MySqlDbType.Int32:
                case MySqlDbType.Double:
                case MySqlDbType.String:
                case MySqlDbType.UInt32:
                case MySqlDbType.Byte:      // TinyInt for boolean representation
                    break;
                case MySqlDbType.VarChar:
                case MySqlDbType.Date:
                    // In the user interface handle VarChar as a string.
                    Type = MySqlDbType.String;
                    break;

                default:
                    ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("Type");
                    throw ex;
            }

            _publicName = PublicName;
            _storedProcedureParameterName = SBParamName;
            _direction = Direction;
            _isRequired = IsRequired;
            _type = Type;
            _isValueSet = false;
            _value = null;
            _valueKey = 0;
            _valueInt = 0;
            _valueDouble = 0.0;
            _skipInsertOfPrimaryKey = SkipInserOfPrimaryKey;
        }

        public SqlCmdParameter(SqlCmdParameter original)
        {
            _publicName = original._publicName;
            _storedProcedureParameterName = original._storedProcedureParameterName;
            _direction = original._direction;
            _isRequired = original._isRequired;
            _type = original._type;
            _isValueSet = original._isValueSet;
            _value = original._value;
            _valueKey = original._valueKey;
            _valueInt = original._valueInt;
            _valueDouble = original._valueDouble;
            _skipInsertOfPrimaryKey = original._skipInsertOfPrimaryKey;
        }

        public string PublicName
        {
            get { return _publicName; }
        }

        public ParameterDirection Direction
        {
            get { return _direction;  }
            set { _direction = value; }
        }

        public bool IsValid { get { return _dataIsValid(); } }

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public string Value
        {
            get { return _value; }
            set { SetValue(value); }
        }

        public bool BValue
        {
            get { return (_valueInt > 0); }
            set { SetValue(value); }
        }

        public uint KeyValue
        {
            get { return _valueKey; }
            set { _valueKey = value; }
        }

        public MySqlDbType Type
        {
            get { return _type; }
        }

        public bool AddParameterToCommand(MySqlCommand cmd)
        {
            if (_skipInsertOfPrimaryKey)
            {
                return true;
            }

            // If it is an output variable validity doesn't matter.
            if (_direction != ParameterDirection.Input)
            {
                string IndexByNameValue = _storedProcedureParameterName;
                cmd.Parameters.Add(new MySqlParameter(IndexByNameValue, _type));
                cmd.Parameters[IndexByNameValue].Direction = _direction;
                return true;
            }

            if (!IsValid)
            {
                return IsValid;
            }

            switch (_type)
            {
                case MySqlDbType.Byte:
                case MySqlDbType.Int16:
                case MySqlDbType.Int32:
                    cmd.Parameters.AddWithValue(_storedProcedureParameterName, _valueInt);
                    break;
                case MySqlDbType.Double:
                    cmd.Parameters.AddWithValue(_storedProcedureParameterName, _valueDouble);
                    break;
                case MySqlDbType.UInt32:
                    cmd.Parameters.AddWithValue(_storedProcedureParameterName, _valueKey);
                    break;
                case MySqlDbType.String:
                    cmd.Parameters.AddWithValue(_storedProcedureParameterName, _value);
                    break;
            }
            return true;
        }

        protected void SetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            _value = value;

            string eMsg = null;
            switch (_type)
            {
                case MySqlDbType.Int16:
                case MySqlDbType.Byte:
                    bool tmp = false;
                    if (!bool.TryParse(_value, out tmp))
                    {
                        eMsg = _publicName + ": Value is not True or False";
                    }
                    _valueInt = (tmp) ? 1 : 0;
                    break;
                case MySqlDbType.Int32:
                    if (!int.TryParse(_value, out _valueInt))
                    {
                        eMsg = _publicName + ": Value is not in the proper format of an integer";
                    }
                    break;
                case MySqlDbType.Double:
                    if (!double.TryParse(_value, out _valueDouble))
                    {
                        eMsg = _publicName + ": Value is not in the proper format of an floating point number";
                    }
                    break;
                case MySqlDbType.UInt32:
                    _valueKey = Convert.ToUInt32(value);
                    if (!uint.TryParse(_value, out _valueKey))
                    {
                        eMsg = _publicName + ": Value is not in the proper format of an unsigned integer";
                    }
                    break;
                case MySqlDbType.String:
                default:
                    break;
            }

            if (eMsg != null)
            {
                MessageBox.Show(eMsg);
                _isValueSet = false;
            }
            else
            {
                _isValueSet = true;
            }
        }

        protected void SetValue(bool InVal)
        {
            _value = (InVal) ? "true" : "false";
            if (_type == MySqlDbType.Int16 || _type == MySqlDbType.Byte)
            {
                _valueInt = (InVal) ? 1 : 0;
            }

            _isValueSet = true;
        }

        protected bool _dataIsValid()
        {
            bool dataIsValid = true;

            if (_direction == ParameterDirection.Input && _isRequired && !_isValueSet)
            {
                    dataIsValid = false;
            }

            return dataIsValid;
        }

    }
}
