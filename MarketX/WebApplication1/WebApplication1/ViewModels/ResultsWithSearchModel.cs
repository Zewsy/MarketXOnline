using MarketX.BLL.DTOs;
using X.PagedList;

namespace MarketX.ViewModels
{
    public class ResultsWithSearchModel
    {
        public ResultsWithSearchModel(SearchModel searchModel, IPagedList<ResultAdvertisementCard> results)
        {
            SearchModel = searchModel;
            Results = results;
        }
        public SearchModel SearchModel { get; set; }
        public IPagedList<ResultAdvertisementCard> Results { get; set; }
    }
}
