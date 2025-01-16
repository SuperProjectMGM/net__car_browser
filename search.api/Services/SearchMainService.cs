using search.api.DTOs;
using search.api.Interfaces;
using search.api.Helpers;
using search.api.Mappers;

namespace search.api.Services
{
    public class SearchMainService : ISearchInterface
    {
        // Here We must ask api and after that create a SearchInfo object
        private readonly IUserInfoInterface _userRepo;
        private readonly IEnumerable<IVehicleGetter> _vehicleGetters;
        // Now it's hardcoded path. I think that we should develop something more interesting than hardcoded paths.

        public SearchMainService(IEnumerable<IVehicleGetter> vehicleGetters, IUserInfoInterface userRepo)
        {
            _vehicleGetters = vehicleGetters;
            _userRepo = userRepo;
        }

        public async Task<SearchInfo> Search(DateTime start, DateTime end)
        {
            // Here we go to endpoint and have cars
            var resList = new List<Vehicle>();
            foreach (var getter in _vehicleGetters)
            {
                var tmpList = await getter.GetAvailableVehiclesFromRemoteOrigin(start, end);
                resList.AddRange(tmpList.Select(dto => dto.VehicleOurDtoToVehicle()).ToList());
            }            
            SearchInfo info = new SearchInfo();
            
            info.Vehicles = resList;
            return info;
        }



        public async Task<decimal?> CalculatePrice(VehicleOurDto vehicle, string Token, DateTime start, DateTime end)
        {
            var userID = _userRepo.ReturnIdFromToken(Token);
            var user = await _userRepo.FindUserById(userID!.Value);
            decimal? res = null;
            foreach (var getter in _vehicleGetters)
            {
                res = await getter.GetPriceForCar(vehicle, user!, start, end);
                if (res != null)
                {
                    break;
                }
            }       
            return res;
        }
    }
}