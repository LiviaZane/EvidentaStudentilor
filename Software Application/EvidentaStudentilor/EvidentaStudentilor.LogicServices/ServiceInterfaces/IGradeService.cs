using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IGradeService
    {
        public void createGrade(Grade grade);
        public void updateGrade(Grade grade);
        public void deleteGrade(Grade grade);

        public IEnumerable<Grade> GetGrade();
        public Grade GetGrade(int id);
        public IEnumerable<Grade> GetGradeOfExam(int ex);
        public IEnumerable<Teacher> GetTeacher();
        public IEnumerable<Profile> GetProfile();
        public IEnumerable<Subject> GetSubject();
        public IEnumerable<Student> GetStudent();
    }
}
