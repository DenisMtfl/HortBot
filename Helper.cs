namespace HortBot
{
    public static class Helper
    {
        public static DateTime OnlyDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }

    }
}