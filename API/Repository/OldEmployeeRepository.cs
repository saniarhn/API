using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class OldEmployeeRepository : Interface.IEmployeeRepository
    {
        private readonly MyContext context;

        public OldEmployeeRepository(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            /*throw new NotImplementedException();*/
            var entity = context.Employees.Find(NIK);
            /* apabila nik tidak ditemukan*/
            if (entity == null)
            {
                return 0;
            }
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            /*throw new NotImplementedException();*/
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            /*throw new NotImplementedException();*/
            return context.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            /*throw new NotImplementedException();*/
            var cekNik = context.Employees.Find(employee.NIK);
            /*  apabila nik sudah ada maka kembalikan nilai 0, 
             *  artinya tidak terjadi penambahan data, karena eror karena double primarykey*/
            if (cekNik != null)
            {
                return 0;
            }
            context.Employees.Add(employee);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            /*throw new NotImplementedException();*/


            var update = context.Entry(employee).State = EntityState.Modified;
            var result = 0;
            try
            {
                var cekNik = context.Employees.Find(employee.NIK);
                /*  apabila nik tidak ada maka kembalikan nilai 0, 
                 *  artinya tidak dapat terjadi perubahan data*/
                if (cekNik == null)
                {
                    result = 0;
                }
                result = context.SaveChanges();
            }
            catch (Exception )
            {
                result = 0;
            }


            return result;
        }
    }
}
