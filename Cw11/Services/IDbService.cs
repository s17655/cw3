using Cw11.DTO;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public interface IDbService
    {
        public void seed();
        public IEnumerable<Doctor> GetDoctors();
        public Doctor addDoctor(DoctorRequest request);

        public Doctor modifyDoctor(DoctorRequest request);
        public bool DeleteDoctor(DeleteDoctorRequest request);
        
    }
}
