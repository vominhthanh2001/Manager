using Manager.Api.Data;
using Manager.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Manager.Api.Services
{
    public class AntiHackerService : IAntiHacker
    {
        private readonly ServerDbContext _contenxt;

        public AntiHackerService(ServerDbContext contenxt)
        {
            _contenxt = contenxt;
        }

        public async Task<bool> AntiHacker(string jwt)
        {
            var auth = await _contenxt.Authentications.FirstOrDefaultAsync(x => x.Jwt == jwt);
            if (auth is null)
                return false;

            int id = auth.Id;
            var users = await _contenxt.IncludeGetAllUsers();
            var userFind = users.FirstOrDefault(x => x.Authentication.Id == id);
            if (userFind is null)
                return false;

            var device = userFind.Device;
            if (device.IsOnline && (device.TotalOnline + 1) > device.MaxDevice)
                return true;

            return false;
        }
    }
}
