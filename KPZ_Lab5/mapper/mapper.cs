using AutoMapper;
using KPZ_lab5.Models;
using KPZ_lab5.ViewModels;

namespace KPZ_lab5
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AccountName)) // "Name" instead of "AccountName"
                .ReverseMap()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Name)); // Reverse mapping

            CreateMap<Counterparty, CounterpartyViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TaxId))
                .ReverseMap()
                .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.InvoiceCategoryId)) // CategoryId instead of InvoiceCategoryId
                .ReverseMap()
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<InvoiceStatus>(src.Status)))
                .ForMember(dest => dest.InvoiceCategoryId, opt => opt.MapFrom(src => src.CategoryId)); // Reverse mapping

            CreateMap<Payment, PaymentViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PaymentDate)) // "Date" instead of "PaymentDate"
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.PaymentAmount)) // "Amount" instead of "PaymentAmount"
                .ReverseMap()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.Date)) // Reverse mapping
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.Amount)); // Reverse mapping

            CreateMap<InvoiceCategory, InvoiceCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceCategoryId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.CategoryName)) // "Name" instead of "CategoryName"
                .ReverseMap()
                .ForMember(dest => dest.InvoiceCategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name)); // Reverse mapping
        }
    }
}
