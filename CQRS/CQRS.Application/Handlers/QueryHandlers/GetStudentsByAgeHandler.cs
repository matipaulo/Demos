using CQRS.Application.Models;
using CQRS.Application.Queries;
using CQRS.Application.Wrappers;
using CQRS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Handlers.QueryHandlers
{
    public class GetStudentsByAgeHandler : IHandlerWrapper<GetStudentsByAgeQuery, ICollection<StudentDto>>
    {
        private readonly SchoolContext _schoolContext;

        public GetStudentsByAgeHandler(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        /// <inheritdoc />
        public async Task<Response<ICollection<StudentDto>>> Handle(GetStudentsByAgeQuery request, CancellationToken cancellationToken)
        {
            var students = await _schoolContext.Students.Where(x => x.Age == request.Age).ToListAsync(cancellationToken);
            if (students == null || students.Count == 0)
                return Response.Ok<ICollection<StudentDto>>(Array.Empty<StudentDto>());

            var response = new List<StudentDto>(students.Count);
            response.AddRange(students.Select(student => new StudentDto
            { Id = student.Id.ToString(), Age = student.Age, Name = student.Name }));

            return Response.Ok<ICollection<StudentDto>>(response);
        }
    }
}