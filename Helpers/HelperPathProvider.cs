using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

namespace IronGrip.Helpers
{
    public class HelperPathProvider
    {
      
            private IServer server;
            private IWebHostEnvironment environment;

        //Como yo solo voy a usar una carpeta lo hago de forma estática, sin enum ni ifs ni leches.
        private static string CarpetaSubidas = Path.Combine("images", "uploads");

            public HelperPathProvider(IServer server, IWebHostEnvironment environment)
            {
                this.server = server;
                this.environment = environment;

            }

            public string MapPath(string fileName)
            {
                string rootPath = this.environment.WebRootPath;
                string path = Path.Combine(rootPath, CarpetaSubidas, fileName);
                return path;
            }

            public string MapUrlPath(string fileName)
            {
                var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
                string serverUrl = addresses.FirstOrDefault();
                string urlPath = serverUrl + "/" + CarpetaSubidas + "/" + fileName;
                return urlPath;
            }
        }
}
