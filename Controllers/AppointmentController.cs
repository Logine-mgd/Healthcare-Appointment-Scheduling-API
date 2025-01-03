using AutoMapper;
using MedConnect_API.DTOs;
using MedConnect_API.Models;
using MedConnect_API.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        unitofwork uow;
        IMapper mapper;
        public AppointmentController(unitofwork _uow,IMapper _map)
        {
            uow= _uow;
            mapper= _map;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new appointment")]
        [SwaggerResponse(201, "Appointment is added")]
        [SwaggerResponse(400, "Invalid Data")]
        public IActionResult Post(AppointmentDTO adto)
        {
            if (ModelState.IsValid)
            {
                Appointment a = mapper.Map<Appointment>(adto);
                uow.Appointments.Add(a);
                uow.Appointments.Save();
                return Created(string.Empty,adto);
            }
            else return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get appointment by id")]
        [SwaggerResponse(200, "Appointment is found")]
        [SwaggerResponse(400, "Appointment not found")]
        public IActionResult Get(int id)
        {
            GetAppointmentDTO a = mapper.Map<GetAppointmentDTO>(uow.Appointments.SelectById<int>(id));
            if (a != null) return Ok(a);
            else return NotFound();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Reschedule Appointment")]
        [SwaggerResponse(200, "Appointment is updated")]
        [SwaggerResponse(400, "Invalid Data")]
        public IActionResult Reschedule(AppointmentDTO adto, int id)
        {
            if (ModelState.IsValid)
            {
                Appointment a = uow.Appointments.SelectById<int>(id);
                a.Reason = adto.Reason;
                a.Provider_id = adto.Provider_id;
                a.Patient_id = adto.Patient_id;
                a.Date = DateTime.ParseExact(adto.Date, "yyyy-MM-dd", null).Add(TimeSpan.Parse(adto.Time));
                uow.Appointments.Update(a);
                uow.Appointments.Save();
                return Ok(a);
            }
            else return BadRequest(ModelState);

        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete Appointment")]
        [SwaggerResponse(200, "Appointment is updated")]
        [SwaggerResponse(400, "Invalid Data")]
        IActionResult Cancel(int id)
        {
            Appointment a = uow.Appointments.SelectById<int>(id);
            if (a != null)
            {
                uow.Appointments.Delete<int>(id); 
                uow.Appointments.Save();
                return Ok(a);
            }
            else return NotFound();
            
        }
    }
}
