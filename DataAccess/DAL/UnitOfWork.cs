using DataAccess.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region DB

        private readonly ClientContext _context;
        public UnitOfWork(DbContextOptions<ClientContext> options)
        {
            _context = new ClientContext(options);
        }

        #endregion

        #region User Repository

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }

        #endregion

        #region Job Repository

        private IJobRepository jobRepository;
        public IJobRepository JobRepository 
        {
            get 
            {
                if (this.jobRepository == null)
                {
                    this.jobRepository = new JobRepository(_context);
                }
                return jobRepository;
            }
        }

        #endregion

    }
}
