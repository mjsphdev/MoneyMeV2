namespace MoneyMe.API.Application.Shared.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Default { get; }

        DateTime ToUtc(DateTime dateTime);

        bool IsDefault(DateTime dateTime);
        bool IsDefault(string dateTime);
    }
}
