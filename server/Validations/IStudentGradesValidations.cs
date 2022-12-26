namespace server.Validations
{
    public interface IStudentGradesValidations
    {
        public bool validationResult { get; set; }
        public Task<bool> StudentExist(long studentId);
        public Task<bool> StudentSubject(long studentid, long subjectId);
        public Task<bool> ValidateProfessor(long studentid, long professorId);
        public bool CheckIfGradeIsValidated(int validatedGrade);
        public Task<string> Validations(server.Models.DTOs.StudentGrades.PostStudentGrades create);

    }
}
