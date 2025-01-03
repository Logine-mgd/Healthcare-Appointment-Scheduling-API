using MedConnect_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MedConnect_API.UOW
{
    public class unitofwork
    {
        HealthCareContext db;

        public unitofwork(HealthCareContext db)
        {
            this.db = db;
        }

        repo<Appointment> appointments;
        repo<Medication> medications;
        repo<Record_medications> record_medications;
        repo<MedicalRecord> medrecs;
        repo<Patient> patients;
        repo<Provider> providers;

        public List<Record_medications> Selectbymid(int id)
        {
            return db.Record_Medications.Where(m => m.MedicalRecord_id == id).ToList();
        }

        public List<MedicalRecord> Selectbypatientid(string id)
        {
            return db.MedicalRecords.Where(m => m.Patient_id == id).ToList();
        }
        public repo<Appointment> Appointments
        {
            get
            {
                if (appointments == null)
                    appointments = new repo<Appointment>(db);

                return appointments;
            }
        }

        
        public repo<Medication> Medication
        {
            get
            {
                if (medications == null)
                    medications = new repo<Medication>(db);

                return medications;
            }
        }
        public repo<Record_medications> Record_medications
        {
            get
            {
                if (record_medications == null)
                    record_medications = new repo<Record_medications>(db);

                return record_medications;
            }
        }

        public repo<MedicalRecord> Medrec
        {
            get
            {
                if (medrecs == null)
                    medrecs = new repo<MedicalRecord>(db);

                return medrecs;
            }
        }

        public repo<Patient> Patients
        {
            get
            {
                if (patients == null)
                    patients = new repo<Patient>(db);

                return patients;
            }
        }

        public repo<Provider> Providers
        {
            get
            {
                if (providers == null)
                    providers = new repo<Provider>(db);
                return providers;
            }
        }

        public void SaveAll()
        {
            db.SaveChanges();
        }
    }
}
