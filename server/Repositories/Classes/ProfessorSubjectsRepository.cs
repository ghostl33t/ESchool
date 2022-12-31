using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.ProfessorSubjects;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class ProfessorSubjectsRepository : IProfessorSubjectsRepository
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        public ProfessorSubjectsRepository(DBMain dbMain,DBRegistries dbRegistries)
        {
            _dbMain = dbMain;
            _dbRegistries = dbRegistries;
        }
        public async Task<long> CreateProfSubj(ProfessorSubjects newProfSubj)
        {
            try
            {
                newProfSubj.Professor = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newProfSubj.ProfessorId_);
                await _dbMain.ProfessorSubjects.AddAsync(newProfSubj);
                await _dbMain.SaveChangesAsync();
                return newProfSubj.ID;
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }
        public async Task<long> UpdateProfSubj(ProfessorSubjects profSubj)
        {
            try
            {
                _dbMain.ProfessorSubjects.Update(profSubj);
                await _dbMain.SaveChangesAsync();
                return profSubj.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> DeleteProfSubj(long Id, long AdministratorId)
        {
            try
            {
                var profsubj = await _dbMain.ProfessorSubjects.FirstOrDefaultAsync(s => s.ID == Id);
                profsubj.Deleted = 1;
                profsubj.DeletedDate = DateTime.Now;
                profsubj.DeletedById = AdministratorId;
                _dbMain.ProfessorSubjects.Update(profsubj);
                await _dbMain.SaveChangesAsync();
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<GetProfessorSubjects>> GetProfessorsAndSubjects()
        {
            try
            {
                var query = from profsubjects in _dbMain.ProfessorSubjects
                            join professors in _dbMain.Users on profsubjects.Professor.Id equals professors.Id
                            join subjects in _dbRegistries.Subjects on profsubjects.SubjectId equals subjects.Id
                            select new
                            {
                                ProfessorNameAndSurname = professors.Name + " " + professors.LastName,
                                SubjectName = subjects.Name 
                            };
                List<GetProfessorSubjects> data = new();
                foreach(var row in query)
                {
                    GetProfessorSubjects newItem = new();
                    newItem.ProfessorNameAndSurname = row.ProfessorNameAndSurname;
                    newItem.SubjectName = row.SubjectName;
                    data.Add(newItem);
                }
                return await Task.FromResult(data);
            }   
            catch(Exception)
            {
                throw;
            }
        }
    }
}
