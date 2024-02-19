namespace Aws_Login.Core.ServicesCore
{
    public class Notification<T>
    {
        public T Value { get; init; }
        private List<string> ListErrors { get; init; } = [];
        public IReadOnlyList<string> Notifications => ListErrors;


        public Notification(T value, List<string> errors)
        {
            Value = value;
            ListErrors = errors;
        }
        public Notification(T value)
        {
            Value = value;
        }

        public Notification(List<string> errors)
        {
            ListErrors = errors;
        }
        public Notification(string error)
        {
            ListErrors.Add(error);
        }


    }
}

