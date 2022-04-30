using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using DocGenerator.Certificate.Models;
using System.Threading.Tasks;

namespace DocGenerator.Certificate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificateController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CertificateClass> Get()
        {
            return default(IEnumerable<CertificateClass>);
        }
    }
}
