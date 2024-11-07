using search.api.Interfaces;

namespace search.api.Services
{
    public class SearchMainService : ISearchInterface
    {
        // Here We must ask api and after that create a SearchInfo object
        public Task<SearchInfo> Search()
        {
            throw new NotImplementedException();
        }
    }
}