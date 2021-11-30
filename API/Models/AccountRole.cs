using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_ts_accountrole")]
    public class AccountRole
    {
        public int AccountRoleId { get; set; }
        public int RoleId { get; set; }
        public string AccountNIK { get; set; }
        public virtual Role Role { get; set; }
        public virtual Account Account { get; set; }
    }
}
