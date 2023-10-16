public class PeriodFinder
{
    public static void FindBusiestPeriod(List<BreakPeriod> breakPeriods)
    {
        
        var (start, end) = GetBusiestPeriod(breakPeriods, out int overlappingBreaksCount);

        Console.WriteLine($"The busiest time is {start} to {end} with a total of {overlappingBreaksCount} drivers taking a break.");
    }

    private static (TimeOnly start, TimeOnly end) GetBusiestPeriod(List<BreakPeriod> breakPeriods, out int count)
    {
        // Sorted list to escape early from the inner for loop 
        breakPeriods.Sort((x,y) => x.StartTime == y.StartTime ? x.EndTime.CompareTo(y.EndTime) : x.StartTime.CompareTo(y.StartTime));

        TimeOnly busiestStart = new();
        TimeOnly busiestEnd = new();
        count = 0;

        // Check each period how many brakes overlapped with the given period 
        for (int i = 0; i < breakPeriods.Count; i++)
        {
            TimeOnly tempBusiestStart = breakPeriods[i].StartTime;
            TimeOnly tempBusiestEnd = breakPeriods[i].EndTime;
            int tempCount = 1;

            for (int j = 0; j < breakPeriods.Count; j++)
            {
                if ( i == j ){
                    continue;
                }

                if(breakPeriods[j].StartTime > tempBusiestEnd)
                {
                    break;
                }


                bool isOverlapping = breakPeriods[j].StartTime <= tempBusiestEnd && tempBusiestStart <= breakPeriods[j].EndTime;


                if (isOverlapping)
                {
                    tempBusiestStart = tempBusiestStart > breakPeriods[j].StartTime ? tempBusiestStart : breakPeriods[j].StartTime;
                    tempBusiestEnd = tempBusiestEnd < breakPeriods[j].EndTime ? tempBusiestEnd : breakPeriods[j].EndTime;
                    tempCount++;
                }
            }

            if (count < tempCount)
            {
                busiestStart = tempBusiestStart;
                busiestEnd = tempBusiestEnd;
                count = tempCount;
            }
        }

        return (busiestStart, busiestEnd);
    }
}