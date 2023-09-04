using ASPIspit.Application.DTO;
using ASPIspit.Application.UseCases.Queries;
using ASPIspit.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Implementation.Queries
{
    public class EfGetServiceScheduleQuery : IGetServicescheduleQuery
    {
        public int Id => 3;

        public string Name => "Search service ef";

        public string Description => "";

        private AspIspitContext _context;

        public EfGetServiceScheduleQuery(AspIspitContext context)
        {
            _context = context;
        }
        public PagedResponse<ServiceScheduleDto> Execute(ServiceScheduleSearch search)
        {
            var query = _context.ServiceSchedules
                        .Include(x => x.ServiceType)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Registrations).ThenInclude(x => x.RegistrationPlate)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Manufacturer)
                        .Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.VehicleType)
                        .AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword))
            {
                var kw = search.Keyword.ToLower();
                query = query.Where(q => q.Vehicle.Label.Contains(kw)
                                      || q.Vehicle.Model.Name.Contains(kw) 
                                      || q.Vehicle.Model.Manufacturer.Name.Contains(kw)
                                      || q.Vehicle.Model.VehicleType.Name.Contains(kw)
                                      || q.Vehicle.Registrations.Any(r => r.RegistrationPlate.RegistrationPlate1.Contains(kw) && r.ValidUntil > DateTime.UtcNow)
                                      || q.ServiceType.Name.Contains(kw));
            }

            if(search.IsFinished.HasValue)
            {
                var finished = search.IsFinished.Value;
                query = query.Where(x => finished ? x.FinishedAt != null : x.FinishedAt == null);
            }

            if(search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= search.MinPrice.Value);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= search.MaxPrice.Value);
            }

            if (search.PerPage == 0 || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == 0 || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page - 1) * search.PerPage;

            var response = new PagedResponse<ServiceScheduleDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage).Select(x => new ServiceScheduleDto
            {
                Id = x.Id,
                AdditionalInfo = x.AdditionalInfo == null ? "/" : x.AdditionalInfo,
                FinishedAt = x.FinishedAt,
                Manufacturer = x.Vehicle.Model.Manufacturer.Name,
                Model = x.Vehicle.Model.Name,
                Price = x.Price, 
                RegistrationPage = x.Vehicle.Registrations.OrderByDescending(y => y.ValidUntil).Any() ?
                                   x.Vehicle.Registrations.OrderByDescending(y => y.ValidUntil).FirstOrDefault().RegistrationPlate.RegistrationPlate1 :
                                   "/",
                ServiceTypeName = x.ServiceType.Name,
                ScheduleFor = x.ScheduledFor
            }).ToList();
            response.CurrentPage = search.Page;
            response.ItemsPerPage = search.PerPage;
            //return query.Select(

            return response;
        }
    }
}
