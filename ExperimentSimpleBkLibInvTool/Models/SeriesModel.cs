﻿using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public class SeriesModel : DataTableItemBaseModel, ISeriesModel
    {
        private string title;
        private int authorId;
        private AuthorModel _author;

        public AuthorModel Author { get { return _author; }  }

        public string SeriesTitle { get { return title; } }

        public SeriesModel()
        {
            authorId = 0;
            title = null;
            _author = null;
        }

        public SeriesModel(AuthorModel author)
        {
            authorId = 0;
            _author = author;
            title = null;
        }

        public void SetAuthor(AuthorModel author)
        {
            _author = author;
        }

        public void  SetTitle(string seriesTitle)
        {
            title = seriesTitle;
        }

        public int getAuthorId()
        {
            return authorId;
        }

        public void setAuthorId(int AuthorId)
        {
            authorId = AuthorId;
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;
            if (_author == null)
            {
                dataIsValid = false;
            }
            else
            {
                dataIsValid = _author.IsValid;
                if (string.IsNullOrEmpty(title))
                {
                    string errorMsg = "Add Series error: Missing title of series";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }
            }
            return dataIsValid;
        }
    }

    public class SeriesTableModel : CDataTableModel
    {
        public SeriesTableModel()
        {
            _getTableStoredProcedureName = "getAllSeriesData";
            _addItemStoredProcedureName = "addAuthorSeries";
        }

        public DataTable Series
        {
            get
            {
                return DataTable;
            }
        }

        public bool AddSeries(ISeriesModel iSeriesData)
        {
            SeriesModel seriesModel = (SeriesModel)iSeriesData;
            bool canInsertData = seriesModel.IsValid;

            if (canInsertData) {
                canInsertData = _dbAddSeries(seriesModel);
            }

            return canInsertData;
        }

        private bool _dbAddSeries(SeriesModel SeriesData)
        {
            bool AddSeriesSuccess = true;
            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = _addItemStoredProcedureName;
                        cmd.Parameters.AddWithValue("authorFirst", SeriesData.Author.FirstName);
                        cmd.Parameters.AddWithValue("authorLast", SeriesData.Author.LastName);
                        cmd.Parameters.AddWithValue("seriesTitle", SeriesData.SeriesTitle);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    AddSeriesSuccess = false;
                }
            }
            return AddSeriesSuccess;
        }
    }
}
