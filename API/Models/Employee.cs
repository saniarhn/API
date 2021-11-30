using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
       
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
/*        public bool ShouldSerializeAccount()
        {
            if (Account == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/
    }
        public enum Gender
        {
            Male,
            Female
        }


    


    
}
