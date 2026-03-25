using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        public string UserName => "Ercan16";

        public List<string> Roles => throw new NotImplementedException();
    }
}
