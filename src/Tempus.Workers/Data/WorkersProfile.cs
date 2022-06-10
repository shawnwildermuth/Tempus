using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Tempus.Workers.Data;

public class WorkersProfile : Profile
{
  public WorkersProfile()
  {
    this.CreateMap<Worker, Worker>().ReverseMap();
  }
}
