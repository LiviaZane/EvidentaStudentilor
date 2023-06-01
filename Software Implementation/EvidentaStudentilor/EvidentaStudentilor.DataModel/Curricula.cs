namespace EvidentaStudentilor.DataModel
{
    public class Curricula
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int SubjectId { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public int YearIn { get; set; }
        public int YearOut { get; set; }


        //navigation property, for foreign key
        public virtual Profile? Profile { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
