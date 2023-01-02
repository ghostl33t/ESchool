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
        
        public ClassDepartmentRepository(DBMain dbMain)
        {
            this._dbMain = dbMain;
        }
        public async Task<long> CreateClassDepartmentAsync(ClassDepartment newclassdepp)
        {
            try
            {
                newclassdepp.CreatedBy = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newclassdepp.CreatedById_);
                newclassdepp.LeaderProfessor = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newclassdepp.LeaderProfessorId_);
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
                updatedclassdep.LeaderProfessor = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == updatedclassdep.LeaderProfessorId_);
                 _dbMain.ClassDepartments.Update(updatedclassdep);
                return updatedclassdep;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task<List<GetStudentDetails>> GetStudentsPerClassDetailsAsync(long id)
        {
            try
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
                        break;
                }
                var query = from students in _dbMain.StudentsDetails
                            join users in _dbMain.Users on students.Student.Id equals users.Id
                            join classDep in _dbMain.ClassDepartments on students.ClassDepartment.ID equals classDep.ID
                            join schoolList in _dbMain.SchoolList on classDep.SchoolListId equals schoolList.Id
                            where students.ClassDepartment.ID == id
                            select new
                            {
                                Name = users.Name + ' ' + users.LastName,
                                SchoolType = schoolList.Name + " - " + classDep.SerialNumber + " - " + classDep.Name,
                                ClassGrade = classGrade
                            };
                List<GetStudentDetails> studentsList = new List<GetStudentDetails>();
                foreach (var row in query)
                {
                    GetStudentDetails student = new GetStudentDetails();
                    student.Name = row.Name;
                    student.SchoolType = row.SchoolType;
                    student.ClassGrade = row.ClassGrade;
                    studentsList.Add(student);
                }
                return studentsList;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

    }
}
