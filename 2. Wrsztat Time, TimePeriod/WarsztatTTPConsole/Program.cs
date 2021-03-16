using System;
using WarsztatTTPLib;

namespace WarsztatTTPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Time time1 = new Time();
            Time time2 = new Time(60);
            Time time3 = new Time(hours: 2);
            Time time4 = new Time(2, 30);
            Time time5 = new Time(2, 30, 00);
            Time time6 = new Time("03:30:45");

            TimePeriod timePeriod1 = new TimePeriod(); 
            TimePeriod timePeriod2 = new TimePeriod((long)120); 
            TimePeriod timePeriod3 = new TimePeriod(hours: 30); 
            TimePeriod timePeriod4 = new TimePeriod(30, 30); 
            TimePeriod timePeriod5 = new TimePeriod(30, 30, 45); 
            TimePeriod timePeriod6 = new TimePeriod("45:00:10");

            Console.WriteLine($"Time: {time1} + Time: {time2} = Time: {time1 + time2}");
            Console.WriteLine($"Time: {time3} + Time: {time4} = Time: {time3 + time4}");
            Console.WriteLine($"Time: {time5} + Time: {time6} = Time: {time5 + time6}");

            Console.WriteLine();

            Console.WriteLine($"Time: {time6} - Time: {time5} = Time: {time6 - time5}");
            Console.WriteLine($"Time: {time4} - Time: {time3} = Time: {time4 - time3}");
            Console.WriteLine($"Time: {time2} - Time: {time1} = Time: {time2 - time1}");

            Console.WriteLine();

            Console.WriteLine($"Time: {time1} * 2 = Time: {time1 * 2}");
            Console.WriteLine($"Time: {time3} * 4 = Time: {time3 * 4}");
            Console.WriteLine($"Time: {time5} * 6 = Time: {time5 * 6}");

            Console.WriteLine();

            Console.WriteLine($"Time: {time6} / 5 = Time: {time6 / 5}");
            Console.WriteLine($"Time: {time4} / 3 = Time: {time4 / 3}");
            Console.WriteLine($"Time: {time2} / 1 = Time: {time2 / 1}");

            Console.WriteLine();

            Console.WriteLine($"TimePeriod: {timePeriod1} + TimePeriod: {timePeriod2} = TimePeriod: {timePeriod1 + timePeriod2}");
            Console.WriteLine($"TimePeriod: {timePeriod3} + TimePeriod: {timePeriod4} = TimePeriod: {timePeriod3 + timePeriod4}");
            Console.WriteLine($"TimePeriod: {timePeriod5} + TimePeriod: {timePeriod6} = TimePeriod: {timePeriod5 + timePeriod6}");

            Console.WriteLine();

            Console.WriteLine($"TimePeriod: {timePeriod6} - TimePeriod: {timePeriod5} = TimePeriod: {timePeriod6 - timePeriod5}");
            Console.WriteLine($"TimePeriod: {timePeriod4} - TimePeriod: {timePeriod3} = TimePeriod: {timePeriod4 - timePeriod3}");
            Console.WriteLine($"TimePeriod: {timePeriod2} - TimePeriod: {timePeriod1} = TimePeriod: {timePeriod2 - timePeriod1}");

            Console.WriteLine();

            Console.WriteLine($"TimePeriod: {timePeriod1} * 2 = TimePeriod: {timePeriod1 * 2}");
            Console.WriteLine($"TimePeriod: {timePeriod3} * 4 = TimePeriod: {timePeriod3 * 4}");
            Console.WriteLine($"TimePeriod: {timePeriod5} * 6 = TimePeriod: {timePeriod5 * 6}");

            Console.WriteLine();

            Console.WriteLine($"TimePeriod: {timePeriod6} / 5 = TimePeriod: {timePeriod6 / 5}");
            Console.WriteLine($"TimePeriod: {timePeriod4} / 3 = TimePeriod: {timePeriod4 / 3}");
            Console.WriteLine($"TimePeriod: {timePeriod2} / 1 = TimePeriod: {timePeriod2 / 1}");

            Console.WriteLine();

            Console.WriteLine($"Time: {time1} + TimePeriod: {timePeriod2} = TimePeriod: {time1 + timePeriod2}");
            Console.WriteLine($"Time: {time3} + TimePeriod: {timePeriod4} = TimePeriod: {time3 + timePeriod4}");
            Console.WriteLine($"Time: {time5} + TimePeriod: {timePeriod6} = TimePeriod: {time5 + timePeriod6}");

            Console.WriteLine();

            Console.WriteLine($"Time: {time6} - TimePeriod: {timePeriod5} = TimePeriod: {time6 - timePeriod5}");
            Console.WriteLine($"Time: {time4} - TimePeriod: {timePeriod3} = TimePeriod: {time4 - timePeriod3}");
            Console.WriteLine($"Time: {time2} - TimePeriod: {timePeriod1} = TimePeriod: {time2 - timePeriod1}");

            Console.WriteLine();

            if(time2 < time3)
                Console.WriteLine($"Time: {time2} < Time: {time3}");
            
            if(time3 > time2)
                Console.WriteLine($"Time: {time3} > Time: {time2}");

            if (time2 <= time3)
                Console.WriteLine($"Time: {time2} <= Time: {time3}");

            if (time3 >= time2)
                Console.WriteLine($"Time: {time3} >= Time: {time2}");

            Console.WriteLine();

            if (timePeriod2 < timePeriod3)
                Console.WriteLine($"TimePeriod: {timePeriod2} < TimePeriod: {timePeriod3}");

            if (timePeriod3 > timePeriod2)
                Console.WriteLine($"TimePeriod: {timePeriod3} > TimePeriod: {timePeriod2}");

            if (timePeriod2 <= timePeriod3)
                Console.WriteLine($"TimePeriod: {timePeriod2} <= TimePeriod: {timePeriod3}");

            if (timePeriod3 >= timePeriod2)
                Console.WriteLine($"TimePeriod: {timePeriod3} >= TimePeriod: {timePeriod2}");

            Console.ReadKey();
        }
    }
}
