using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class GradeService : IGradeService
    {

        private readonly IWrapperRepository _wrapperRepository;

        public GradeService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createGrade(Grade grade)
        {
            _wrapperRepository.GradeRepository.Create(grade);
            _wrapperRepository.Save();
        }

        public void updateGrade(Grade grade)
        {
            _wrapperRepository.GradeRepository.Update(grade);
            _wrapperRepository.Save();
        }

        public void deleteGrade(Grade grade)
        {
            _wrapperRepository.GradeRepository.Delete(grade);
            _wrapperRepository.Save();
        }

        public IEnumerable<Grade> GetGrade()
        {
            return _wrapperRepository.GradeRepository.FindAll().ToList();
        }


        public Grade GetGrade(int id)
        {
            return _wrapperRepository.GradeRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }


        public IEnumerable<Grade> GetGradeOfExam(int ex)
        {
            return _wrapperRepository.GradeRepository.FindAll().Where(x => x.ExamId == ex);
        }

        public IEnumerable<Teacher> GetTeacher()
        {
            return _wrapperRepository.TeacherRepository.FindAll().ToList();
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll().ToList();
        }

        public IEnumerable<Subject> GetSubject()
        {
            return _wrapperRepository.SubjectRepository.FindAll().ToList();
        }

        public IEnumerable<Student> GetStudent()
        {
            return _wrapperRepository.StudentRepository.FindAll().ToList();
        }
    }
}
