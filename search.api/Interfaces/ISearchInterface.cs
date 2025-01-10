using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces

{
    public interface ISearchInterface
    {
        public Task<SearchInfo> Search(DateTime start, DateTime end);
        public Task<decimal> CalculatePrice(decimal price, string Token, DateTime start, DateTime end);
    }
}