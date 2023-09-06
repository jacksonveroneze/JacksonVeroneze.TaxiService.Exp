using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Models;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappers;

[ExcludeFromCodeCoverage]
public class UserInfraMapper : Profile
{
    public UserInfraMapper()
    {
        // Entity -> Model
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday.ToDateTime(TimeOnly.Parse("00:00"))))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.Cpf, opts => opts.MapFrom(src => src.Cpf.Value));

        // Model -> Entity
        CreateMap<UserModel, UserEntity>()
            .ConstructUsing(src => new UserEntity(
                NameValueObject.Create(src.Name!).Value!,
                DateOnly.FromDateTime(src.Birthday),
                (Gender)src.Gender,
                CpfValueObject.Create(src.Cpf!).Value!
            ))
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Birthday, opts => opts.Ignore())
            .ForMember(dest => dest.Emails, opts => opts.Ignore())
            .ForMember(dest => dest.Phones, opts => opts.Ignore())
            .ForMember(dest => dest.Events, opts => opts.Ignore())
            .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.UpdatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
            .ForMember(dest => dest.Version, opts => opts.Ignore());
    }
}
