using System.Collections.Generic;

namespace ATS.DTO.Reporting
{
    public interface IPagedResult<T>
    {
        IList<T> Items { get; set; }
        int TotalCount { get; set; }
    }

    public class PagedResult<T> : IPagedResult<T>
    {
        public IList<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
