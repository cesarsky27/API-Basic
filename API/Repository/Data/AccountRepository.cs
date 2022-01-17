using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using MimeKit;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        public AccountRepository (MyContext myContext) : base (myContext)
        {
            this.myContext = myContext;
        }
        //1.1 FIX
        public string Login(LoginVM loginVM)
        {
            var result = "0";
            var cekPassword = "";
            try
            {
                var cekEmail = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                //var cekPhone = myContext.Employees.Where(e => e.Phone == loginVM.Phone).FirstOrDefault();


                if (cekEmail != null)
                    //if (cekEmail != null || cekPhone != null)
                    if(cekEmail != null)
                    {
                        var NIK = (from e in myContext.Set<Employee>()
                                   where e.Email == loginVM.Email
                                   //where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                   select e.NIK).Single();

                        cekPassword = (from e in myContext.Set<Employee>()
                                       join a in myContext.Set<Account>() on e.NIK equals a.NIK
                                       where e.Email == loginVM.Email
                                       //where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                       select a.Password).Single();

                        var passTry = Hashing.ValidatePassword(loginVM.Password, cekPassword);
                    if (passTry != false)
                    {
                        result = NIK;
                    }
                    else
                    {
                        result = "2";
                    }
                }
                else
                {
                    result = "3";
                }
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }

        public IEnumerable<ProfileVM> GetProfile(string Key)
        {
            var cekNIK = myContext.Employees.Find(Key);

            if (cekNIK != null)
            {
                var getProfile = (from e in myContext.Employees
                                  where e.NIK == Key
                                  join a in myContext.Accounts on e.NIK equals a.NIK
                                  join p in myContext.Profillings on a.NIK equals p.NIK
                                  join ed in myContext.Educations on p.EducationID equals ed.EducationID
                                  join u in myContext.Universities on ed.UniversityID equals u.UniversityID

                                  select new ProfileVM
                                  {
                                      NIK = e.NIK,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Phone = e.Phone,
                                      BirthDate = e.BirthDate,
                                      Salary = e.Salary,
                                      Email = e.Email,
                                      EducationId = p.EducationID,
                                      Degree = ed.Degree,
                                      GPA = ed.GPA,
                                      UniversityId = ed.UniversityID,
                                      Name = u.UniversityName
                                  });
                return getProfile;
            }
            return null;
        }

        //Change Password - FIX
        public int ChangePassword(ChangePasswordVM changepassVM)
        {
            var cekEmail = myContext.Employees.Where(cp => cp.Email == changepassVM.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                if (changepassVM.NewPass == changepassVM.ConfPass)
                {
                    var NIK = (from e in myContext.Set<Employee>()
                               where e.Email == changepassVM.Email
                               join a in myContext.Set<Account>() on e.NIK equals a.NIK
                               select e.NIK).Single();
                    var password = (from e in myContext.Set<Employee>()
                                    where e.Email == changepassVM.Email
                                    join a in myContext.Set<Account>() on e.NIK equals a.NIK
                                    select a.Password).Single();
                    var dataAkun = myContext.Accounts.Find(NIK);
                    if (dataAkun.OTP != changepassVM.OTP)
                    {
                        return 4;
                    }
                    else
                    {
                        if (dataAkun.ExpiredToken <= DateTime.Now)
                        {
                            return 6; //Expired Token
                        }
                        else
                        {
                            if (dataAkun.IsUsed)
                            {
                                return 2; //Token telah digunakan
                            }
                            else
                            {
                                if (dataAkun != null)
                                {
                                    myContext.Entry(dataAkun).State = EntityState.Detached;
                                }
                                dataAkun.Password = Hashing.HashPassword(changepassVM.NewPass);
                                //dataAkun.Password = changepassVM.NewPass;
                                dataAkun.IsUsed = true;
                                myContext.Entry(dataAkun).State = EntityState.Modified;
                                myContext.SaveChanges();
                                return 1; //Berhasil
                            }
                        }

                    }                   
                }
                else
                {
                    return 3;
                }
            }
            return 2;
        }

        //Forgot Password - FIX
        public Employee CekDataEmployee (String param)
        {
            var respond = myContext.Employees.Where(em => em.Email == param).FirstOrDefault();
            return respond;
        }
        public Account CekDataAccount (String param)
        {
            var respond = myContext.Accounts.Find(param);
            return respond;
        }
        public int GenerateToken()
        {
            int min = 11111;
            int max = 99999;
            Random rand = new Random();
            return rand.Next(min, max);
        }
       public int ForgotPassword (ForgotPasswordVM forgotPasswordVM)
        {
            var cekEmail = myContext.Employees.Where(em => em.Email == forgotPasswordVM.Email).FirstOrDefault();
            var tokenGenerate = GenerateToken();
            var cekAkun = CekDataAccount(cekEmail.NIK);
            if (cekEmail != null)
            {
                var FirstName = (from e in myContext.Set<Employee>()
                                 where e.Email == forgotPasswordVM.Email
                                 select e.FirstName).Single();
                var LastName = (from e in myContext.Set<Employee>()
                                where e.Email == forgotPasswordVM.Email
                                select e.LastName).Single();

                //Mengenerate Token, Expired Token dan Penggunaan Token
                cekAkun.OTP = tokenGenerate;
                var timenow = DateTime.Now.AddMinutes(5);
                cekAkun.ExpiredToken = timenow;
                cekAkun.IsUsed = false;

                myContext.Entry(cekAkun).State = EntityState.Modified;
                var dbSave = myContext.SaveChanges();
                var mailContent = new MailContent
                {
                    Email = cekEmail.Email,
                    TimeNow = timenow,
                    Token = tokenGenerate,
                    Body = $"{cekEmail.LastName}, {cekEmail.FirstName}"
                };
                var name = FirstName + " " + LastName;
                var NIK = (from e in myContext.Set<Employee>()
                           where e.Email == forgotPasswordVM.Email
                           join a in myContext.Set<Account>() on e.NIK equals a.NIK
                           select e.NIK).Single();
                var original = myContext.Accounts.Find(NIK);
                DateTimeOffset time = (DateTimeOffset)DateTime.Now;
                //Isi Email
                if (original != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(forgotPasswordVM.Email);
                    mail.From = new MailAddress("caesar.apridarkar27@gmail.com", "Forgot Password!", System.Text.Encoding.UTF8);
                    mail.Subject = "Kode OTP " + time;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = "Hi " + name + "<br/> Kode token Anda adalah: " + tokenGenerate;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    
                    //Koneksikan Email
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("botemailmii@gmail.com", "Dafi99@@");
                    //client.Credentials = new System.Net.NetworkCredential("caesar.bot.mii@gmail.com", "Caesar123");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    try
                    {
                        client.Send(mail);
                        return 1;
                    }
                    catch (Exception)
                    {

                        return 3;
                    }
                }
               
            }
            return 2;
        }             
    }
}
