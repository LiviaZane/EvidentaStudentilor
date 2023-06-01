using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private EvidentaStudentilorContext _context;
        private IRoleRepository? _roleRepository;
        private IUserRepository? _userRepository;
        private IDepartmentRepository? _departmentRepository;
        private ICurriculaRepository? _curriculaRepository;
        private IExamRepository? _examRepository;
        private IGradeRepository? _gradeRepository;
        private IScheduleRepository? _scheduleRepository;
        private IStudentRepository? _studentRepository;
        private ISubjectRepository? _subjectRepository;
        private ITeacherRepository? _teacherRepository;
        private IProfileRepository? _profileRepository;

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_context);
                }
                return _roleRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_context);
                }
                return _departmentRepository;
            }
        }
        public ICurriculaRepository CurriculaRepository
        {
            get
            {
                if (_curriculaRepository == null)
                {
                    _curriculaRepository = new CurriculaRepository(_context);
                }
                return _curriculaRepository;
            }
        }

        public IExamRepository ExamRepository
        {
            get
            {
                if (_examRepository == null)
                {
                    _examRepository = new ExamRepository(_context);
                }
                return _examRepository;
            }
        }

        public IGradeRepository GradeRepository
        {
            get
            {
                if (_gradeRepository == null)
                {
                    _gradeRepository = new GradeRepository(_context);
                }
                return _gradeRepository;
            }
        }
        public IScheduleRepository ScheduleRepository
        {
            get
            {
                if (_scheduleRepository == null)
                {
                    _scheduleRepository = new ScheduleRepository(_context);
                }
                return _scheduleRepository;
            }
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_context);
                }
                return _studentRepository;
            }
        }


        public ISubjectRepository SubjectRepository
        {
            get
            {
                if (_subjectRepository == null)
                {
                    _subjectRepository = new SubjectRepository(_context);
                }
                return _subjectRepository;
            }
        }

        public IProfileRepository ProfileRepository
        {
            get
            {
                if (_profileRepository == null)
                {
                    _profileRepository = new ProfileRepository(_context);
                }
                return _profileRepository;
            }
        }

        public ITeacherRepository TeacherRepository
        {
            get
            {
                if (_teacherRepository == null)
                {
                    _teacherRepository = new TeacherRepository(_context);
                }
                return _teacherRepository;
            }
        }


        public WrapperRepository(EvidentaStudentilorContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
