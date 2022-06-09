using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

namespace Tempus.Common;
public interface IApi
{
  void Register(WebApplication app);
}
