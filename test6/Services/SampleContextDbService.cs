using test6.Exceptions;
using test6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test6.Services
{
    public class SampleContextDbService : IDbService
    {
        private readonly APBD_testContext _context;
        public SampleContextDbService(APBD_testContext context)
        {
            _context = context;
        }
        public void sampleMethod()
        {
        }


    }
}
