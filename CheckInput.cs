using System;

namespace Lab4
{
    public static class CheckInput
    {
        public static double ReadDouble(string message)
        {
            double result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (double.TryParse(input, out result) ||
                    double.TryParse(input?.Replace('.', ','), out result))
                {
                    return result;
                }

                Console.WriteLine("Ошибка: Пожалуйста, введите корректное вещественное число.");
            }
        }

        public static int ReadInt(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                Console.WriteLine("Ошибка: Пожалуйста, введите целое число.");
            }
        }

        // Чтение непустой строки
        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Ошибка: Строка не может быть пустой.");
            }
        }
    }
}
