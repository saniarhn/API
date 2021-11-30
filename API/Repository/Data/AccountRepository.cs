using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using API.Context;
using API.Models;
using API.Repository.GeneralRepository;
using API.ViewModel;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {

        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;

        }
        public int Login(LoginVM loginVM)
        {
            var result = 0;
            try
            {
                var getEmail = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                var getPhone = myContext.Employees.Where(e => e.Phone == loginVM.Phone).FirstOrDefault();

                var pass = (from e in myContext.Set<Employee>()
                            join a in myContext.Set<Account>() on e.NIK equals a.NIK
                            where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                            select a.Password).Single();

                if (getEmail != null || getPhone != null)
                {
                    var getPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, pass);
                    if (getPassword)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 3;
                }

            }
            catch (Exception)
            {
                result = 0;
            }

            return result;

        }


        public IEnumerable GetProfile(LoginVM loginVM)
        {
            var register = from e in myContext.Set<Employee>()
                           where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                           join a in myContext.Set<Account>() on e.NIK equals a.NIK
                           join p in myContext.Set<Profiling>() on a.NIK equals p.NIK
                           join ed in myContext.Set<Education>() on p.EducationId equals ed.EducationId
                           join u in myContext.Set<University>() on ed.UniversityId equals u.UniversityId
                           select new
                           {
                               NIK = e.NIK,
                               Fullname = e.FirstName + e.LastName,
                               Gender = e.Gender == 0 ? "Male" : "Female",
                               Phone = e.Phone,
                               Salary = e.Salary,
                               Email = e.Email,
                               GPA = ed.GPA,
                               Degree = ed.Degree,
                               UniversityName = u.Name

                           };
            return register.ToList();
        }


        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var checkEmail = myContext.Employees.Where(e => e.Email == changePasswordVM.Email).FirstOrDefault();

            if (checkEmail != null)
            {
                var getNIK = checkEmail.NIK;

                var getData = myContext.Accounts.Find(getNIK);
                if (getData != null)
                {
                    if (Hashing.Hashing.ValidatePassword(changePasswordVM.CurrentPassword, getData.Password))
                    {
                        if (changePasswordVM.NewPassword == changePasswordVM.ConfirmPassword)
                        {
                            getData.Password = Hashing.Hashing.HashPassword(changePasswordVM.NewPassword);
                            myContext.SaveChanges();
                            return 1;
                        }
                        return 2;
                    }
                    return 3;
                }
                return 4;
            }
            return 4;
        }

        public int ForgotPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var checkEmail = myContext.Employees.Where(e => e.Email == forgetPasswordVM.Email).FirstOrDefault();

            if (checkEmail != null)
            {
                var getName= checkEmail.FirstName+" "+checkEmail.LastName;
                var getNIK = checkEmail.NIK;
                var getData = myContext.Accounts.Find(getNIK);
                if (getData != null)
                {
                    string ChangePassword = Guid.NewGuid().ToString(); 

                    getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                    myContext.SaveChanges();

                    DateTime today = DateTime.Now;

                    /*  untuk mengirim email*/
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("anothername.ok@gmail.com");
                    msg.To.Add(new MailAddress(forgetPasswordVM.Email));
                    msg.Subject = "Reset Password "+ today;
                    msg.Body = $"<p>Hai,{getName}</p>"+$"</br><p>Password Baru Anda Adalah {ChangePassword} <p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("anothername.ok@gmail.com", "polmed2018");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;

                }
                return 2;
            }
            return 2;

       
        }

        }
}
