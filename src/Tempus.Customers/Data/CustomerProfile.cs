using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Tempus.Customers.Data;

public class CustomerProfile : Profile
{
  public CustomerProfile()
  {
    this.CreateMap<Customer, Customer>()
      .ForMember(d => d.Contacts,
                 opt => opt.Ignore())
      .ReverseMap();
    this.CreateMap<Location, Location>().ReverseMap();
    this.CreateMap<Contact, Contact>().ReverseMap();
  }
}
