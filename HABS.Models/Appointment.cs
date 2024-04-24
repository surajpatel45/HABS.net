using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HABS.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("PatientID")] public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        [ForeignKey("DoctorID")] public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
