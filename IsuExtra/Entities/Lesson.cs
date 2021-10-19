namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string name, GroupWithLessons group, Lector lector, LessonNumber lessonNumber, Auditorium auditorium)
        {
            Name = name;
            Group = group;
            Lector = lector;
            LessonTime = lessonNumber;
            Auditorium = auditorium;
        }

        public string Name { get; }

        public GroupWithLessons Group { get; }

        public Lector Lector { get; }

        public LessonNumber LessonTime { get; }

        public Auditorium Auditorium { get; }
    }
}