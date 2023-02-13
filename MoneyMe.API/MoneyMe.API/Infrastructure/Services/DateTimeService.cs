using MoneyMe.API.Application.Shared.Interfaces;

namespace MoneyMe.API.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime Default => Convert.ToDateTime("01/01/1900");

        public bool IsDefault(DateTime dateTime)
        {
            return dateTime == Default;
        }

        public bool IsDefault(string dateTime)
        {
            return Convert.ToDateTime(dateTime) == Default;
        }

        public DateTime ToUtc(DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }
    }
}
