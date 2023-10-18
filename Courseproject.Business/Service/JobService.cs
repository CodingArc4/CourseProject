using AutoMapper;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Interface;
using Courseproject.Common.Model;
using FluentValidation;

namespace Courseproject.Business.Service
{
    public class JobService : IJobService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<Job> JobRepository { get; }

        private JobCreateValidator JobCreateValidator { get; }
        private JobUpdateValidator JobUpdateValidator { get; }

        public JobService(IMapper mapper, IGenericRepository<Job> jobRepository,
            JobUpdateValidator jobUpdateValidator, JobCreateValidator jobCreateValidator)
        {
            Mapper = mapper;
            JobRepository = jobRepository;
            JobUpdateValidator = jobUpdateValidator;
            JobCreateValidator = jobCreateValidator;
        }


        public async Task<int> CreateJobAsync(JobCreate jobCreate)
        {
            await JobCreateValidator.ValidateAndThrowAsync(jobCreate);

            var entity = Mapper.Map<Job>(jobCreate);
            await JobRepository.InsertAsync(entity);
            await JobRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteJobAsync(JobDelete jobDelete)
        {
            var entity = await JobRepository.getByIdAsync(jobDelete.Id);
            JobRepository.Delete(entity);
            await JobRepository.SaveChangesAsync();
        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await JobRepository.getByIdAsync(id);
            return Mapper.Map<JobGet>(entity);
        }

        public async Task<List<JobGet>> GetJobsAsync()
        {
            var entities = await JobRepository.GetAsync(null, null);
            return Mapper.Map<List<JobGet>>(entities);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await JobUpdateValidator.ValidateAndThrowAsync(jobUpdate);

            var entity = Mapper.Map<Job>(jobUpdate);
            JobRepository.Update(entity);
            await JobRepository.SaveChangesAsync();
        }
    }
}
