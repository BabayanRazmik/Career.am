using DataAccess.Models;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Interfaces
{
    public interface IJobRepository
    {
        public Task<List<LocationModel>> GetLocations();
        public Task<List<IndustryModel>> GetIndustries();
        public Task<List<TypeEmploymentModel>> GetTypeEmployments();
        public Task AddJob(JobModel job);
        public Task<List<JobModel>> GetActiveJob(string search, int? locationId, int? IndustryId, int? TypeEmploymentId);
        public Task<JobModel> GetJob(int jobId, int userId);
        public Task<JobModel> GEditJob(int jobId, int userId);
        public Task PEditJob(JobModel editJob);
        Task<List<JobModel>> GetInActiveJob();
        Task Apply(int userId, int? jobId);
        Task Delete(int jobId, int userId);
        Task<List<JobModel>> MyJob(string search, int? locationId, int? IndustryId, int? TypeEmploymentId, int userId);
        Task<List<User>> ApplaysJob(int jobId);
    }
}
