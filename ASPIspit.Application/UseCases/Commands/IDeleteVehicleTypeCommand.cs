using ASPIspit.DataAccess.Models;
using ASPNedelja3.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Application.UseCases.Commands
{
    public interface IDeleteVehicleTypeCommand : ICommand<int>
    {
    }
}
