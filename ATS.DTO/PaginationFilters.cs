using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class PaginationFilters
    {
        public int TableDrawSequence { get; set; }
        public int TableDataStartingIndex { get; set; }
        public int TablePageLength { get; set; }
        public string TableColumnToSort { get; set; }
        public string TableColumnSortDirection { get; set; }
    }
}
