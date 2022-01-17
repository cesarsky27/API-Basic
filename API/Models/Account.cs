using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
        //Belum Fix, kalau salah langsung comment!
        public int OTP { get; set; }
        public DateTime ExpiredToken { get; set; }
        public Boolean IsUsed { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual Profilling Profilling { get; set; }
        //[JsonIgnore]
        public virtual IEnumerable <AccountRole> AccountRole { get; set; }
        
    }
}
