using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface ISubjectService
    {
        public void createSubject(Subject subject);
        public void updateSubject(Subject subject);
        public void deleteSubject(Subject subject);
        public IEnumerable<Subject> GetSubject();
        public Subject GetSubject(int id);
    }
}
