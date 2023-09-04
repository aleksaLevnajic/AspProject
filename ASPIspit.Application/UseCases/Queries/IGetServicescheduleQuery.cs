using ASPIspit.Application.DTO;
using ASPNedelja3.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Application.UseCases.Queries
{
    public interface IGetServicescheduleQuery : IQuery<ServiceScheduleSearch, PagedResponse<ServiceScheduleDto>>
    {
    }
}
