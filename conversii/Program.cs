namespace conversii;

class Program
{

    static void Main(string[] args)
    {

        int b1, b2;
        string num;
        (b1, b2, num) = GetInput();

        Console.WriteLine(ConvertToDesiredBase(ConvertToBase10(num, b1), b2));
        Console.ReadLine();

    }

    static (int b1, int b2, string num) GetInput()
    {
        Console.Write("b1: ");
        int b1 = int.Parse(Console.ReadLine());

        Console.Write("b2: ");
        int b2 = int.Parse(Console.ReadLine());

        if (b1 < 2 || b1 > 16 || b2 < 2 || b2 > 16)
            Exit("bazele trebuie sa fie intre 2 si 16");

        Console.Write("num: ");
        string num = Console.ReadLine();

        return (b1, b2, num);
    }

    static decimal ConvertToBase10(string num, int b1)
    {
        string coresp = "0123456789ABCDEF";

        int intreg = 0;
        decimal fr = 0;

        int dotind = num.IndexOf('.');

        if (dotind == -1)
            dotind = num.Length;


        for (int i = 0; i < dotind; i++)
        {

            char digit = num[i];
            int d = coresp.IndexOf(digit, 0, b1);

            if (d == -1)           
                Exit("numarul nu este in baza b1");

            intreg = intreg * b1 + d;

        }
        
        for (int j = num.Length - 1; j >= dotind + 1; j--)
        {
            char digit = num[j];
            int d = coresp.IndexOf(digit, 0, b1);

            if (d == -1)
                Exit("numarul nu este in baza b1");

            fr = fr / b1 + d;
            
        }

        fr /=  b1;
        
        return intreg + fr;

    }


    static string ConvertToDesiredBase(decimal num, int b2)
    {

        int intreg = Convert.ToInt32(Math.Floor(num));
        decimal frac = num - intreg;

        string coresp = "0123456789ABCDEF";
        string result = $"{coresp[intreg % b2]}";

        while (intreg / b2 != 0)
        {
            intreg /= b2;
            int abc = intreg % b2;
            result = $"{coresp[abc]}" + result;
        }

        result += '.';
        for (int i = 0; i < 20; i++)
        {
            frac *= b2;
            int pint = Convert.ToInt32(Math.Floor(frac));
            result += coresp[pint];
            frac -= pint;
            if (frac == 0)
                break;

        }

        return result;
    }

    static void Exit(string message)
    {
        Console.WriteLine(message);
        Console.ReadLine();
        Environment.Exit(0);
    }

}

