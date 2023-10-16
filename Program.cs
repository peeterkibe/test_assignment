using System;

public class Program 
{
    public static void Main(string[] args) 
    {
        string filePath = string.Empty;

        for(int i = 0; i < args.Length; i++)
        {
            if(args[i] == "filename" && i + 1 < args.Length )
            {
                filePath = args[i + 1];
                break;
            }
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("File path not provided. Try again and add filepath after 'filename' tag.");
            return;
        }

        List<BreakPeriod> times = Utility.ReadBreakPeriodsFromFile(filePath);

        while(true)
        {
            PeriodFinder.FindBusiestPeriod(times);
            Console.Write("Insert new Time: ");
            string input = Console.ReadLine()!;

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Thank you for using our services. Safe planning!");
                return;
            }

            TimeOnly start;
            TimeOnly end;

            while (!Utility.ProcessInput(input, out start, out end))
            {
                if (input.ToLower() == "exit")
                {
                Console.WriteLine("Thank you for using our services. Safe planning!");

                    return;
                }

                Console.WriteLine($"Warning: Given input is invalid: {input}");
                Console.Write("Insert valid time (<start time><end time>): ");
                input = Console.ReadLine()!;
            }
            
            times.Add(new BreakPeriod(start, end));
        }
    }
}
