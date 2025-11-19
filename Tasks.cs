using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4
{
    // Класс для решения задач 1-5
    public static class CollectionTasks
    {
        // Задание 1. List
        // Составить программу, которая формирует список L, включив в него по одному разу элементы,
        // которые входят одновременно в оба списка L1 и L2.
        public static void Task1()
        {
            Console.WriteLine("\n--- Задание 1 ---");

            List<int> L1 = new List<int> { 1, 2, 3, 4, 5, 2, 1 };
            List<int> L2 = new List<int> { 3, 4, 5, 6, 7, 3 };

            Console.WriteLine("L1: " + string.Join(", ", L1));
            Console.WriteLine("L2: " + string.Join(", ", L2));

            // Используем Intersect для нахождения пересечения (оставляет уникальные)
            // ToList преобразует результат обратно в List
            List<int> L = L1.Intersect(L2).ToList();

            Console.WriteLine("Результат (пересечение L1 и L2): " + string.Join(", ", L));
        }

        // Задание 2. LinkedList
        // В конец непустого списка добавить все его элементы, располагая их в обратном порядке.
        public static void Task2()
        {
            Console.WriteLine("\n--- Задание 2 ---");

            LinkedList<int> list = new LinkedList<int>(new[] { 1, 2, 3 });
            Console.WriteLine("Исходный список: " + string.Join(", ", list));

            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }


            // Создаем временный список, чтобы не модифицировать коллекцию во время итерации
            List<int> buffer = new List<int>(list);

            // Проходим по буферу с конца
            for (int i = buffer.Count - 1; i >= 0; i--)
            {
                list.AddLast(buffer[i]);
            }

            Console.WriteLine("Результат: " + string.Join(", ", list));
        }

        // Задание 3.
        // Есть перечень ТРЦ. Известно, где был каждый студент.
        // 1. В какие ТРЦ ходили ВСЕ. 2. В какие НЕКОТОРЫЕ. 3. В какие НИКТО.
        public static void Task3()
        {
            Console.WriteLine("\n--- Задание 3 ---");

            // Все возможные ТРЦ
            HashSet<string> allMalls = new HashSet<string> { "Аура", "Галерея", "Планета", "Сибирский Молл" };

            // Данные студентов (Имя -> Набор посещенных ТРЦ)
            Dictionary<string, HashSet<string>> students = new Dictionary<string, HashSet<string>>
            {
                { "Иван", new HashSet<string> { "Аура", "Галерея" } },
                { "Мария", new HashSet<string> { "Аура", "Планета" } },
                { "Петр", new HashSet<string> { "Аура", "Галерея", "Планета" } }
            };

            Console.WriteLine("Все ТРЦ: " + string.Join(", ", allMalls));
            foreach (var s in students)
            {
                Console.WriteLine($"Студент {s.Key} посетил: {string.Join(", ", s.Value)}");
            }

            if (students.Count == 0) return;

            // 1. ТРЦ, где были все студенты (Пересечение множеств)
            // Логика: берем первый сет, пересекаем со вторым и т.д.
            HashSet<string> visitedByAll = new HashSet<string>(allMalls);

            var firstStudentSet = students.First().Value;
            visitedByAll = new HashSet<string>(firstStudentSet);

            foreach (var studentSet in students.Values)
            {
                visitedByAll.IntersectWith(studentSet);
            }

            // 2. ТРЦ, где был хотя бы один (Объединение множеств всех студентов)
            HashSet<string> visitedBySome = new HashSet<string>();
            foreach (var studentSet in students.Values)
            {
                visitedBySome.UnionWith(studentSet);
            }

            // 3. ТРЦ, где не был ни один (Все возможные минус те, где был хоть кто-то)
            HashSet<string> visitedByNone = new HashSet<string>(allMalls);
            visitedByNone.ExceptWith(visitedBySome);

            Console.WriteLine("\n1. Посетили все студенты: " + (visitedByAll.Count > 0 ? string.Join(", ", visitedByAll) : "Нет таких"));
            Console.WriteLine("2. Посетили некоторые (хотя бы один): " + (visitedBySome.Count > 0 ? string.Join(", ", visitedBySome) : "Нет таких"));
            Console.WriteLine("3. Никто не посетил: " + (visitedByNone.Count > 0 ? string.Join(", ", visitedByNone) : "Нет таких"));
        }

        // Задание 4. HashSet (Работа с текстом)
        // Файл содержит текст на русском языке. Сколько разных букв встречается в тексте?
        public static void Task4(string filename)
        {
            Console.WriteLine("\n--- Задание 4 ---");

            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            string text = File.ReadAllText(filename, Encoding.UTF8);
            Console.WriteLine($"Текст из файла: {text}");

            HashSet<char> uniqueLetters = new HashSet<char>();

            foreach (char c in text)
            {
                // Приводим к нижнему регистру для корректного подсчета ("А" и "а" — одна буква)
                char lowerChar = char.ToLower(c);

                // Проверяем, является ли символ русской буквой
                bool isRussian = (lowerChar >= 'а' && lowerChar <= 'я') || lowerChar == 'ё';

                if (isRussian)
                {
                    uniqueLetters.Add(lowerChar);
                }
            }

            Console.WriteLine($"\nКоличество разных русских букв в тексте: {uniqueLetters.Count}");
            if (uniqueLetters.Count > 0)
            {
                // Вывод найденных букв в алфавитном порядке для наглядности
                var sortedLetters = uniqueLetters.OrderBy(c => c);
                Console.WriteLine("Найденные буквы: " + string.Join(" ", sortedLetters));
            }
        }

        // Задание 5. Dictionary
        // АЗС. <Компания><Улица><Марка><Цена>.
        // Найти кол-во магазинов с мин. ценой для 92, 95, 98.
        public static void Task5(string filename)
        {
            Console.WriteLine("\n--- Задание 5 ---");

            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            string[] lines = File.ReadAllLines(filename, Encoding.UTF8);
            if (lines.Length == 0) return;

            int n;
            if (!int.TryParse(lines[0], out n))
            {
                Console.WriteLine("Ошибка чтения количества записей.");
                return;
            }

            // Словарь: Марка бензина -> (Минимальная цена, Количество АЗС)
            // Инициализируем максимальными значениями
            var stats = new Dictionary<int, (int minPrice, int count)>
            {
                { 92, (int.MaxValue, 0) },
                { 95, (int.MaxValue, 0) },
                { 98, (int.MaxValue, 0) }
            };

            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 4) continue;

                if (int.TryParse(parts[parts.Length - 1], out int price) &&
                    int.TryParse(parts[parts.Length - 2], out int type))
                {
                    if (stats.ContainsKey(type))
                    {
                        var currentStat = stats[type];

                        if (price < currentStat.minPrice)
                        {
                            stats[type] = (price, 1);
                        }
                        else if (price == currentStat.minPrice)
                        {
                            stats[type] = (currentStat.minPrice, currentStat.count + 1);
                        }
                    }
                }
            }

            Console.WriteLine("Количество АЗС с минимальной ценой (92 95 98):");

            int count92 = stats[92].minPrice == int.MaxValue ? 0 : stats[92].count;
            int count95 = stats[95].minPrice == int.MaxValue ? 0 : stats[95].count;
            int count98 = stats[98].minPrice == int.MaxValue ? 0 : stats[98].count;

            Console.WriteLine($"{count92} {count95} {count98}");
        }
    }
}
