using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IExamService
    {
        public void createExam(Exam exam);
        public void updateExam(Exam exam);
        public void deleteExam(Exam exam);

        public IEnumerable<Exam> GetExam();
        public IEnumerable<Exam> GetExamByTeacherId(int teachId, bool closed);
        public Exam GetExam(int id);
        public Curricula GetCurricula(int id);
        public IEnumerable<Teacher> GetTeacher();
        public IEnumerable<Subject> GetSubject();
        public IEnumerable<Profile> GetProfile();
        public IEnumerable<Student> GetStudent();
        public IEnumerable<Grade> GetGrade();
    }
}
