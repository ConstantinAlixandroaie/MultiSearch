using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.Model
{
    class SearchResult
    {
        public string FilePath { get; set; }

        public string Keyword { get; set; }

        public int? PageNumber { get; set; }

        public int? RowNumber { get; set; }

    }

}

