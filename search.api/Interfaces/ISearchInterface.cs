using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces

{
    public interface ISearchInterface
    {
        public Task<SearchInfo> Search(DateTime start, DateTime end);
        public decimal CalculatePrice(VehicleOurDto veh, UserDto user, DateTime start, DateTime end);
    }
}