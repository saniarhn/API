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
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;

        }

        public int Register(RegisterVM registerVM)
        {
            var result = 0;
            try
            {
                var cekNik = myContext.Employees.Find(registerVM.NIK);
                var cekEmail = myContext.Employees.Where(e => e.Email.Contains(registerVM.Email)).FirstOrDefault();
                var cekPhone = myContext.Employees.Where(e => e.Phone.Contains(registerVM.Phone)).FirstOrDefault();
                var cekUniversityId = myContext.Universities.Find(registerVM.UniversityId);

                if (cekNik != null)
                {
                    return 2;
                }
                else if (cekEmail != null)
                {
                    return 3;
                }
                else if (cekPhone != null)
                {
                    return 4;
                }
                else if (cekUniversityId == null)
                {
                    return 5;
                }

                Gender gender = new Gender();
                if (registerVM.Gender == "Male" || registerVM.Gender == "male")
                {
                    gender = Gender.Male;
                }
                else if (registerVM.Gender == "Female" || registerVM.Gender == "female")
                {
                    gender = Gender.Female;
                }
                Employee employee = new Employee()
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Gender = gender,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.BirthDate,
                    Email = registerVM.Email,
                    Salary = registerVM.Salary
                };


                myContext.Employees.Add(employee);

                Account a = new Account()

                {
                    NIK = employee.NIK,
                    Password = Hashing.Hashing.HashPassword(registerVM.Password),

                };

                myContext.Accounts.Add(a);

                Education education = new Education()
                {

                    GPA = registerVM.GPA,
                    Degree = registerVM.Degree,
                    UniversityId = registerVM.UniversityId

                };
                myContext.Educations.Add(education);
                myContext.SaveChanges();

                Profiling p = new Profiling()
                {
                    NIK = employee.NIK,
                    EducationId = education.EducationId

                };

                myContext.Profilings.Add(p);

                AccountRole r = new AccountRole()
                {
                    RoleId = 1,
                    AccountNIK = employee.NIK
                };
                myContext.AccountRoles.Add(r);
                myContext.SaveChanges();
                result = 1;

            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }
        public IEnumerable GetAll()
        {
            var register= from e in myContext.Set<Employee>()
                            join a in myContext.Set<Account>() on e.NIK equals a.NIK
                            join p in myContext.Set<Profiling>() on a.NIK equals p.NIK
                            join ed in myContext.Set<Education>() on p.EducationId equals ed.EducationId
                            join u in myContext.Set<University>() on ed.UniversityId equals u.UniversityId
                            select new {
                               NIK= e.NIK,
                               Fullname = e.FirstName + e.LastName,
                               Gender = e.Gender == 0? "Male":"Female",
                               Phone = e.Phone,
                               Salary = e.Salary,
                               Email = e.Email,
                               GPA = ed.GPA,
                               Degree = ed.Degree,
                               UniversityName = u.Name

                            };
            return register.ToList();
        }
       
    }
}
