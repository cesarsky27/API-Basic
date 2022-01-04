using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        public int EducationID { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public virtual University University { get; set; }
        public int UniversityID { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profilling> Profillings { get; set; }
    }
}
