using Practical17.Models;

namespace Practical17.Repository
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();  
        Student Add(Student student);
        Student Update(Student student); 
        Student Delete(int id);
    }
}
