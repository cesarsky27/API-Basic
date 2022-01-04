﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }
}
