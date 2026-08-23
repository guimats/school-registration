using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Service.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolRegistration.API.Controllers
{
    [RoutePrefix("api/matriculas")]
    public class RegistrationsController : ApiController
    {
        private readonly IRegistrationService _service;

        public RegistrationsController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(RegistrationResponse))]
        public async Task<IHttpActionResult> AddRegistration([FromBody] RegistrationRequest request)
        {
            var response = await _service.AddRegistration(request);

            return Created(new Uri(Request.RequestUri + "/" + response.Id), response);
        }
    }
}