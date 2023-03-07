namespace MedokStore.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Client \"{name}\" ({key}) not found.") { }
    }
}
