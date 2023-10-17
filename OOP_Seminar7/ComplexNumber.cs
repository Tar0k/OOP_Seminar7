using System.Globalization;
using System.Text.RegularExpressions;

namespace OOP_Seminar7;


public partial class ComplexNumber
{
    private readonly double _realPart;
    private readonly double _imaginePart;

    public ComplexNumber(string complexNumber)
    {
        _realPart = GetPart(complexNumber, RealPartRegex());
        _imaginePart = GetPart(complexNumber, ImaginePartRegex());
    }

    private ComplexNumber(double realPart, double imaginePart)
    {
        _realPart = realPart;
        _imaginePart = imaginePart;
    }


    public static bool TryParse(string complexNumber, out ComplexNumber? result)
    {
        try
        {
            result = new ComplexNumber(complexNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = null;
            return false;
        }
        return true;
    }
    

    private static double GetPart(string complexNumber, Regex regex)
    {
        var res = regex.Match(complexNumber.Replace(" ", ""));
        if (res.Length == 0)
        {
            throw new ArgumentException($"В числе \"{complexNumber}\" не выявлена часть");
        }
        return double.TryParse(res.Value, out var result) ? result : throw new InvalidCastException("Не удалось конверитровать часть");
    }

    public override string ToString()
    {
        var realPartText = _realPart > 0 ? _realPart.ToString(CultureInfo.InvariantCulture) : $"- {Math.Abs(_realPart)}";
        var imaginePartText =
            _imaginePart > 0 ? $"+ {_imaginePart}i" : $"- {Math.Abs(_imaginePart)}i";
        return $"{realPartText} {imaginePartText}";
    }
    
    public static ComplexNumber operator + (ComplexNumber num1, ComplexNumber num2)
    {
        return new ComplexNumber(
            realPart: num1._realPart + num2._realPart,
            imaginePart: num1._imaginePart + num2._imaginePart);
    }
     
    public static ComplexNumber operator * (ComplexNumber num1, ComplexNumber num2)
    {
        var realPart = num1._realPart * num2._realPart - num1._imaginePart * num2._imaginePart;
        var imaginePart = num1._realPart * num2._imaginePart + num1._imaginePart * num2._realPart;
        return new ComplexNumber(
            realPart: realPart,
            imaginePart: imaginePart);
    }
    
    
    public static ComplexNumber operator / (ComplexNumber num1, ComplexNumber num2)
    {
        var realPart = (num1._realPart * num2._realPart + num1._imaginePart * num2._imaginePart) /
                       (Math.Pow(num2._realPart, 2) + Math.Pow(num2._imaginePart, 2));
        var imaginePart = (num2._realPart * num1._imaginePart - num1._realPart * num2._imaginePart) /
            (Math.Pow(num2._realPart, 2) + Math.Pow(num2._imaginePart, 2));
        return new ComplexNumber(
            realPart: realPart,
            imaginePart: imaginePart);
    }

    [GeneratedRegex(@"[+-]?(\d+?)\b")]
    private static partial Regex RealPartRegex();
    
    [GeneratedRegex(@"[+-]?(\d+?)(?=i)")]
    private static partial Regex ImaginePartRegex();
}


