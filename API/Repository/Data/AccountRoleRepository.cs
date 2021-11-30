using API.Context;
using API.Models;
using API.Repository.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, string>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int SignManager(AccountRole accountRole)
        {
            var cekNik = myContext.Employees.Find(accountRole.AccountNIK);
            if(cekNik != null)
            {
                AccountRole r = new AccountRole()
                {
                    RoleId = 2,
                    AccountNIK = accountRole.AccountNIK
                };
                myContext.AccountRoles.Add(r);
            }
  
            var result = myContext.SaveChanges();
            return result;


        }
    }
}
