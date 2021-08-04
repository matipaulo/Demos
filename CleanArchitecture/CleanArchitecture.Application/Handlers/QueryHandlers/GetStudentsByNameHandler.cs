using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Queries;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Core.Repositories;

namespace CleanArchitecture.Application.Handlers.QueryHandlers
{
    public class GetStudentsByNameHandler : IHandlerWrapper<GetStudentsByNameQuery, ICollection<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsByNameHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<ICollection<StudentDto>>> Handle(GetStudentsByNameQuery request,
            CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetByName(request.Name, cancellationToken);
            if (students == null || students.Count == 0)
                return Response.Ok<ICollection<StudentDto>>(Array.Empty<StudentDto>());

            var response = new List<StudentDto>(students.Count);
            response.AddRange(students.Select(student => new StudentDto
            { Id = student.Id.ToString(), Age = student.Age, Name = student.Name }));

            return Response.Ok<ICollection<StudentDto>>(response);
        }
    }
}