using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Service.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolRegistration.API.Controllers
{
    [RoutePrefix("api/turmas")]
    public class SchoolClassesController : ApiController
    {
        private readonly ISchoolClassService _service;

        public SchoolClassesController(ISchoolClassService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(SchoolClassLongResponse))]
        public async Task<IHttpActionResult> GetAllClasses()
        {
            var response = await _service.GetSchoolClasses();

            return Ok(response);
        }
    }
}
