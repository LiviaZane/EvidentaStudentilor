using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class ExamService : IExamService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public ExamService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createExam(Exam exam)
        {
            _wrapperRepository.ExamRepository.Create(exam);
            _wrapperRepository.Save();
        }

        public void updateExam(Exam exam)
        {
            _wrapperRepository.ExamRepository.Update(exam);
            _wrapperRepository.Save();
        }

        public void deleteExam(Exam exam)
        {
            _wrapperRepository.ExamRepository.Delete(exam);
            _wrapperRepository.Save();
        }

        public IEnumerable<Exam> GetExam()
        {
            return _wrapperRepository.ExamRepository.FindAll();
        }

        public IEnumerable<Exam> GetExamByTeacherId(int teachId, bool closed)
        {
            return _wrapperRepository.ExamRepository.FindAll().Where(x => x.TeacherId == teachId && x.Closed == closed);
        }


        public Exam GetExam(int id)
        {
            return _wrapperRepository.ExamRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }


        public Curricula GetCurricula(int id)
        {
            return _wrapperRepository.CurriculaRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Teacher> GetTeacher()
        {
            return _wrapperRepository.TeacherRepository.FindAll().ToList();
        }

        public IEnumerable<Subject> GetSubject()
        {
            return _wrapperRepository.SubjectRepository.FindAll();
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll();
        }

        public IEnumerable<Student> GetStudent()
        {
            return _wrapperRepository.StudentRepository.FindAll();
        }

        public IEnumerable<Grade> GetGrade()
        {
            return _wrapperRepository.GradeRepository.FindAll();
        }
    }
}
