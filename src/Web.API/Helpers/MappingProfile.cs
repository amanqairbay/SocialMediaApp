using System;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Message;
using Core.DTOs.Photo;
using Core.DTOs.User;
using Core.Entities;
using Web.API.Extensions;

namespace Web.API.Helpers
{
    /// <summary>
    /// Mapping profile
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // user dto
            CreateMap<AppUser, UserDto>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.City!.Name))
                .ForMember(d => d.Region, o => o.MapFrom(s => s.Region!.Name))
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender!.Name))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status!.Name))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(p => p.IsMain == true)!.Url));

            // user for detailed dto
            CreateMap<AppUser, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(scr => scr.Photos.FirstOrDefault(p => p.IsMain)!.Url);
                })
                .ForMember(d => d.City, o => o.MapFrom(s => s.City!.Name))
                .ForMember(d => d.Region, o => o.MapFrom(s => s.Region!.Name))
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender!.Name))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status!.Name))
                .ForMember(d => d.LastActive, o => o.MapFrom(s => s.LastActive))
                .ForMember(d => d.Age, o => { o.MapFrom(d => d.DateOfBirth.CalculateAge()); });

            // user for list dto
            CreateMap<AppUser, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(scr => scr.Photos.FirstOrDefault(p => p.IsMain)!.Url);
                })
                .ForMember(d => d.City, o => o.MapFrom(s => s.City!.Name))
                .ForMember(d => d.Region, o => o.MapFrom(s => s.Region!.Name))
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender!.Name))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status!.Name))
                .ForMember(d => d.LastActive, o => o.MapFrom(s => s.LastActive))
                .ForMember(d => d.Age, o => { o.MapFrom(d => d.DateOfBirth.CalculateAge()); });

            //CreateMap<UserForRegisterDto, AppUser>();
            CreateMap<UserForUpdateDto, AppUser>();

            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();

            CreateMap<City, CityDto>();
            CreateMap<Region, RegionDto>();
            CreateMap<Gender, GenderDto>();
            CreateMap<Status, StatusDto>();

            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(d => d.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain)!.Url))
                .ForMember(d => d.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain)!.Url));
        }
    }
}

