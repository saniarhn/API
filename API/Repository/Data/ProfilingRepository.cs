using System;
using API.Context;
using API.Models;
using API.Repository.GeneralRepository;

namespace API.Repository.Data
{

    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
