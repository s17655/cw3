using test3.Exceptions;
using test3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test3.Services
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
