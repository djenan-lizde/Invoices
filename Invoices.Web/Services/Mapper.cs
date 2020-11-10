using AutoMapper;
using Invoices.Models.Models;
using Invoices.Web.ViewModels;

namespace Invoices.Web.Services
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<InvoiceOutputViewModel,Invoice>();
        }
    }
}
