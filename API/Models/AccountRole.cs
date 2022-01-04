using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table ("tb_m_accountrole")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleID { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string AccountNIK { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
