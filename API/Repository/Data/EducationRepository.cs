using System;
using API.Context;
using API.Models;
using API.Repository.GeneralRepository;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {

        public EducationRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
