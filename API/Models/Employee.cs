using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        //[Required(ErrorMessage = "Data tidak boleh ada yang kosong!")]
        [Key]
        public string NIK { get; set; }
        [Required(ErrorMessage ="Nama depan harus diisi!")]
        [MinLength(3, ErrorMessage = "Nama depan minimal 3 karakter! ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Nama belakang harus diisi!")]
        [MaxLength(20, ErrorMessage = "Nama belakang maksimal 20 karakter!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Nomor handphone wajib diisi!")]
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }
        [Range(1000000, 20000000, ErrorMessage = "Gaji yang dimasukkan hanya dapat 1.000.000 - 20.000.000")]
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}

public enum Gender
{
    Male,
    Female
}
