using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.StudentDetails;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class ClassDepartmentRepository : IClassDepartment
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        
        public ClassDepartmentRepository(DBMain dbMain, DBRegistries dbRegistries)
        {
            this._dbMain = dbMain;
            this._dbRegistries = dbRegistries;
        }
        public async Task<long> CreateClassDepartmentAsync(ClassDepartment newclassdepp)
        {
            try
            {
                newclassdepp.CreatedBy = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newclassdepp.CreatorId);
                newclassdepp.LeaderProfessor = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newclassdepp.ProfessorId);
                await _dbMain.ClassDepartments.AddAsync(newclassdepp);

                await _dbMain.SaveChangesAsync();
                return newclassdepp.ID;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<long> DeleteClassDepartmentAsync(long Id, long AdministratorId)
        {
            try
            {
                var classdep = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                if (classdep != null)
                {
                    classdep.DeletedById = AdministratorId;
                    classdep.Deleted = 1;
                    classdep.DeletedDate = DateTime.Today;
                    await _dbMain.SaveChangesAsync();
                    return classdep.ID;
                }
                return 0;
            }
            catch (Exception)
            { 
                throw;
            }
        }
        public async Task<ClassDepartment> GetClassDepartmentByIdAsync(long Id)
        {
            try
            {
                var classDep = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                return classDep;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ClassDepartment>> GetAllClassDepartmentsAsync()
        {
            try
            {
                var classDeps = await _dbMain.ClassDepartments.ToListAsync();
                return classDeps;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<ClassDepartment> ModifyClassDepartmentAsync(long Id,ClassDepartment updatedclassdep)
        {
            try
            {
                updatedclassdep.ID = Id;
                 _dbMain.ClassDepartments.Update(updatedclassdep);
                return updatedclassdep;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        //d)	Kreirati metodu koja ce za odredjeni razred vratiti listu svih studenata koji se nalaze u njemu
        // Ime (ime roditelja) Prezime | Vrsta skole | Razred | 

        public async Task<List<GetStudentDetails>> GetStudentsPerClassDetailsAsync(long id)
        {
            var classExist = await this._dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == id && s.Deleted == 0);
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
            var studentsFromClassList = await this._dbMain.StudentsDetails.Where(s => s.ClassDepartment.ID == id && s.Deleted == 0).ToListAsync();
            var usersList = await this._dbMain.Users.Where(s => s.Deleted == 0 && s.UserType == 2).ToListAsync();
            var schoolsType = await this._dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Deleted == 0 && s.Id == classExist.SchoolListId);
            var query = from students in studentsFromClassList
                        join users in usersList on students.Student.Id equals users.Id
                        select new
                        {
                            Name = users.Name + ' ' + users.LastName,
                            SchoolType = schoolsType.Name,
                            ClassGrade = classGrade 
                        };
            List<GetStudentDetails> studentsList = new List<GetStudentDetails>();
            foreach(var row in query)
            {
                GetStudentDetails student = new GetStudentDetails();
                student.Name = row.Name;
                student.SchoolType = row.SchoolType;
                student.ClassGrade = row.ClassGrade;
                studentsList.Add(student);
            }
            return studentsList;
        }

    }
}
