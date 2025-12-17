internal class Program
{
    private static readonly CheckInput _check = new CheckInput();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n--- Лабораторная работа 4 (Вариант 11) ---");
            Console.WriteLine("1. Задание 1 (List)");
            Console.WriteLine("2. Задание 2 (LinkedList)");
            Console.WriteLine("3. Задание 3 (HashSet - ТРЦ)");
            Console.WriteLine("4. Задание 4 (HashSet - Текст)");
            Console.WriteLine("5. Задание 5 (Dictionary - Файл)");
            Console.WriteLine("6. Задания 6 и 7 (Класс LineSegment)");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Lab4Tasks.Task1();
                    break;
                case "2":
                    Lab4Tasks.Task2();
                    break;
                case "3":
                    Lab4Tasks.Task3();
                    break;
                case "4":
                    Lab4Tasks.Task4();
                    break;
                case "5":
                    Lab4Tasks.Task5();
                    break;
                case "6":
                    Task6And7();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    // Демонстрация задач 6 и 7
    private static void Task6And7()
    {
        Console.WriteLine("\n--- Демонстрация LineSegment (Задания 6-7) ---");

        double x = _check.ReadDouble("Введите X начала отрезка 1: ");
        double y = _check.ReadDouble("Введите Y конца отрезка 1: ");
        LineSegment seg1 = new LineSegment(x, y);

        Console.WriteLine($"Отрезок 1 создан: {seg1}");

        // Тест длины (!)
        Console.WriteLine($"Длина отрезка 1 (!seg1): {!seg1}");

        // Тест расширения (++)
        seg1++;
        Console.WriteLine($"Отрезок 1 после расширения (seg1++): {seg1}");

        // Тест приведения типов
        int xInt = seg1; // неявное
        double yDouble = (double)seg1; // явное
        Console.WriteLine($"Неявное приведение к int (X): {xInt}");
        Console.WriteLine($"Явное приведение к double (Y): {yDouble}");

        Console.WriteLine("\nСоздание второго отрезка для проверки пересечения.");
        double x2 = _check.ReadDouble("Введите X начала отрезка 2: ");
        double y2 = _check.ReadDouble("Введите Y конца отрезка 2: ");
        LineSegment seg2 = new LineSegment(x2, y2);
        Console.WriteLine($"Отрезок 2: {seg2}");

        // Тест пересечения 
        bool intersectMethod = seg1.Intersects(seg2);
        Console.WriteLine($"Пересечение (метод Intersects): {intersectMethod}");

        
        bool intersectOp = seg1 < seg2;
        Console.WriteLine($"Пересечение (оператор <): {intersectOp}");

        // Тест бинарных минуса('-')
        Console.WriteLine("\nПроверка бинарных минусов:");
        int val = _check.ReadInt("Введите целое число N: ");

        LineSegment leftOp = seg1 - val;
        Console.WriteLine($"seg1 - {val} (уменьшение X): {leftOp}");

        LineSegment rightOp = val - seg1;
        Console.WriteLine($"{val} - seg1 (уменьшение Y): {rightOp}");
    }
}
