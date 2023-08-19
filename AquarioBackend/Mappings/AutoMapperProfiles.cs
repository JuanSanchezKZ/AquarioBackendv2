using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AutoMapper;

namespace AquarioBackend.Mappings
{
    public class AutoMapperProfiles: Profile
    {

        public AutoMapperProfiles() {

            CreateMap<ReplyDTO, Reply>();
        }
    }
}
