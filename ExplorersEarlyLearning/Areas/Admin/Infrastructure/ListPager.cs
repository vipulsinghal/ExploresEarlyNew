using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorers.Web.Areas.Admin.Infrastructure
{
    public class ListPager
    {
        #region Constants

        private const int DefaultMaxDisplayedPages = 10;
        private const int DefaultPageSize = 15;
        private const string DefaultPageQueryParameter = "PageNo";

        #endregion

        #region Fields

        private readonly int _itemsCount;
        private int _maxDisplayedPages;
        private int _pageSize;

        #endregion

        #region Properties

        /// <summary>
        /// Count of displayed pages
        /// </summary>
        public int MaxDisplayedPages
        {
            get { return _maxDisplayedPages == 0 ? DefaultMaxDisplayedPages : _maxDisplayedPages; }
            set { _maxDisplayedPages = value; }
        }

        /// <summary>
        /// Items per page
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {

                _pageSize = value;
                PageCount = (int)(Math.Ceiling(_itemsCount / (double)_pageSize));

                if (CurrentPage > PageCount)
                    CurrentPage = PageCount;

                StartDisplayedPage = (CurrentPage - MaxDisplayedPages / 2) < 1
                                            ? 1
                                            : CurrentPage - MaxDisplayedPages / 2;
                EndDisplayedPage = (CurrentPage + MaxDisplayedPages / 2) > PageCount
                                        ? PageCount
                                        : CurrentPage + MaxDisplayedPages / 2;
            }
        }

        

        #endregion

        #region Constructors

        public ListPager(int itemsCount)
            : this(itemsCount, DefaultPageSize)
        {
        }

        public ListPager(int itemsCount, int pageSize)
            : this(itemsCount, pageSize, DefaultMaxDisplayedPages)
        {
        }

        public ListPager(int itemsCount, int pageSize, int maxDisplayedPages, int pageNumber=1)
        {
            _itemsCount = itemsCount;
            
            MaxDisplayedPages = maxDisplayedPages;
            CurrentPage = pageNumber;
            PageSize = pageSize;
        }

        #endregion

        public int PageCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int StartDisplayedPage { get; private set; }
        public int EndDisplayedPage { get; private set; }

    }
}