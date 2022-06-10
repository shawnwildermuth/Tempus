using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Tempus.WorkTypes.Data;

public class WorkTypeProfile : Profile
{
  public WorkTypeProfile()
  {
    this.CreateMap<WorkType, WorkType>().ReverseMap();
  }
}
