using AutoMapper;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;

namespace CarPoolingApplication.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<BookedRides, BookedRidesDTO>();
            CreateMap<OfferedRides, OfferedRidesDTO>();
            CreateMap<BookedRidesDTO, BookedRides>();
            CreateMap<OfferedRidesDTO, OfferedRides>();
        }
    }
}
