using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Commands
{
    public class CreateStudentCommand : ITransactional, IRequestWrapper<StudentDto>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}