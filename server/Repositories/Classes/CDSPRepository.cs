using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Migrations.DBRegistriesMigrations;
using server.Models.Domain;
using server.Models.DTOs.ClassDepartmentSubjectProfessor;

namespace server.Repositories.Classes
{
    public class CDSPRepository : server.Repositories.Interfaces.ICDSP
    {
        private readonly DBRegistries DbRegistries;
        private readonly DBMain DbMain;
        private readonly IMapper IMapper;

        public CDSPRepository(DBRegistries DbRegistries, DBMain DbMain, IMapper IMapper)
        {
            this.DbRegistries = DbRegistries;
            this.DbMain = DbMain;
            this.IMapper = IMapper;
        }

        public async Task<List<server.Models.DTOs.ClassDepartmentSubjectProfessor.ClassDetails>> GetClassDetails(long classdepid)
        {
            var classDep = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classdepid && s.Deleted == 0);
            if (classDep == null)
            {
                return null;
            }
            var cdspexist = await this.DbMain.ClassDepartmentSubjectProfessors.Where(s => s.ClassDepartment.ID == classDep.ID).ToListAsync();
            if(cdspexist == null)
            {
                return null;
            }
            var cdspDTO = IMapper.Map<List<server.Models.Domain.ClassDepartmentSubjectProfessor>>(cdspexist);
            var subjects = await this.DbRegistries.Subjects.Where(s=>s.Deleted == 0).ToListAsync();
            var profs = await this.DbMain.Users.Where(s => s.UserType == 1 && s.Deleted == 0).ToListAsync();
            List<ClassDetails> classDetails = new List<ClassDetails>();
            var query = from cdspobj in cdspDTO
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
                ClassDetails classDetailsObject = new ClassDetails();
                classDetailsObject.ClassDepartmentSerial = classDep.SerialNumber;
                classDetailsObject.ClassDepartmentName = classDep.Name;
                classDetailsObject.SubjectName = cdspobj.SubjectName;
                classDetailsObject.SubjectSerial = cdspobj.SubjectSerial;
                classDetailsObject.ProfessorNameSurname = cdspobj.ProfessorName;
                classDetails.Add(classDetailsObject);
            }
            return classDetails;
        }
    }
}
