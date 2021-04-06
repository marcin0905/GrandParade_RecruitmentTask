using System;
using System.Collections.Generic;
using CustomerService.Application.Extensions;

namespace CustomerService.Application.Dto.Paging
{
    public class PageableResponseDto<TResult> where TResult : class
    {
        public int ItemsCount { get; }


        public int ItemsPerPage { get; }


        public int PageCount { get; }


        public IEnumerable<TResult> Items { get; }


        private PageableResponseDto(int itemsCount, IEnumerable<TResult> items)
        {

            ItemsPerPage = PaginationConfiguration.ItemsPerPage;

            ItemsCount = itemsCount;

            Items = items;


            PageCount = (int)Math.Round(

                decimal.ToDouble(itemsCount) / decimal.ToDouble(ItemsPerPage));

        }


        public static PageableResponseDto<TResult> Empty => new PageableResponseDto<TResult>(0, new TResult[0]);


        public static PageableResponseDto<TResult> Create(int itemsCount, IEnumerable<TResult> items) =>

            new PageableResponseDto<TResult>(itemsCount, items);
    }
}