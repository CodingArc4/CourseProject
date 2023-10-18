using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService JobService;
        public JobController(IJobService jobService)
        {
            JobService = jobService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJob(JobCreate jobCreate)
        {
            var id = await JobService.CreateJobAsync(jobCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateJob(JobUpdate jobUpdate)
        {
            await JobService.UpdateJobAsync(jobUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteJob(JobDelete jobDelete)
        {
            await JobService.DeleteJobAsync(jobDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await JobService.GetJobAsync(id);
            return Ok(job);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await JobService.GetJobsAsync();
            return Ok(jobs);
        }
    }
}