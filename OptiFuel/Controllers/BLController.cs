using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFuel.Data;
using OptiFuel.Models;

namespace OptiFuel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BLController(AppDbContext context)
        {
            _appDbContext = context;
        }


        




    }
}
