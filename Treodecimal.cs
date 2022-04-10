using System.Text;

[System.Diagnostics.DebuggerDisplay("{ToString()}")]
public class Treodecimal
{
    // Отдельный класс используется только для упращения некоторых операций с числамив тринадатиричной системе, например, увеличения на единицу.
    // Все остальные операции можно проводить в десятичной системе, т.к. если числа равны в тринадцатиричной, то и в десятичной они тоже равны, и т.д.
    
    // Хотя нужны числа длиной 6, используются числа длиной 7, чтобы не разбирать крайние случаи и переполнение.
    private const int len = 7; 
    private const int b = 13; // основание

    // Цифры в десятичной системе, 0-ой элемент массива - старший разряд, последний элемент - младший разряд. 
    private int[] values; 

    public Treodecimal()
    {
        this.values = new int[len];
    }

    public Treodecimal(string value)
    : this()
    {
        if (value.Length > len)
        {
            throw new OverflowException();
        }

        // Строковое представление числа может быть короче полной длины числа, 
        // нужно заполнять не с 0-ого элемента, смещение.
        int offset = len - value.Length; 

        for (int i = 0; i < value.Length; i++)
        {
            this.values[i + offset] = IntFromChar(value[i]);
        }
    }

    public static implicit operator Treodecimal(string value)
    {
        return new Treodecimal(value);
    }

    public void AddOne()
    {
        int i = len - 1;
        int carry = (values[i] + 1) / b;
        values[i] = (values[i] + 1) % b;

        while (carry > 0)
        {
            i--;

            if (i < 0)
            {
                throw new OverflowException();
            }

            carry = (values[i] + 1) / b;
            values[i] = (values[i] + 1) % b;
        }
    }

    public bool LessOrEqual(Treodecimal t)
    {
        for (int i = 0; i < len; i++)
        {
            if (values[i] > t[i])
            {
                return false;
            }
        }

        return true;
    }

    public int SumNumbersInt()
    {
        int result = 0;
        for (int i = 0; i < len; i++)
        {
            result += this[i];
        }
        return result;
    }

    // С одной стороны, это показывает наружу внутреннее представление.
    // С другой, так, например, быстрее проводить сравнение. 
    public int this[int i]
    {
        get
        {
            return this.values[i];
        }
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < len; i++)
        {
            result.Append(StringFromInt(values[i]));
        }

        result.Append(";");

        for (int i = 0; i < len; i++)
        {
            result.Append($" {values[i]}");
        }

        return result.ToString();
    }

    private static string StringFromInt(int i)
    {
        switch (i)
        {
            case int n when n < 10:
                return n.ToString();
            case 10:
                return "A";
            case 11:
                return "B";
            case 12:
                return "C";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static int IntFromChar(char ch)
    {
        switch (ch)
        {
            case char c when c >= '0' && c <= '9':
                return c - '0';
            case char c when c == 'a' || c == 'A':
                return 10;
            case char c when c == 'b' || c == 'B':
                return 11;
            case char c when c == 'c' || c == 'C':
                return 12;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}