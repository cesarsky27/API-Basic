using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, string>
    {
        private readonly MyContext myContext;
        public UniversityRepository (MyContext myContext) : base (myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<RegisteredByUniversityVM> GetEmpCountByUniv()
        {
            var result = (from p in myContext.Profillings
                          join ed in myContext.Educations on p.EducationID equals ed.EducationID
                          join un in myContext.Universities on ed.UniversityID equals un.UniversityID
                          group un by new { ed.UniversityID, un.UniversityName } into Group
                          select new RegisteredByUniversityVM
                          {
                              UniversityID = Group.Key.UniversityID,
                              UniversityName = Group.Key.UniversityName,
                              Value = Group.Count()
                          });

            return result.ToList();
        }

    }
}
