using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAL
{
    public class UserRepository : IUserRepository
    {
        #region Context

        private ClientContext _context;

        public UserRepository(ClientContext context)
        {
            _context = context;
        }

        #endregion

        

    }
}
