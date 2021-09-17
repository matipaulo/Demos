using CQRS.Application.Models;
using CQRS.Application.Wrappers;

namespace CQRS.Application.Commands
{
    public class CreateStudentCommand : ITransactional, IRequestWrapper<StudentDto>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}