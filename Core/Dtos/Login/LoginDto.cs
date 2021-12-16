using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Login
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<object> Claims { get; internal set; }
    }
}
