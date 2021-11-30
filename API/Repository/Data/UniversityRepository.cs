using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.Repository.GeneralRepository;
using API.ViewModel;

namespace API.Repository.Data
{

    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public KeyValuePair<List<string>, List<int>> GetCountUniversity()
        {
            var getData = (from p in myContext.Profilings
                           join ed in myContext.Educations on p.EducationId equals ed.EducationId
                           join u in myContext.Universities on ed.UniversityId equals u.UniversityId
                           group u by u.Name into count
                           select new
                           {   UniversityName = count.Key,
                               NumberOfEmployees = count.Count()
                           }).ToList();
            List<string> UniversityName = new List<string>();
            List<int> NumberOfEmployees = new List<int>();
            foreach(var dt in getData)
            {
                UniversityName.Add(dt.UniversityName);
                NumberOfEmployees.Add(dt.NumberOfEmployees);
            }
            return new KeyValuePair<List<string>, List<int>>(UniversityName, NumberOfEmployees);

        }
       
    }
}
