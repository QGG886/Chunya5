using Microsoft.EntityFrameworkCore;

namespace Chunya5.Helper
{
    public class PageList<T> : List<T>
    {
        //当前页数
        public int CurrentPage { get; set; }

        //总页数
        public int TotalPages { get; set; }

        //每一页需要多少条数据
        public int PageSize { get; set; }

        //总共有多少条数据
        public int TotalCount { get; set; }

        //是否有上一页
        public bool HasPrevious => CurrentPage > 1;

        //是否有下一页
        public bool HasNext => CurrentPage < TotalPages;

        public PageList(List<T> items, int pageNumber, int pageSize, int count)
        {
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            AddRange(items);
        }

        //异步创建
        public static async Task<PageList<T>> CreatPageListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToListAsync();

            return new PageList<T>(items, pageNumber, pageSize, count);

        }


    }
}
