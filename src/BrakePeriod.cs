public class BreakPeriod
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public BreakPeriod(TimeOnly start, TimeOnly end)
    {
        StartTime = start;
        EndTime = end;
    }
}