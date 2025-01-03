using AutoMapper;
using MedConnect_API.DTOs;
using MedConnect_API.Models;
using MedConnect_API.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace MedConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        unitofwork uow;
        IMapper mapper;

        public MedicalRecordController(unitofwork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost("/patients/{id}/medical-records")]
        [SwaggerOperation(Summary = "Add medical record")]
        [SwaggerResponse(201, "Medical record is added")]
        [SwaggerResponse(404, "Invalid Data")]
        [SwaggerResponse(401,"Unauthorized,must be a provider")]
        [Authorize(Roles = "provider")]
        public IActionResult Add(string id, MedicalRecordDTO mdto)
        {
            if (ModelState.IsValid)
            {
                var medicalrec = new MedicalRecord()
                {
                    Doctor_Name = mdto.Doctor_Name,
                    Patient_id = mdto.Patient_id,
                    Visit_date = mdto.Visit_date,
                    Reason = mdto.Reason,
                    Treatmentnotes = mdto.Treatmentnotes,
                };

                var mr = new List<Medication_recordDTO>();
                foreach (var m in mdto.medication_Record)
                {
                    var medicationrec = new Record_medications()
                    {
                        Medication_id = m.Medication_id,
                        MedicalRecord_id = medicalrec.Id,
                        Dosage = m.Dosage,
                    };

                    uow.Record_medications.Add(medicationrec);
                    medicalrec.Record_medications.Add(medicationrec);
                }

                uow.Medrec.Add(medicalrec);
                uow.SaveAll();
                return Created();
            }else
                return BadRequest(ModelState);
        }

        [HttpGet("/patients/{patient_id}/medical-records/{record_id}")]
        [SwaggerOperation(Summary = "Get medical record of entered patient id and record id")]
        [SwaggerResponse(200, "Records is returned")]
        [SwaggerResponse(400,"Invalid Data ")]
        [SwaggerResponse(404, "No medical record is found")]
        public IActionResult Get(string patient_id, int record_id)
        {
            var medrec = uow.Medrec.SelectById<int>(record_id);
            if (medrec != null)
            {
                if (medrec.Patient_id == patient_id)
                {
                    var res = mapper.Map<EditMedRecordDTO>(medrec);
                    res.medication_Record = mapper.Map<List<EditMedcationrecDTO>>(uow.Selectbymid(record_id));
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Patient Id is incorrect");
                }
            }
            else
                return NotFound();
        }

        [HttpPut("/patients/{patient_id}/medical-records/{record_id}")]
        [SwaggerOperation(Summary = "Edit medical record of entered patient id and record id")]
        [SwaggerResponse(200, "Records is updated")]
        [SwaggerResponse(400, "Invalid Data ")]
        [SwaggerResponse(404, "No medical record is found")]
        [SwaggerResponse(401, "Unauthorized,must be a provider")]
        [Authorize(Roles = "provider")]
        public IActionResult Put(string patient_id, int record_id, EditMedRecordDTO mdto)
        {
            var medrec = uow.Medrec.SelectById<int>(record_id);
            if (medrec != null)
            {
                if (medrec.Patient_id == patient_id)
                {
                    foreach (var r in mdto.medication_Record)
                    {
                        var recmedications = uow.Record_medications.SelectById<int>(r.Id);
                        recmedications.Medication_id = r.Medication_id;
                        recmedications.Dosage = r.Dosage;
                        uow.Record_medications.Update(recmedications);
                    }
                    medrec.Doctor_Name = mdto.Doctor_Name;
                    medrec.Treatmentnotes = mdto.Treatmentnotes;
                    medrec.Visit_date = mdto.Visit_date;
                    medrec.Patient_id = patient_id;
                    medrec.Reason = mdto.Reason;
  
                    uow.Medrec.Update(medrec);
                    uow.SaveAll();
                    return Ok();
                }
                else return BadRequest("Patient Id is incorrect");
            }
            else return NotFound();
        }
    }
}
