using System.Collections.Generic;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Wrappers;

namespace CleanArchitecture.Application.Queries
{
    public class GetStudentsByNameQuery : IRequestWrapper<ICollection<StudentDto>>
    {
        public string Name { get; set; }
    }
}