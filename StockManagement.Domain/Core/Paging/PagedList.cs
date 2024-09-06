using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Domain.Core.Paging
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }

        public PagedList(List<T> items,int totalItems,int pageNumber,int pageSize) 
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages =(int)Math.Ceiling(totalItems/(double)pageSize);
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static PagedList<T> Create(List<T> source,int pageNumber,int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items,count, pageNumber, pageSize);
        }
    }
}
