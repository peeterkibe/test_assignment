using System.Globalization;

public class Utility 
{
    // Method to read and format the data from provided txt file and return it formated
    public static List<BreakPeriod> ReadBreakPeriodsFromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found at path: {path}");
        }

        var lines = File.ReadAllLines(path);
        var breakPeriods = new List<BreakPeriod>();

        foreach (string line in lines)
        {
            var times = line.Split('-');

            if (times.Length != 2 
                || !TryParseTimeOnly(times[0], out TimeOnly startTime) 
                || !TryParseTimeOnly(times[1], out TimeOnly endTime))
            {
                Console.WriteLine($"Warning: Skipping invalid line: {line}");
                continue;
            }

            breakPeriods.Add(new BreakPeriod(startTime, endTime));
        }

        return breakPeriods;
    }

    // Method to validate input and format valid value
    public static bool ProcessInput(string inputValue, out TimeOnly startTime, out TimeOnly endTime)
    {
        if (inputValue.Length != 10)
        {
            startTime = default;
            endTime = default;
            return false;
        }

        string start = inputValue[..5];
        string end = inputValue[5..];

        return TryParseTimeOnly(start, out startTime) & TryParseTimeOnly(end, out endTime);
    }

    private static bool TryParseTimeOnly(string timeString, out TimeOnly result)
    {
        return TimeOnly.TryParse(timeString.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }
}