using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Service.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolRegistration.API.Controllers
{
    [RoutePrefix("api/relatorios")]
    public class ReportsController : ApiController
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("alunos-por-turma")]
        [ResponseType(typeof(SchoolClassReportResponse))]
        public async Task<IHttpActionResult> GetSchoolClassReport()
        {
            var response = await _service.GetSchoolClassReport();

            return Ok(response);
        }
    }
}