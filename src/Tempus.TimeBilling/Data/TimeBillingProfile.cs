using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Tempus.TimeBilling.Data;

public class TimeBillingProfile : Profile
{
  public TimeBillingProfile()
  {
    this.CreateMap<TimeBill, TimeBill>().ReverseMap();
  }
}
