using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Repositories;

namespace CleanArchitecture.Application.Handlers.CommandHandlers
{
    public class CreateStudentHandler : IHandlerWrapper<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<StudentDto>> Handle(CreateStudentCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _studentRepository.InsertAsync(new Student
            {
                Name = request.Name,
                Age = request.Age
            }, cancellationToken);

            return Response.Ok(new StudentDto
            {
                Age = student.Age,
                Id = student.Id.ToString(),
                Name = student.Name
            });
        }
    }
}