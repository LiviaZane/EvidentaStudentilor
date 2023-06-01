namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IWrapperRepository
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ICurriculaRepository CurriculaRepository { get; }
        IExamRepository ExamRepository { get; }
        IGradeRepository GradeRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        IStudentRepository StudentRepository { get; }
        IProfileRepository ProfileRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        void Save();

    }
}
