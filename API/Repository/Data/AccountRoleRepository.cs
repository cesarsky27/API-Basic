using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository <MyContext, AccountRole, string>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base (myContext)
        {
            this.myContext = myContext;
        }
        public int SignManager(AccountRoleVM accountRoleVM)
        {
            AccountRole ar = new AccountRole()
            {
                RoleId = accountRoleVM.RoleId,
                AccountNIK = accountRoleVM.AccountNIK
            };
            myContext.AccountRoles.Add(ar);
            var result = myContext.SaveChanges();

            return result;
        }
    }
}
