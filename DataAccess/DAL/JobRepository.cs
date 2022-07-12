using DataAccess.DAL.Interfaces;
using DataAccess.Models;
using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    class JobRepository : IJobRepository
    {
        #region Context

        private ClientContext _context;
        public JobRepository(ClientContext context)
        {
            _context = context;
        }

        #endregion

        #region Get Locations

        public async Task<List<LocationModel>> GetLocations()
        {
            var locations = await _context.Locations
                .Select(l => new LocationModel
                {
                    Id = l.Id,
                    Name = l.Name
                }).ToListAsync();

            return locations;
        }

        #endregion

        #region Get Industry

        public async Task<List<IndustryModel>> GetIndustries()
        {
            var industries = await _context.Industries
                .Select(i => new IndustryModel
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToListAsync();

            return industries;
        }

        #endregion

        #region Get Type Employment

        public async Task<List<TypeEmploymentModel>> GetTypeEmployments()
        {
            var typeEmployments = await _context.TypeEmployments
                .Select(te => new TypeEmploymentModel
                {
                    Id = te.Id,
                    Name = te.Type
                }).ToListAsync();

            return typeEmployments;
        }

        #endregion

        #region Add Job

        public async Task AddJob(JobModel Pjob)
        {
            Job job = new Job();
            job.Title = Pjob.Title;
            job.CompanyName = Pjob.CompanyName;
            job.Requirement = Pjob.Requirement;
            job.Responsibilities = Pjob.Responsibilities;
            job.AppProcedures = Pjob.AppProcedures;
            job.UserId = Pjob.UserId;
            job.LocationId = Pjob.LocationId;
            job.IndustryId = Pjob.IndustryId;
            job.TypeEmploymentId = Pjob.TypeEmploymentId;
            job.Deadline = Pjob.Deadline;
            job.Salary = Pjob.Salary;
            job.StartDate = DateTime.Now;
            job.Status = true;

            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Get Active Job
        public async Task<List<JobModel>> GetActiveJob(string search, int? locationId, int? IndustryId, int? TypeEmploymentId)
        {
            return await (from j in _context.Jobs
                          where ((string.IsNullOrEmpty(search) || j.Title.ToLower().Contains(search.ToLower()) || j.Requirement.ToLower().Contains(search.ToLower()))
                          && (!locationId.HasValue || locationId == j.LocationId)
                          && (!IndustryId.HasValue || IndustryId == j.IndustryId)
                          && (!TypeEmploymentId.HasValue || TypeEmploymentId == j.TypeEmploymentId)
                          && (j.Deadline > DateTime.Now && j.Status == true))
                          orderby j.Id descending
                          select new JobModel
                          {
                              Id = j.Id,
                              Title = j.Title,
                              CompanyName = j.CompanyName,
                              Requirement = j.Requirement,
                              Responsibilities = j.Responsibilities,
                              AppProcedures = j.AppProcedures,
                              Deadline = j.Deadline,
                              Status = j.Status,
                              Salary = j.Salary,
                              LocationId = j.LocationId,
                              IndustryId = j.IndustryId,
                              TypeEmploymentId = j.TypeEmploymentId
                          }).ToListAsync();
        }

        #endregion

        #region Get Job
        public async Task<JobModel> GetJob(int jobId, int userId)
        {
            var job = await _context.Jobs.Where(j => j.Id == jobId).SingleOrDefaultAsync();
            JobModel getJob = new JobModel();
            getJob.Title = job.Title;
            getJob.Id = job.Id;
            getJob.UserId = job.UserId;
            getJob.CompanyName = job.CompanyName;
            getJob.Requirement = job.Requirement;
            getJob.Responsibilities = job.Responsibilities;
            getJob.AppProcedures = job.AppProcedures;
            getJob.Salary = job.Salary;
            getJob.Status = job.Status;
            getJob.Deadline = job.Deadline;
            Industry industry = await _context.Industries.Where(i => i.Id == job.IndustryId).SingleOrDefaultAsync();
            getJob.IndustryName = industry.Name;
            Location location = await _context.Locations.Where(l => l.Id == job.LocationId).SingleOrDefaultAsync();
            getJob.LocationName = location.Name;
            TypeEmployment typeEmp = await _context.TypeEmployments.Where(te => te.Id == job.TypeEmploymentId).SingleOrDefaultAsync();
            getJob.TypeEmployments = typeEmp.Type;
            getJob.IsApplaied = await _context.Applays.AnyAsync(x => x.JobId == jobId && x.UserId == userId);

            return getJob;
        }
        #endregion

        #region Edit Job
        //Get
        public async Task<JobModel> GEditJob(int jobId, int userId)
        {
            var job = await _context.Jobs.Where(j => j.Id == jobId && j.UserId == userId).SingleOrDefaultAsync();
            if (job != null)
            {
                JobModel getJob = new JobModel();
                getJob.Title = job.Title;
                getJob.Id = job.Id;
                getJob.CompanyName = job.CompanyName;
                getJob.Requirement = job.Requirement;
                getJob.Responsibilities = job.Responsibilities;
                getJob.AppProcedures = job.AppProcedures;
                getJob.Salary = job.Salary;
                getJob.Status = job.Status;
                getJob.Deadline = job.Deadline;
                Industry industry = await _context.Industries.Where(i => i.Id == job.IndustryId).SingleOrDefaultAsync();
                getJob.IndustryName = industry.Name;
                Location location = await _context.Locations.Where(l => l.Id == job.LocationId).SingleOrDefaultAsync();
                getJob.LocationName = location.Name;
                TypeEmployment typeEmp = await _context.TypeEmployments.Where(te => te.Id == job.TypeEmploymentId).SingleOrDefaultAsync();
                getJob.TypeEmployments = typeEmp.Type;
                return getJob;
            }

            return null;
        }

        //Post
        public async Task PEditJob(JobModel editJob)
        {
            var job = _context.Jobs.Where(j => j.Id == editJob.Id && j.UserId == editJob.UserId).FirstOrDefault();
            job.Title = editJob.Title;
            job.CompanyName = editJob.CompanyName;
            job.LocationId = editJob.LocationId;
            job.IndustryId = editJob.IndustryId;
            job.TypeEmploymentId = editJob.TypeEmploymentId;
            job.Deadline = editJob.Deadline;
            job.Status = editJob.Status;
            job.Salary = editJob.Salary;
            job.Requirement = editJob.Requirement;
            job.Responsibilities = editJob.Responsibilities;
            job.AppProcedures = editJob.AppProcedures;

            await _context.SaveChangesAsync();
        }

        #endregion

        #region Get Inactive Jobe
        public async Task<List<JobModel>> GetInActiveJob()
        {
            return await (from j in _context.Jobs
                          where (j.Deadline < DateTime.Now || j.Status == false)
                          orderby j.Id descending
                          select new JobModel
                          {
                              Id = j.Id,
                              Title = j.Title,
                              CompanyName = j.CompanyName,
                              Requirement = j.Requirement,
                              Responsibilities = j.Responsibilities,
                              AppProcedures = j.AppProcedures,
                              Deadline = j.Deadline,
                              Status = j.Status,
                              Salary = j.Salary,
                              LocationId = j.LocationId,
                              IndustryId = j.IndustryId,
                              TypeEmploymentId = j.TypeEmploymentId
                          }).ToListAsync();
        }
        #endregion

        #region Applay Job

        public async Task Apply(int userId, int? jobId)
        {
            Applay app = new Applay();
            app.UserId = userId;
            app.JobId = jobId;

            await _context.AddAsync(app);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public async Task Delete(int jobId, int userId)
        {
            var appDel = _context.Applays.Where(ad => ad.JobId == jobId).ToList();
            _context.Applays.RemoveRange(appDel);
            var jobDel = await _context.Jobs.Where(jd => jd.Id == jobId && jd.UserId == jd.UserId).SingleOrDefaultAsync();
            _context.Jobs.Remove(jobDel);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region My Job

        public async Task<List<JobModel>> MyJob(string search, int? locationId, int? IndustryId, int? TypeEmploymentId, int userId)
        {
            return await (from j in _context.Jobs
                          where ((string.IsNullOrEmpty(search) || j.Title.ToLower().Contains(search.ToLower()) || j.Requirement.ToLower().Contains(search.ToLower()))
                          && (!locationId.HasValue || locationId == j.LocationId)
                          && (!IndustryId.HasValue || IndustryId == j.IndustryId)
                          && (!TypeEmploymentId.HasValue || TypeEmploymentId == j.TypeEmploymentId)
                          && (j.UserId == userId))
                          orderby j.Id descending
                          select new JobModel
                          {
                              Id = j.Id,
                              Title = j.Title,
                              CompanyName = j.CompanyName,
                              Requirement = j.Requirement,
                              Responsibilities = j.Responsibilities,
                              AppProcedures = j.AppProcedures,
                              Deadline = j.Deadline,
                              Status = j.Status,
                              Salary = j.Salary,
                              LocationId = j.LocationId,
                              IndustryId = j.IndustryId,
                              TypeEmploymentId = j.TypeEmploymentId
                          }).ToListAsync();
        }

        #endregion
    }
}
