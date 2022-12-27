﻿namespace server.Models.DTOs.StudentGrades
{
    public class PatchStudentGrades
    {
        public long Id { get; set; }
        public server.Models.Domain.User? Student { get; set; }
        public long StudentId { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; } = String.Empty;
        public long CDSPID { get; set; }
        public server.Models.Domain.ClassDepartmentSubjectProfessor? ClassDepartmentSubjectProfessor { get; set; }
    }
}
