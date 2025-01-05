using search.api.Models;

namespace search.api.Interfaces

{
    public interface ISearchInterface
    {
        // Method for finding cars that are available
        public Task<SearchInfo> Search(DateTime start, DateTime end);
    }
}