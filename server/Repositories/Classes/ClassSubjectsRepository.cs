using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.ClassSubjects;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes;
public class ClassSubjectsRepository : IClassSubjects
{
    private readonly DBMain _dbMain;

	public ClassSubjectsRepository(DBMain dbMain)
	{
		_dbMain = dbMain;
	}
    public async Task<List<GetClassSubject>> GetSubjectsPerClass(long classDepartmentId)
    {
        try
        {
            List<GetClassSubject> subjects = new List<GetClassSubject>();
            // Dodati vezu na ProfessorClassDepartment i to ce potvrdit sigurnost
            var query = from classSubject in _dbMain.ClassSubjects
                        join subject in _dbMain.Subjects on classSubject.Subject.Id equals subject.Id
                        join classdep in _dbMain.ClassDepartments on classSubject.ClassDepartment.ID equals classdep.ID
                        join professorsubjects in _dbMain.ProfessorSubjects on subject.Id equals professorsubjects.SubjectId
                        join professor in _dbMain.Users on professorsubjects.Professor.Id equals professor.Id
                        join professorclassdep in _dbMain.ClassProfessors on professor.Id equals professorclassdep.Professor.Id
                        where classSubject.ClassDepartment.ID == classDepartmentId
                        select new
                        {
                            SubjectName = subject.Name,
                            ClassDepartment = classdep.SerialNumber + " - " + classdep.Name,
                            ProfessorNameAndSurname = professor.Name + " " + professor.LastName
                        };
            foreach (var row in query)
            {
                GetClassSubject item = new()
                {
                    SubjectName = row.SubjectName,
                    ClassDepartment = row.ClassDepartment,
                    ProfessorNameAndSurname = row.ProfessorNameAndSurname
                };
                subjects.Add(item);
            }
            return subjects;
        }
        catch (Exception)
        {

            throw;
        }
        
    }
    public async Task<long> CreateClassSubjects(ClassSubjects classSubject)
    {
        try
        {
            classSubject.Subject = await _dbMain.Subjects.FirstOrDefaultAsync(s => s.Id == classSubject.SubjectId_);
            classSubject.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classSubject.ClassDepartmentId_);
            classSubject.CreatedBy = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == classSubject.CreatedById_);
            await _dbMain.ClassSubjects.AddAsync(classSubject);
            await _dbMain.SaveChangesAsync();
            return classSubject.ID;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<long> UpdateClassSubjects(long Id, ClassSubjects classSubjects)
    {
        try
        {
            classSubjects.ID = Id;
            classSubjects.Subject = await _dbMain.Subjects.FirstOrDefaultAsync(s => s.Id == classSubjects.SubjectId_);
            classSubjects.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classSubjects.ClassDepartmentId_);
            _dbMain.ClassSubjects.Update(classSubjects);
            await _dbMain.SaveChangesAsync();
            return Id;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<long> DeleteClassSubjects(long classSubjectId, long leaderId)
    {
        try
        {
            var classSubject = await _dbMain.ClassSubjects.FirstOrDefaultAsync(s => s.ID == classSubjectId);
            classSubject.Deleted = 1;
            classSubject.DeletedDate = DateTime.Now;
            classSubject.DeletedById = leaderId;
            return classSubjectId;
        }
        catch (Exception)
        {
            throw;
        }
     
    }
}
