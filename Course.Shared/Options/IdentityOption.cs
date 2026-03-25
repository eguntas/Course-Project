using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Options
{
    public class IdentityOption
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Address { get; set; }
    }
}
