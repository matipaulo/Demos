using CQRS.Application.Commands;
using CQRS.Application.Models;
using CQRS.Application.Wrappers;
using CQRS.Core.Entities;
using CQRS.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Handlers.CommandHandlers
{
    public class CreateStudentHandler : IHandlerWrapper<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <inheritdoc />
        public async Task<Response<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
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