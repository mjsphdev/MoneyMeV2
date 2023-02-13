namespace MoneyMe.API.Application.Shared.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public List<string> MessageList { get; set; } = new List<string>();

        public InvalidParameterException(List<string> messageList, string message = "One or more parameter/s is invalid.")
            : base(message)
        {
            MessageList = messageList;
        }

        public InvalidParameterException(string errorMessage, string message = "One or more parameter/s is invalid.")
           : base(message)
        {
            MessageList = new List<string>();
            MessageList.Add(errorMessage);
        }
    }
}
