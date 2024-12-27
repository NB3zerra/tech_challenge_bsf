using AutoMapper;
using PS.Domain.DTOs;
using PS.Domain.DTOs.Requests;
using PS.Domain.Entities;

namespace PS.Services.AutoMapper
{
    public class PaymentProfile : Profile {
        public PaymentProfile(){
            CreateMap<PaymentIntentEntity, PaymentIntentRequestDTO>();
            CreateMap<PaymentIntentEntity,PaymentIntentDto>();
        }
    }
}