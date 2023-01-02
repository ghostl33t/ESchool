using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes;

public class ClassProfessorRepository : IClassProfessor
{
    private readonly DBMain _dbMain;
    public ClassProfessorRepository(DBMain dbMain)
    {
        _dbMain = dbMain;
    }

    //get

    //post
    public async Task<long> CreateClassProfessor(ClassProfessors classProfessor)
    {
        try
        {
            await _dbMain.ClassProfessors.AddAsync(classProfessor);
            await _dbMain.SaveChangesAsync();
            return classProfessor.ID;
        }
        catch (Exception)
        {

            throw;
        }
    }
    //patch
    public async Task<long> UpdateClassProfessor(long Id,ClassProfessors classProfessor)
    {
        try
        {
            classProfessor.ID = Id;
            _dbMain.ClassProfessors.Update(classProfessor);
            await _dbMain.SaveChangesAsync();
            return Id;
        }
        catch (Exception)
        {

            throw;
        }
    }
    //patch-delete
    public async Task<long> DeleteClassProfessor(long classProfId, long leaderId)
    {
        try
        {
            var classProf = await _dbMain.ClassProfessors.AsNoTracking().FirstOrDefaultAsync(s => s.ID == classProfId);
            classProf.Deleted = 1;
            classProf.DeletedById = leaderId;
            classProf.DeletedDate = DateTime.Now;
            _dbMain.ClassProfessors.Update(classProf);
            await _dbMain.SaveChangesAsync();
            return classProfId;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
