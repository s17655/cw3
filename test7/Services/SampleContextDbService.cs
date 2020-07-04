using test7.Exceptions;
using test7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test7.Services
{
    public class SampleContextDbService : IDbService
    {
        private readonly SampleDbContext _context;
        public SampleContextDbService(SampleDbContext context)
        {
            _context = context;
        }
        public void sampleMethod()
        {
        }


    }
}
