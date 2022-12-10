using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.ClassDepartment;
using server.Models.DTOs.StudentDetails;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class ClassDepartmentRepository : IClassDepartment
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DbRegistries;
        private readonly IMapper IMapper;
        
        public ClassDepartmentRepository(DBMain DbMain, DBRegistries DbRegistries, IMapper IMapper)
        {
            this.DbMain = DbMain;
            this.DbRegistries = DbRegistries;
            this.IMapper = IMapper;
        }
        public async Task<Models.DTOs.ClassDepartment.Create> CreateSchoolAsync(Models.DTOs.ClassDepartment.Create newclassdepp)
        {
            var classDep = IMapper.Map<Models.Domain.ClassDepartment>(newclassdepp);
            await DbMain.ClassDepartments.AddAsync(classDep);
            await DbMain.SaveChangesAsync();
            return newclassdepp;
        }

        public async Task<ClassDepartmentDTO> DeleteSchoolAsync(long Id)
        {
            try
            {
                var classdep = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                var classdepDTO = IMapper.Map<Models.DTOs.ClassDepartment.ClassDepartmentDTO>(classdep);
                if (classdep != null)
                {
                    classdep.Deleted = 1;
                    classdep.DeletedDate = DateTime.Today;
                    await DbMain.SaveChangesAsync();
                    return classdepDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async  Task<ClassDepartmentDTO> GetSchoolById(long Id)
        {
            try
            {
                var classDep = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                var classDepDTO = IMapper.Map<ClassDepartmentDTO>(classDep);
                if(classDepDTO != null)
                {
                    return  classDepDTO;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<List<ClassDepartmentDTO>> GetSchoolsList()
        {
            try
            {
                var classDeps = await DbMain.ClassDepartments.ToListAsync();
                var classDepsDTO = IMapper.Map<List<Models.DTOs.ClassDepartment.ClassDepartmentDTO>>(classDeps);
                if(classDepsDTO != null)
                {
                    return classDepsDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<ClassDepartmentDTO> ModifySchoolAsync(server.Models.DTOs.ClassDepartment.Update classdep)
        {
            try
            {
                var classDepExist = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classdep.ID);
                if(classDepExist != null)
                {
                    classDepExist.SerialNumber = classdep.SerialNumber;
                    classDepExist.Name = classdep.Name;
                    classDepExist.SchoolListId = classdep.SchoolListId;
                    var leadprofmap = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == classdep.LeaderProfessorId);
                    classDepExist.LeaderProfessor = leadprofmap;//classdep.LeaderProfessorId;
                    classDepExist.Year = classdep.Year;

                    await DbMain.SaveChangesAsync();
                    var classDepDTO = IMapper.Map<ClassDepartmentDTO>(classDepExist);
                    return classDepDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        //d)	Kreirati metodu koja ce za odredjeni razred vratiti listu svih studenata koji se nalaze u njemu
        // Ime (ime roditelja) Prezime | Vrsta skole | Razred | 

        public async Task<List<server.Models.DTOs.StudentDetails.StudentDetailsDTO>> GetStudentsPerClassDetilsAsync(long id)
        {
            var classExist = await this.DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == id && s.Deleted == 0);
            if (classExist == null)
            {
                return null;
            }
            string classGrade = "";
            switch (classExist.Year)
            {
                case 1:
                    classGrade = "I";
                    break;
                case 2:
                    classGrade = "II";
                    break;
                case 3:
                    classGrade = "III";
                    break;
                case 4:
                    classGrade = "IV";
                    break;
                default:
                    break ;
            }
            var studentsFromClassList = await this.DbMain.StudentsDetails.Where(s => s.ClassDepartment.ID == id && s.Deleted == 0).ToListAsync();
            var usersList = await this.DbMain.Users.Where(s => s.Deleted == 0 && s.UserType == 2).ToListAsync();
            var schoolsType = await this.DbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Deleted == 0 && s.Id == classExist.SchoolListId);
            var query = from students in studentsFromClassList
                        join users in usersList on students.Student.Id equals users.Id
                        select new
                        {
                            Name = users.Name + ' ' + users.LastName,
                            SchoolType = schoolsType.Name,
                            ClassGrade = classGrade 
                        };
            List<StudentDetailsDTO> studentsList = new List<StudentDetailsDTO>();
            foreach(var row in query)
            {
                StudentDetailsDTO student = new StudentDetailsDTO();
                student.Name = row.Name;
                student.SchoolType = row.SchoolType;
                student.ClassGrade = row.ClassGrade;
                studentsList.Add(student);
            }
            return studentsList;
        }

    }
}
