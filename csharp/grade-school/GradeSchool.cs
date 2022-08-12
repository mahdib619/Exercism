using System.Collections.Generic;

public class GradeSchool
{
    private readonly SortedDictionary<int, SortedSet<string>> _students = new();

    public void Add(string student, int grade)
    {
        if (!_students.TryGetValue(grade, out var gradeStudents))
            _students[grade] = gradeStudents = new();

        gradeStudents.Add(student);
    }

    public IEnumerable<string> Roster()
    {
        foreach (var grades in _students.Values)
        {
            foreach (var student in grades)
            {
                yield return student;
            }
        }
    }

    public IEnumerable<string> Grade(int grade) => _students.GetValueOrDefault(grade, new());
}