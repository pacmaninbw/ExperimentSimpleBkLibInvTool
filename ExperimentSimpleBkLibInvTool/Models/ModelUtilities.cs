using System;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public static class ModelUtilities
    {
        public static MySqlCommand AddParameter<T>(this MySqlCommand command, string name, MySqlDbType type, T value)
        {
            command.Parameters.Add(name, type);
            command.Parameters[name].Value = value;
            return command;
        }
    }
}
