using Lab4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Установка кодировки для корректного отображения русского текста в консоли
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            CreateFiles();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Задание 1 (List): Пересечение списков");
                Console.WriteLine("2. Задание 2 (LinkedList): Палиндром из списка");
                Console.WriteLine("3. Задание 3 (HashSet): ТРЦ и студенты");
                Console.WriteLine("4. Задание 4 (HashSet): Символы в нечетных словах (из файла)");
                Console.WriteLine("5. Задание 5 (Dictionary): АЗС и цены (из файла)");
                Console.WriteLine("6. Задание 6 (Class): Отрезок (проверка пересечения)");
                Console.WriteLine("7. Задание 7 (Operators): Перегрузка операций для Отрезка");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CollectionTasks.Task1();
                        break;
                    case "2":
                        CollectionTasks.Task2();
                        break;
                    case "3":
                        CollectionTasks.Task3();
                        break;
                    case "4":
                        CollectionTasks.Task4("Task4.txt");
                        break;
                    case "5":
                        CollectionTasks.Task5("Task5.txt");
                        break;
                    case "6":
                        Task6();
                        break;
                    case "7":
                        Task7();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите Enter.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }

        // Задание 6
        static void Task6()
        {
            Console.WriteLine("\n--- Тест Задания 6 (Класс LineSegment) ---");

            // Ввод первого отрезка через класс CheckInput
            double x1 = CheckInput.ReadDouble("Введите X для отрезка 1: ");
            double y1 = CheckInput.ReadDouble("Введите Y для отрезка 1: ");
            LineSegment seg1 = new LineSegment(x1, y1);

            // Ввод второго отрезка
            double x2 = CheckInput.ReadDouble("Введите X для отрезка 2: ");
            double y2 = CheckInput.ReadDouble("Введите Y для отрезка 2: ");
            LineSegment seg2 = new LineSegment(x2, y2);

            Console.WriteLine($"\nОтрезок 1: {seg1}");
            Console.WriteLine($"Отрезок 2: {seg2}");

            bool intersect = seg1.IsIntersect(seg2);
            Console.WriteLine($"Пересекаются ли отрезки? {(intersect ? "Да" : "Нет")}");
        }

        // Задание 7
        static void Task7()
        {
            Console.WriteLine("\n--- Тест Задания 7 (Перегрузка операторов) ---");

            LineSegment seg = new LineSegment(2, 5);
            Console.WriteLine($"Исходный отрезок: {seg}");

            // 1. Унарный ! (длина)
            Console.WriteLine($"1. Длина (!seg): {!seg}");

            // 2. Унарный ++ (расширение)
            seg++;
            Console.WriteLine($"2. После ++ (расширение на 1): {seg}");

            // 3. Неявное приведение к int (X)
            int xVal = seg;
            Console.WriteLine($"3. Неявное int (координата X): {xVal}");

            // 4. Явное приведение к double (Y)
            double yVal = (double)seg;
            Console.WriteLine($"4. Явное double (координата Y): {yVal}");

            // 5. Бинарный - => уменьшает X
            Console.WriteLine("5. seg - 2 (уменьшение X):");
            LineSegment segMinusLeft = seg - 2;
            Console.WriteLine($"   Результат: {segMinusLeft}");

            // 6. Бинарный - => уменьшает Y
            Console.WriteLine("6. 3 - seg (уменьшение Y):");
            LineSegment segMinusRight = 3 - seg; 
            Console.WriteLine($"   Результат: {segMinusRight}");

            // 7. Бинарный < 
            LineSegment testIntersect = new LineSegment(4, 8);
            Console.WriteLine($"\nПроверка пересечения (<) с отрезком {testIntersect}:");
            bool isInteresectOp = seg < testIntersect;
            Console.WriteLine($"   Пересекаются (<)? {(isInteresectOp ? "Да" : "Нет")}");
        }

        static void CreateFiles()
        {
            if (!File.Exists("Task4.txt"))
            {
                File.WriteAllText("Task4.txt", "Текст для проверки в данном тексте проверяются нечетные слова.", Encoding.UTF8);
            }

            if (!File.Exists("Task5.txt"))
            {
                // Формат: N строк, <Фирма> <Улица> <Марка> <Цена>
                string content = "5\n" +
                                 "Лукойл Ленина 92 4500\n" +
                                 "Газпром Мира 95 5200\n" +
                                 "Роснефть Садовая 92 4450\n" + 
                                 "Шелл Победы 98 6000\n" +
                                 "Татнефть Жукова 95 5200\n"; 
                File.WriteAllText("Task5.txt", content, Encoding.UTF8);
            }
        }
    }
}
