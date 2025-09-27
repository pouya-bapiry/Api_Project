using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public ValuesController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}
