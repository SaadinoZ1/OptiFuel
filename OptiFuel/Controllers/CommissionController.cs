using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptiFuel.Data;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

    }
}
