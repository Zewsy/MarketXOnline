using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MarketX.ViewModels
{
    public class ResultsWithSearchFormModel
    {
        public ResultsWithSearchFormModel(SearchFormModel searchFormModel, IPagedList<ResultAdvertisementCard> results)
        {
            SearchFormModel = searchFormModel;
            Results = results;
        }
        public SearchFormModel SearchFormModel { get; set; }
        public IPagedList<ResultAdvertisementCard> Results { get; set; }
    }
}
