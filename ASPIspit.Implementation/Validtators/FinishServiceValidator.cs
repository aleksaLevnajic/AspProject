using ASPIspit.Application.DTO;
using ASPIspit.DataAccess.Data;
using ASPIspit.Implementation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Implementation.Validtators
{
    public class FinishServiceValidator : AbstractValidator<FInishServiceDto>
    {
        public FinishServiceValidator(AspIspitContext context)
        {
            RuleFor(x => x.ServiceId).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("Service is requierd parameter.")
                                     .Must(x => context.ServiceSchedules.Any(s => s.Id == x)).WithMessage("Requierd service dosent exist.")
                                     .Must(x => context.ServiceSchedules.Any(s => s.Id == x && s.FinishedAt == null)).WithMessage("Requierd service is already finished.");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Price is requierd.")
                                    .GreaterThan(0).WithMessage("Price has to have positive value.");
        }
    }

    public class AddRegistrationValidator : AbstractValidator<RegistrationDto>
    {
        public AddRegistrationValidator(AspIspitContext context)
        {
            RuleFor(x => x.VehicleId).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("Vehicle is requierd parameter.")
                                     .Must(x => context.Vehicles.Any(v => v.Id == x)).WithMessage("Requierd vehicle dosent exist.")
                                     .Must(x => !context.Vehicles.Any(v => v.Id == x && v.Registrations.Any(r => r.ValidUntil > DateTime.UtcNow))).WithMessage("You cant register vehicle because it is already registred.");

            RuleFor(x => x.RegistrationPlateId).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Registration plate is requierd.")
                                    .Must(x => context.RegistrationPlates.Any(rp => rp.Id == x)).WithMessage("Registration plate dosnet exists.")
                                    .Must(x => !context.Registrations.Any(r => r.RegistrationPlateId == x && r.ValidUntil > DateTime.UtcNow))
                                    .WithMessage("Registration plate is already in use.");
        }
    }
}
