using CQRS.Application.Commands;
using CQRS.Application.Models;
using CQRS.Application.Wrappers;
using CQRS.Core.Data;
using CQRS.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Handlers.CommandHandlers
{
    public class CreateStudentHandler : IHandlerWrapper<CreateStudentCommand, StudentDto>
    {
        private readonly SchoolContext _schoolContext;

        public CreateStudentHandler(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        /// <inheritdoc />
        public async Task<Response<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = (await _schoolContext.Students.AddAsync(new Student
            {
                Name = request.Name,
                Age = request.Age
            }, cancellationToken)).Entity;

            await _schoolContext.SaveChangesAsync(cancellationToken);

            return Response.Ok(new StudentDto
            {
                Age = student.Age,
                Id = student.Id.ToString(),
                Name = student.Name
            });
        }
    }
}