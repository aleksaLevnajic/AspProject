using ASPIspit.Application.DTO;
using ASPIspit.Application.UseCases.Commands;
using ASPIspit.DataAccess.Data;
using ASPIspit.DataAccess.Models;
using ASPIspit.Implementation.Validtators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Implementation.Commands
{
    public class EfAddRegistrationCommand : IAddRegistrationCommand
    {
        public int Id => 4;

        public string Name => "Add new resgistration";

        public string Description => "";

        private readonly AspIspitContext _context;
        private readonly AddRegistrationValidator _validator;

        public EfAddRegistrationCommand(AspIspitContext context, AddRegistrationValidator validator = null)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(RegistrationDto request)
        {
            _validator.ValidateAndThrow(request);

            var registration = new Registration
            {
                VehicleId = request.VehicleId,
                RegistrationPlateId = request.RegistrationPlateId,
                IssuedAt = DateTime.UtcNow,
                ValidUntil = DateTime.UtcNow.AddYears(1)
            };

            _context.Registrations.Add(registration);
            _context.SaveChanges();
        }
    }
}
