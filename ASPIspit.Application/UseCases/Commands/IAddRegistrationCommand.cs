using ASPIspit.Application.DTO;
using ASPNedelja3.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ASPIspit.Application.UseCases.Commands
{
    public interface IAddRegistrationCommand : ICommand<RegistrationDto>
    {

    }
}
