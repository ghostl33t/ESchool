
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.ClassDepartmentSubjectProfessor;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes;

public class CDSPRepository : ICDSP
{
    private readonly DBRegistries _dbRegistries;
    private readonly DBMain _dbMain;
    public CDSPRepository(DBRegistries dbRegistries, DBMain dbMain)
    {
        this._dbRegistries = dbRegistries;
        this._dbMain = dbMain;
    }
    public async Task<long> CreateCDSP(ClassDepartmentSubjectProfessor newcdsp)
    {
        try
        {
            newcdsp.UserProfessor = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newcdsp.ProfessorId);
            newcdsp.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == newcdsp.ClassDepId);
            await _dbMain.ClassDepartmentSubjectProfessors.AddAsync(newcdsp);
            await _dbMain.SaveChangesAsync();
            return newcdsp.Id;
        }
        catch (Exception)
        {

            throw;
        }   
    }
    public async Task<long> ModifyCDSP(ClassDepartmentSubjectProfessor cdsp)
    {
        try
        {
            cdsp.UserProfessor = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == cdsp.ProfessorId);
            cdsp.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == cdsp.ClassDepId);
            _dbMain.ClassDepartmentSubjectProfessors.Update(cdsp);
            await _dbMain.SaveChangesAsync();
            return cdsp.Id;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<long> DeleteCDSP(long cdspId, long administratorId)
    {
        try
        {
            var cdspExist = await _dbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.Id == cdspId);
            if (cdspExist == null)
            {
                return 0;
            }
            cdspExist.Deleted = 1;
            cdspExist.CreatedById = administratorId;
            cdspExist.DeletedDate = DateTime.Today;
            await _dbMain.SaveChangesAsync();
            return cdspId;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<List<GetClassDetails>> GetClassDetails(long classdepid)
    {
        try
        {
            var classDep = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classdepid && s.Deleted == 0);
            if (classDep == null)
            {
                return null;
            }
            var cdspexist = await this._dbMain.ClassDepartmentSubjectProfessors.Where(s => s.ClassDepartment.ID == classDep.ID).ToListAsync();
            if (cdspexist == null)
            {
                return null;
            }
            var subjects = await this._dbRegistries.Subjects.Where(s => s.Deleted == 0).ToListAsync();
            var profs = await this._dbMain.Users.Where(s => s.UserType == 1 && s.Deleted == 0).ToListAsync();
            List<GetClassDetails> classDetails = new List<GetClassDetails>();
            var query = from cdspobj in cdspexist
                        join subject in subjects on cdspobj.SubjectID equals subject.Id
                        join prof in profs on cdspobj.UserProfessor.Id equals prof.Id

                        select new
                        {
                            ProfessorName = prof.Name + " " + prof.LastName,
                            SubjectName = subject.Name,
                            SubjectSerial = subject.SerialNumber
                        };
            foreach (var cdspobj in query)
            {
                GetClassDetails classDetailsObject = new GetClassDetails();
                classDetailsObject.ClassDepartmentSerial = classDep.SerialNumber;
                classDetailsObject.ClassDepartmentName = classDep.Name;
                classDetailsObject.SubjectName = cdspobj.SubjectName;
                classDetailsObject.SubjectSerial = cdspobj.SubjectSerial;
                classDetailsObject.ProfessorNameSurname = cdspobj.ProfessorName;
                classDetails.Add(classDetailsObject);
            }
            return classDetails;
        }
        catch (Exception)
        {

            throw;
        }
        
    }
    public async Task<List<GetProfesorSubjectDetails>> GetProfessorSubjectDetails(long professorId)
    {
        var professorExist = await this._dbMain.Users.FirstOrDefaultAsync(s => s.Id == professorId);
        if(professorExist == null)
        {
            return null;
        } 
        if(professorExist.UserType != 1)
        {
            return null;
        }
        var subjectsList = await _dbRegistries.Subjects.Where(s => s.Deleted == 0).ToListAsync();
        var classDepList = await _dbMain.ClassDepartments.Where(s => s.Deleted == 0).ToListAsync();
        var cdspList = await _dbMain.ClassDepartmentSubjectProfessors.Where(s=> s.UserProfessor.Id == professorId && s.Deleted == 0).ToListAsync();
        var query = from cdsp
                    in cdspList
                    join subject in subjectsList on cdsp.SubjectID equals subject.Id
                    join classdep in classDepList on cdsp.ClassDepartment.ID equals classdep.ID
                    select new
                    {
                        ClassDepartmentDetails = classdep.SerialNumber + ' ' + classdep.Name,
                        SubjectDetails = subject.SerialNumber + ' ' + subject.Name
                    };
        List<GetProfesorSubjectDetails> ProfesorSubjectDetailsList = new List<GetProfesorSubjectDetails>();
        foreach(var row in query)
        {
            GetProfesorSubjectDetails profsubjobj = new GetProfesorSubjectDetails();
            profsubjobj.SubjectDetails = row.SubjectDetails;
            profsubjobj.ClassDetails = row.ClassDepartmentDetails;
            ProfesorSubjectDetailsList.Add(profsubjobj);
        }
        return ProfesorSubjectDetailsList;
    }
}
