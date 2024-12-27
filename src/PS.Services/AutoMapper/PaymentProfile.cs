using AutoMapper;
using PS.Domain.DTOs;
using PS.Domain.DTOs.Requests;
using PS.Domain.Entities;

namespace PS.Services.AutoMapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentIntentEntity, PaymentIntentRequestDTO>();
            CreateMap<PaymentIntentDto , PaymentIntentEntity>()
                .ForMember(paymentIntentEntity => paymentIntentEntity.Uuid, opt => opt.MapFrom(paymentIntentDto => paymentIntentDto.Uuid))
                .ForMember(paymentIntentEntity => paymentIntentEntity.CustomerName, opt => opt.MapFrom(paymentIntentDto => paymentIntentDto.CustomerName))
                .ForMember(paymentIntentEntity => paymentIntentEntity.Description, opt => opt.MapFrom(paymentIntentDto => paymentIntentDto.Description))
                .ForMember(paymentIntentEntity => paymentIntentEntity.Status, opt => opt.MapFrom(paymentIntentDto => paymentIntentDto.Status))
            ;
        }
    }
}