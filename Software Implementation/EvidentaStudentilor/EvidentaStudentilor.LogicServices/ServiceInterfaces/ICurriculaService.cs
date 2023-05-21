using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface ICurriculaService
    {
        public void createCurricula(Curricula curricula);
        public void updateCurricula(Curricula curricula);
        public void deleteCurricula(Curricula curricula);

        public IEnumerable<Curricula> GetCurricula();
        public Curricula? GetCurricula(int id);
        public IEnumerable<Profile> GetProfile();
        public IEnumerable<Subject> GetSubject(); 
    }
}
