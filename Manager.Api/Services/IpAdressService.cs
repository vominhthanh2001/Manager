using Manager.Shared.Contracts;

namespace Manager.Api.Services
{
    public class IpAdressService : IIpAdress
    {
        public async Task<string> GetIpConnect(HttpRequest request)
        {
            // Check if the request contains the X-Forwarded-For header (used when behind a proxy)
            if (request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var ipAddresses = request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(ipAddresses))
                {
                    // The X-Forwarded-For header may contain a list of IP addresses
                    var addresses = ipAddresses.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }
            }

            // Fallback to the remote IP address if the X-Forwarded-For header is not present
            return request.HttpContext.Connection.RemoteIpAddress?.ToString();
        }

    }
}
