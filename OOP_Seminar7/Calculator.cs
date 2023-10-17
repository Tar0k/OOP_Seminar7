using NLog;

namespace OOP_Seminar7;

public class Calculator
{
    
    public static void Start()
    {
        var resume = true;
        while (resume)
        {
            resume = Cycle();
        }
    }
    
    
    private static bool Cycle()
    {
        var input = RequestInput();
        DoOperation(input.Item1, input.Item2, input.Item3);
        
        Console.Write("Выход? Yes : ");
        var decision = Console.ReadLine();
        return decision?.ToLower() != "yes";
    }

    private static (ComplexNumber, ComplexNumber, string) RequestInput()
    {
        ComplexNumber? firstNumber = null;
        while (firstNumber == null)
        {
            Console.Write("Введите первое число: ");
            var firstNumberText = Console.ReadLine();
            if(firstNumberText != null && ComplexNumber.TryParse(firstNumberText, out firstNumber));
        }
        
        ComplexNumber? secondNumber = null;
        while (secondNumber == null)
        {
            Console.Write("Введите второе число: ");
            var secondNumberText = Console.ReadLine();
            if(secondNumberText != null && ComplexNumber.TryParse(secondNumberText, out secondNumber));
        }

        string? op = null;
        while (op == null)
        {
            Console.Write("Введите операцию: ");
            op = Console.ReadLine();
        }
        

        return (firstNumber, secondNumber, op);
    }
    
    private static void DoOperation(ComplexNumber num1, ComplexNumber num2, string operation)
    {
        switch (operation)
        {
            case "+":
                Console.WriteLine(num1 + num2);
                break;
            case "*":
                Console.WriteLine(num1 * num2);
                break;
            case "/":
                Console.WriteLine(num1 / num2);
                break;
            default:
                Console.WriteLine("Данной операции не заложено");
                break;
        }
    }    
}