using Courseproject.Common.Model;
using System.Runtime.Serialization;

namespace Courseproject.Business.Exceptions;

[Serializable]
public class DependendEmployeeExistException : Exception
{
    public List<Employee> Employees { get; }

    public DependendEmployeeExistException()
    {
    }

    public DependendEmployeeExistException(List<Employee> employees)
    {
        this.Employees = employees;
    }

    public DependendEmployeeExistException(string? message) : base(message)
    {
    }

    public DependendEmployeeExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependendEmployeeExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}