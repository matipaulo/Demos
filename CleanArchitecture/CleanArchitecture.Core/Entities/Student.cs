namespace CleanArchitecture.Core.Entities
{
    public class Student : AuditableEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}