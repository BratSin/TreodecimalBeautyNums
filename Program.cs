﻿// Для примера, количество красивых чисел, у которых левая и правая суммы равны 1.
// С лева в таких числах будут стоять:
// 100000
// 010000
// 001000
// 000100
// 000010
// 000001
// С права будут стоять такие же последовательности. 
// Получается 6x6=36 сочетаний.
// В середине может стоять любое число от 0 до C, каждое сочетание может встречаться 13 раз.
// Получается, количество красивых чисел, у которых левая и правая суммы равны 1, равно 6x6x13=468
// Решение: 
// 1. Для 6-и значных чисел в тринадцатеричной системе, для каждого варианта суммы цифр этих чисел, посчитать количество чисел, лающих такую сумму. 
// 2. Для каждого варианта суммы, количество возвести в квадрат, и умножить на 13.
// 3. Сложить получившиеся числа.

// Сумма всех чисел в CCCCCC получается 72 в десятичной системе. Итого 73 варианта сумм.

int[] result = CalcSumsNumbers("000000", "CCCCCC");

long total = 0;

for (int i = 0; i < 73; i++)
{
    total += (long)result[i] * (long)result[i] * 13;
}

Console.WriteLine($"Total: {total}");

static int[] CalcSumsNumbers(Treodecimal from, Treodecimal to)
{
    int[] result = new int[73];

    for (int i = 0; i < 73; i++)
    {
        result[i] = 0;
    }

    do
    {
        result[from.SumNumbersInt()]++;
        from.AddOne();
    }
    while (from.LessOrEqual(to));

    return result;
}