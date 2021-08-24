using CQRS.Application.Models;
using CQRS.Application.Wrappers;
using System.Collections.Generic;

namespace CQRS.Application.Queries
{
    public class GetStudentsByAgeQuery : IRequestWrapper<ICollection<StudentDto>>
    {
        public int Age { get; set; }
    }
}