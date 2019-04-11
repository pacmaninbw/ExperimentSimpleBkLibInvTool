using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class BookTableModel : CDataTableModel
    {
        public DataTable BookTable
        {
            get { return DataTable; }
        }

        public BookTableModel() : base("bookinfo", "getallbooks")
        {
        }

        public uint InsertTitleIfNotInTable(string title)
        {
            uint titleKey = GetTitleKey(title);

            if (titleKey < 1)
            {
                InsertTitleString(title);
                titleKey = GetTitleKey(title);
            }

            return titleKey;
        }

        public void InsertTitleString(string title)
        {
            string SqlInsert = "INSERT INTO title (title.TitleStr) VALUES('" + title + "');";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlInsert;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }
        }

        public uint GetTitleKey(string title)
        {
            uint titleKey = 0;
            string SqlQuery = "SELECT title.idTitle FROM title WHERE title.TitleStr = @title;";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                int ResultCount = 0;
                DataTable Dt = new DataTable();
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlQuery;
                        cmd.AddParameter("@title", MySqlDbType.String, title);

                        cmd.ExecuteNonQuery();
                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
                        if (ResultCount > 0)
                        {
                            titleKey = Dt.Rows[0].Field<uint>(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    titleKey = 0;
                }
            }

            return titleKey;
        }

        public string GetTitle(uint titleKey)
        {
            string title = "";
            string SqlQuery = "SELECT title.TitleStr FROM title WHERE title.idTitle = @titleid;";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    int ResultCount = 0;
                    DataTable Dt = new DataTable();
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlQuery;
                        cmd.AddParameter("@titleid", MySqlDbType.UInt32, titleKey);

                        cmd.ExecuteNonQuery();
                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
                    }
                    if (ResultCount > 0)
                    {
                        title = Dt.Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }

            return title;
        }

        public bool DeleteBook(uint bookId)
        {
            bool deleted = true;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "deleteBookById";
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "Database Error: " + ex.Message;
                MessageBox.Show(errorMsg);
                deleted = false;
            }

            return deleted;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;
        }

    }
}
