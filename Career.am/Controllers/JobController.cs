using DataAccess.DAL.Interfaces;
using DataAccess.Models;
using DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.am.Extensions;

namespace Career.am.Controllers
{
    public class JobController : BaseController
    {
        #region UOW

        public JobController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        #endregion

        #region ViewBag

        public async Task LoadDropDownInfos()
        {
            var locations = await _unitOfWork.JobRepository.GetLocations();
            ViewBag.locations = new SelectList(locations, "Id", "Name");

            var industries = await _unitOfWork.JobRepository.GetIndustries();
            ViewBag.industries = new SelectList(industries, "Id", "Name");

            var typeEmployments = await _unitOfWork.JobRepository.GetTypeEmployments();
            ViewBag.typeEmployments = new SelectList(typeEmployments, "Id", "Name");
        }

        #endregion

        #region Add JOB

        [HttpGet]
        public async Task<IActionResult> AddJob()
        {
            await LoadDropDownInfos();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddJob(JobModel job)
        {
            if (ModelState.IsValid && job.Deadline >= DateTime.Now && job.Deadline <= DateTime.Now.AddMonths(1))
            {
                var user = await _userManager.GetUserAsync(User);
                job.UserId = user.Id;
                await _unitOfWork.JobRepository.AddJob(job);
                return RedirectToAction("ListActiveJob", "Job");
            }
            else
            {
                await LoadDropDownInfos();
            }
            return View(job);
        }
        #endregion

        #region List Job

        [HttpGet]
        public async Task<IActionResult> ListActiveJob(string search, int? locationId, int? IndustryId, int? TypeEmploymentId)
        {
            await LoadDropDownInfos();

            var listActiveJob = await _unitOfWork.JobRepository.GetActiveJob(search, locationId, IndustryId, TypeEmploymentId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListActiveJob", listActiveJob);
            }

            return View(listActiveJob);
        }

        #endregion

        #region View Job
        [HttpGet]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var job = await _unitOfWork.JobRepository.GetJob(jobId, user.Id);

            return View(job);
        }

        #endregion

        #region Edit Job

        [HttpGet]
        public async Task<IActionResult> EditJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var job = await _unitOfWork.JobRepository.GEditJob(jobId, user.Id);

            await LoadDropDownInfos();
            if (job != null)
            {
                return View(job);
            }
            else
            {
                return RedirectToAction("ListActiveJob", "Job");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobModel job)
        {
            if (ModelState.IsValid && job.Deadline >= DateTime.Now && job.Deadline <= DateTime.Now.AddMonths(1))
            {
                var user = await _userManager.GetUserAsync(User);
                job.UserId = user.Id;
                await _unitOfWork.JobRepository.PEditJob(job);

                return RedirectToAction("ListActiveJob", "Job");
            }
            else
            {
                await LoadDropDownInfos();
                ModelState.AddModelError("", "This file is required\n[min - today] | [max - up to a month]");
                return View(job);
            }
        }
        #endregion

        #region List Archive
        [HttpGet]
        public async Task<IActionResult> ArchiveList()
        {
            var list = await _unitOfWork.JobRepository.GetInActiveJob();
            return View(list);
        }
        #endregion

        #region Apply Job
        [HttpPost]
        public async Task<IActionResult> ApplyJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _unitOfWork.JobRepository.Apply(user.Id, jobId);
            return RedirectToAction("ListActiveJob", "Job");
        }

        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var user = _userManager.GetUserAsync(User);
            await _unitOfWork.JobRepository.Delete(jobId, user.Id);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region MyJob

        public async Task<IActionResult> MyJobs(string search, int? locationId, int? IndustryId, int? TypeEmploymentId)
        {
            var user = await _userManager.GetUserAsync(User);
            var listMyJob = await _unitOfWork.JobRepository.MyJob(search, locationId, IndustryId, TypeEmploymentId, user.Id);

            await LoadDropDownInfos();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListActiveJob", listMyJob);
            }

            return View(listMyJob);
        }

        #endregion
    }
}
