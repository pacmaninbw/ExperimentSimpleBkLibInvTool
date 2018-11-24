﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC
{
    public class Model
    {
        private SeriesTableModel _series;
        private CategoryTableModel _categories;
        private FormatTableModel _formats;
        private BookTableModel _books;
        private AuthorTableModel _authors;

        public Model()
        {
            _series = new SeriesTableModel();
            _formats = new FormatTableModel();
            _categories = new CategoryTableModel();
            _books = new BookTableModel();
            _authors = new AuthorTableModel();
        }

        public FormatTableModel FormatModel
        {
            get
            {
                return _formats;
            }
        }

        public CategoryTableModel CategoryTable
        {
            get
            {
                return _categories;
            }
        }

        public SeriesTableModel SeriesModel
        {
            get
            {
                return _series;
            }
        }

        public BookTableModel BookTable
        {
            get
            {
                return _books;
            }
        }

        public AuthorTableModel AuthorTable
        {
            get
            {
                return _authors;
            }
        }
    }
}