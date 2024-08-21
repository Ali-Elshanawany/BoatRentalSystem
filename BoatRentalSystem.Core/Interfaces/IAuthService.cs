using BoatRentalSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalSystem.Core.Interfaces
{
    public interface IAuthService
    {

        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> Login(TokenRequestModel model);

        Task<String> AddRoleAsync(AddRoleModel model);

    }

   
}
