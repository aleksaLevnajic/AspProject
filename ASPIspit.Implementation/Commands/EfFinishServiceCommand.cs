using ASPIspit.Application.DTO;
using ASPIspit.Application.UseCases.Commands;
using ASPIspit.DataAccess.Data;
using ASPIspit.Implementation.Validtators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Implementation.Commands
{
    public class EfFinishServiceCommand : IFinishServiceCommand
    {
        public int Id => 2;

        public string Name => "Ef Finish Service Command";

        public string Description => "";

        private AspIspitContext _context;
        private FinishServiceValidator _validator;

        public EfFinishServiceCommand(AspIspitContext context, FinishServiceValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(FInishServiceDto request)
        {
            _validator.ValidateAndThrow(request);

            var service = _context.ServiceSchedules.Find(request.ServiceId.Value);

            service.Price = request.Price.Value;
            service.FinishedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
