using AutoMapper;
using MedConnect_API.DTOs;
using MedConnect_API.Models;
using System;

namespace MedConnect_API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProviderDTO, Provider>().ReverseMap();
            CreateMap<Patient_RegDTO, Patient>().AfterMap(
            (pdto, p) =>
            {
                 p.DoB = DateTime.ParseExact(pdto.DoB, "yyyy-MM-dd", null);
            });
            CreateMap<AppointmentDTO, Appointment>().AfterMap(
                (adto,a)=>
                {
                    DateTime parsedDate = DateTime.ParseExact(adto.Date, "yyyy-MM-dd", null);
                    TimeSpan timeSpan = TimeSpan.Parse(adto.Time);
                    a.Date = parsedDate.Add(timeSpan);
                }
                ).ReverseMap();
            CreateMap<GetAppointmentDTO, Appointment>().ReverseMap();
            CreateMap<Medication_recordDTO,Record_medications>().ReverseMap();
            CreateMap<EditMedRecordDTO, MedicalRecord>().ReverseMap();
            CreateMap<EditMedcationrecDTO, Record_medications>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDTO>().ReverseMap();
        }
    }
}
