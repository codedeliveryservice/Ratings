using System.Runtime.Serialization;

namespace Web.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string name, object key) : base($"Entity '{name}' ('{key}') has not been found.") { }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
