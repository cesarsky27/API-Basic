using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base (myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            int increment = myContext.Employees.ToList().Count + 1;
            //string NIK = DateTime.Now.ToString("yyyy") + "0" + increment.ToString();
            var cekNIK = myContext.Employees.Find(registerVM.NIK);
            var cekPhone = myContext.Employees.Where(p => p.Phone == registerVM.Phone).FirstOrDefault();
            var cekEmail = myContext.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault();
            if (cekNIK != null)
            {
                return 1;
            }
            if (cekPhone != null)
            {
                return 2;
            }
            if (cekEmail != null)
            {
                return 3;
            }

            Employee e = new Employee()
            {
                //int increment =myContext.Employees.ToList().Count;              
                //NIK = DateTime.Now.ToString("yyyymmddHHmmss"),
                NIK = DateTime.Now.ToString("yyyy") + "0" + increment.ToString(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Gender)registerVM.Gender
            };

            myContext.Employees.Add(e);
            myContext.SaveChanges();

            Account a = new Account()
            {
                NIK = e.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            myContext.Accounts.Add(a);
            myContext.SaveChanges();

            Education ed = new Education()
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityID = registerVM.UniversityID
            };

            myContext.Educations.Add(ed);
            myContext.SaveChanges();

            AccountRole ar = new AccountRole()
            {
                AccountNIK = e.NIK,
                RoleId = 1
            };
            myContext.AccountRoles.Add(ar);
            myContext.SaveChanges();

            Profilling p = new Profilling()
            {
                NIK = e.NIK,
                EducationID = ed.EducationID
            };

            return 0;
        }

        public IEnumerable GetRegisteredData()
        {
            var getData = from e in myContext.Set<Employee>()
                          join a in myContext.Set<Account>() on e.NIK equals a.NIK
                          join p in myContext.Set<Profilling>() on a.NIK equals p.NIK
                          join ed in myContext.Set<Education>() on p.EducationID equals ed.EducationID
                          join u in myContext.Set<University>() on ed.UniversityID equals u.UniversityID

                          select new
                          {
                              e.NIK,
                              e.FirstName,
                              e.LastName,
                              Gender = e.Gender == 0 ? "Male" : "Female",
                              e.Phone,
                              e.BirthDate,
                              e.Salary,
                              e.Email,
                              a.Password,
                              ed.Degree,
                              ed.GPA,
                              u.UniversityName
                          };
            return getData.ToList();
            }
            
        //Include
            //public IEnumerable GetRegisteredData2()
            //{
            //    var getData2 = myContext.Employees
            //        .Include(ac => ac.Account)
            //        .ThenInclude(ac => ac.Profilling)
            //        .ThenInclude(edc => edc.Education)
            //        .ThenInclude(univ => univ.University);
            //    return getData2.ToList();
            //}
        }
}
