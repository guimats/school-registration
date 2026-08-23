using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Service.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolRegistration.API.Controllers
{
    [RoutePrefix("api/alunos")]
    public class StudentsController : ApiController
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // Query String
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(StudentFilterResponse))]
        public async Task<IHttpActionResult> FilterStudents([FromUri] StudentFilterRequest filter)
        {
            filter = filter ?? new StudentFilterRequest();

            var response = await _service.FilterStudents(filter);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(StudentResponse))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var response = await _service.GetStudent(id);

            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(StudentResponse))]
        public async Task<IHttpActionResult> AddStudent([FromBody] StudentRequest request)
        {
            var response = await _service.AddStudent(request);

            return Created(new Uri(Request.RequestUri + "/" + response.Id), response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateStudent(int id, [FromBody] StudentRequest request)
        {
            await _service.UpdateStudent(request, id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteStudent([FromUri] int id)
        {
            await _service.DeleteStudent(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}