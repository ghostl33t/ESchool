using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes;

public class SubjectRepository : ISubjects
{
    private readonly DBMain _dbMain;
    //private readonly DBRegistries _dbRegistries;
    public SubjectRepository(DBMain dbMain)
    {
        this._dbMain = dbMain;
        //this._dbRegistries = dbRegistries;
    }
    public async Task<long> CreateSubjectAsync(Subject newSubject)
    {
        try
        {
            await _dbMain.Subjects.AddAsync(newSubject);
            await _dbMain.SaveChangesAsync();
            return newSubject.Id;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<long> DeleteSubjectAsync(long SubjectId, long AdministratorId)
    {
        try
        {
            var subject = await this._dbMain.Subjects.FirstOrDefaultAsync(s => s.Id == SubjectId);
            subject.Deleted = 1;
            subject.DeletedDate = DateTime.Today;
            subject.DeletedById = AdministratorId;
            await this._dbMain.SaveChangesAsync();
            return subject.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async  Task<Subject> GetSubjectById(long Id)
    {
        try
        {
            var subject = await this._dbMain.Subjects.FirstOrDefaultAsync(s => s.Id == Id);
            return subject;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Subject>> GetSubjectsList()
    {
        try
        {
            var subjects = await this._dbMain.Subjects.Where(s=>s.Deleted == 0).ToListAsync();
            return subjects;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<long> ModifySubject(long Id, Subject subject)
    {
        try
        {
            if(subject != null)
            {
                _dbMain.Subjects.Update(subject);
                await this._dbMain.SaveChangesAsync();
            }
            return subject.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
