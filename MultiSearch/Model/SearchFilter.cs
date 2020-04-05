using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.Model
{
    class SearchFilter
    {
        public SearchFilter()
        {
            this.SearchFilters = new List<string>();
        }

        public List<string> SearchFilters { get; set; }
    }

}

