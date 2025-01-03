using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MedConnect_API.Models
{
    public class HealthCareContext: IdentityDbContext
    {
        public HealthCareContext()
        {

        }
        public HealthCareContext(DbContextOptions<HealthCareContext> op) : base(op)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "provider", NormalizedName = "PROVIDER" },
                new IdentityRole() { Id = "2", Name = "patient", NormalizedName = "PATIENT" }
                );

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Patient)
                      .WithMany()
                      .HasForeignKey(a => a.Patient_id)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(a => a.Provider)
                      .WithMany()
                      .HasForeignKey(a => a.Provider_id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<Record_medications> Record_Medications { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }

    }
}
