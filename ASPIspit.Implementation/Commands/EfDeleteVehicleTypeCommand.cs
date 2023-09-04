using ASPIspit.Application.Exceptions;
using ASPIspit.Application.UseCases;
using ASPIspit.Application.UseCases.Commands;
using ASPIspit.DataAccess.Data;
using ASPIspit.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Implementation.Commands
{
    public class EfDeleteVehicleTypeCommand : IDeleteVehicleTypeCommand
    {
        public int Id => 1;
        public string Name => "Ef Delete Vehicle Type";
        public string Description => "";


        private AspIspitContext _context;

        public EfDeleteVehicleTypeCommand(AspIspitContext context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var vehicleType = _context.VehicleTypes.Include(x => x.ManufacturersVehicleTypes)
                                                   .Include(x => x.Models).FirstOrDefault(x => x.Id == request);

            if(vehicleType == null)
            {
                throw new EntityNotFoundException(nameof(VehicleType), request);
            }

            if(vehicleType.ManufacturersVehicleTypes.Any())
            {
                throw new UseCaseConflictException("Unable to delete vehicle type because it is linked to manufactures.");
            }

            if (vehicleType.Models.Any())
            {
                throw new UseCaseConflictException("Unable to delete vehicle type because it is linked to models.");
            }

            _context.VehicleTypes.Remove(vehicleType);
            _context.SaveChanges();
        }
    }
}
