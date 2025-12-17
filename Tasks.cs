using System.Text;


internal static class Lab4Tasks
{
    private static readonly CheckInput _check = new CheckInput();

    // Задание 1. List
    public static void Task1()
    {
        Console.WriteLine("--- Задание 1: List (Пересечение списков) ---");

        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();

        Console.WriteLine("Заполнение списка L1:");
        int count1 = _check.ReadPositiveInt("Введите количество элементов L1: ");
        for (int i = 0; i < count1; i++)
            list1.Add(_check.ReadInt($"Элемент {i + 1}: "));

        Console.WriteLine("Заполнение списка L2:");
        int count2 = _check.ReadPositiveInt("Введите количество элементов L2: ");
        for (int i = 0; i < count2; i++)
            list2.Add(_check.ReadInt($"Элемент {i + 1}: "));

        // Используем метод Intersect(пересечение) для нахождения общих элементов без дубликатов
        List<int> resultList = list1.Intersect(list2).ToList();

        Console.WriteLine("\nРезультат (элементы, входящие в оба списка):");
        if (resultList.Count > 0)
        {
            Console.WriteLine(string.Join(", ", resultList));
        }
        else
        {
            Console.WriteLine("Общих элементов нет.");
        }
    }

    // Задание 2. LinkedList 
    // Добавить в конец списка все его элементы в обратном порядке
    public static void Task2()
    {
        Console.WriteLine("--- Задание 2: LinkedList (Зеркальное добавление) ---");

        LinkedList<int> linkedList = new LinkedList<int>();
        int count = _check.ReadPositiveInt("Введите количество элементов списка: ");

        for (int i = 0; i < count; i++)
        {
            linkedList.AddLast(_check.ReadInt($"Элемент {i + 1}: "));
        }

        Console.WriteLine($"Исходный список: {string.Join(", ", linkedList)}");


        List<int> temp = new List<int>(linkedList);
        temp.Reverse();

        foreach (var item in temp)
        {
            linkedList.AddLast(item);
        }

        Console.WriteLine($"Результат: {string.Join(", ", linkedList)}");
    }

    // Задание 3. HashSet 
    // Анализ посещения ТРЦ студентами
    public static void Task3()
    {
        Console.WriteLine("--- Задание 3: HashSet (ТРЦ) ---");

        // Формируем общий перечень ТРЦ
        HashSet<string> allMalls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        Console.WriteLine("Введите общий список ТРЦ (введите 'end' для завершения):");
        while (true)
        {
            Console.Write("- ");
            string? input = Console.ReadLine()?.Trim();
            if (string.Equals(input, "end", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(input))
                break;
            allMalls.Add(input);
        }

        if (allMalls.Count == 0)
        {
            Console.WriteLine("Список ТРЦ пуст.");
            return;
        }

        int studentCount = _check.ReadPositiveInt("Введите количество студентов: ");
        List<HashSet<string>> studentVisits = new List<HashSet<string>>();

        for (int i = 0; i < studentCount; i++)
        {
            HashSet<string> visits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine($"Студент {i + 1}. Введите посещенные ТРЦ через запятую:");
            string? line = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    string mall = part.Trim();
                    // Учитываем только ТРЦ из общего списка
                    if (allMalls.Contains(mall))
                    {
                        visits.Add(mall);
                    }
                }
            }
            studentVisits.Add(visits);
        }

        // А) Посетили все студенты (Пересечение множеств)
        HashSet<string> visitedByAll = new HashSet<string>(allMalls, StringComparer.OrdinalIgnoreCase);
        if (studentVisits.Count > 0)
        {
            foreach (var visits in studentVisits)
            {
                visitedByAll.IntersectWith(visits);
            }
        }
        else
        {
            visitedByAll.Clear();
        }

        // Б) Посетили некоторые (хотя бы один) (Объединение множеств)
        HashSet<string> visitedBySome = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var visits in studentVisits)
        {
            visitedBySome.UnionWith(visits);
        }

        // В) Никто не посетил (Разность: Все - Посещенные кем-то)
        HashSet<string> visitedByNone = new HashSet<string>(allMalls, StringComparer.OrdinalIgnoreCase);
        visitedByNone.ExceptWith(visitedBySome);

        Console.WriteLine("\n--- Результаты ---");
        Console.WriteLine($"ТРЦ, которые посетили ВСЕ: {string.Join(", ", visitedByAll)}");
        Console.WriteLine($"ТРЦ, которые посетили НЕКОТОРЫЕ: {string.Join(", ", visitedBySome)}");
        Console.WriteLine($"ТРЦ, которые НЕ посетил НИКТО: {string.Join(", ", visitedByNone)}");
    }

    // Задание 4. HashSet 
    // Количество разных букв в тексте файла
    public static void Task4()
    {
        Console.WriteLine("--- Задание 4: HashSet (Анализ текста) ---");
        string filename = "text_task4.txt";

        
        CreateTask4File(filename);

        string text = ReadTextFromFile(filename);

        HashSet<char> uniqueLetters = new HashSet<char>();

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                uniqueLetters.Add(char.ToLower(c)); // Считаем без учета регистра
            }
        }

        Console.WriteLine($"Текст из файла: {text}");
        Console.WriteLine($"Количество различных букв: {uniqueLetters.Count}");
        Console.WriteLine($"Буквы: {string.Join(" ", uniqueLetters.OrderBy(c => c))}");
    }

    // Создание файла для Задания 4
    private static void CreateTask4File(string filename)
    {
        Console.WriteLine($"\nРабота с файлом данных: {filename}");

        if (File.Exists(filename))
        {
            Console.WriteLine("Файл уже существует.");
        }

        Console.Write("Хотите ввести новый текст вручную (y - да, иначе - использовать текущий/стандартный)? ");
        string? answer = Console.ReadLine();

        if (answer?.Trim().ToLower() == "y")
        {
            Console.WriteLine("Введите текст для анализа:");
            string? text = Console.ReadLine();
            File.WriteAllText(filename, text ?? string.Empty, Encoding.UTF8);
            Console.WriteLine("Текст успешно записан в файл.");
        }
        else if (!File.Exists(filename))
        {
            // Если файл не существует и пользователь отказался вводить вручную - создаем дефолтный
            File.WriteAllText(filename, "Тестовый файл для задания 4. Определение количества различных букв.", Encoding.UTF8);
            Console.WriteLine($"Создан тестовый файл {filename}");
        }
    }

    // Чтение текста для Задания 4
    private static string ReadTextFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            return File.ReadAllText(filename);
        }
        return string.Empty;
    }

    // Задание 5. Dictionary/SortedList
    // Соревнования по многоборью
    public static void Task5()
    {
        Console.WriteLine("=== Задание 5: Многоборье (Файл) ===");
        string filename = "competition_task5.txt";

        CreateTask5File(filename);       
        List<AthleteInfo> athletes = ReadAthletesFromFile(filename);

        if (athletes.Count == 0)
        {
            Console.WriteLine("Нет данных для обработки.");
            return;
        }

        
        var sortedAthletes = athletes.OrderByDescending(a => a.TotalScore).ToList();

       
        Console.WriteLine("\n{0,-20} {1,10} {2,5}", "Спортсмен", "Сумма", "Место");
        Console.WriteLine(new string('-', 40));

        int currentRank = 1;
        for (int i = 0; i < sortedAthletes.Count; i++)
        {
            if (i > 0 && sortedAthletes[i].TotalScore < sortedAthletes[i - 1].TotalScore)
            {
                currentRank++;
            }

            Console.WriteLine("{0,-20} {1,10} {2,5}",
                sortedAthletes[i].FullName,
                sortedAthletes[i].TotalScore,
                currentRank);
        }
    }

    // Считывание и парсинг спортсменов из файла для задания 5
    private static List<AthleteInfo> ReadAthletesFromFile(string filename)
    {
        var resultList = new List<AthleteInfo>();

        if (!File.Exists(filename))
        {
            Console.WriteLine("Ошибка: Файл не найден.");
            return resultList;
        }

        string[] lines = File.ReadAllLines(filename);
        if (lines.Length < 3)
        {
            Console.WriteLine("Ошибка: Некорректный формат файла.");
            return resultList;
        }

        if (!int.TryParse(lines[0], out int n) || !int.TryParse(lines[1], out int m))
        {
            Console.WriteLine("Ошибка: Неверный формат N или M.");
            return resultList;
        }

       
        for (int i = 2; i < lines.Length && i < 2 + n; i++)
        {
            string[] parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            
            if (parts.Length < 2 + m) continue;

            string surname = parts[0];
            string name = parts[1];
            int sum = 0;

            for (int j = 0; j < m; j++)
            {
                if (int.TryParse(parts[2 + j], out int score))
                {
                    sum += score;
                }
            }

            resultList.Add(new AthleteInfo { FullName = $"{surname} {name}", TotalScore = sum });
        }

        return resultList;
    }

    // Создание/Заполнение файла для задания 5
    private static void CreateTask5File(string filename)
    {
        Console.WriteLine($"\nРабота с файлом данных: {filename}");
        bool fileExists = File.Exists(filename);

        if (fileExists)
        {
            Console.WriteLine("Файл уже существует.");
        }

        Console.Write("Хотите ввести новые данные вручную (y - да, иначе - использовать текущие/стандартные)? ");
        string? answer = Console.ReadLine();

        if (answer?.Trim().ToLower() == "y")
        {
            Console.WriteLine("\n--- Ввод данных ---");

            using (StreamWriter sw = new StreamWriter(filename))
            {
                int n = _check.ReadPositiveInt("Введите количество спортсменов (N): ");
                int m = _check.ReadPositiveInt("Введите количество видов спорта (M): ");

                sw.WriteLine(n);
                sw.WriteLine(m);

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine($"\nСпортсмен {i + 1}:");
                    string surname = string.Empty;

                    while (string.IsNullOrWhiteSpace(surname))
                    {
                        Console.Write("Фамилия: ");
                        surname = Console.ReadLine()?.Trim() ?? "";
                    }

                    string name = string.Empty;
                    while (string.IsNullOrWhiteSpace(name))
                    {
                        Console.Write("Имя: ");
                        name = Console.ReadLine()?.Trim() ?? "";
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append($"{surname} {name}");

                    for (int j = 0; j < m; j++)
                    {
                        int score = _check.ReadInt($"Баллы за вид спорта {j + 1}: ");
                        sb.Append($" {score}");
                    }

                    sw.WriteLine(sb.ToString());
                }
            }
            Console.WriteLine("Данные успешно записаны в файл.");

        }
        else
        {
            // Если файла нету генерируются данные по умолчанию
            if (!fileExists)
            {
                Console.WriteLine("Файл не найден. Создаем тестовые данные по умолчанию...");
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine("3"); // N спортсменов
                    sw.WriteLine("4"); // M видов спорта
                    sw.WriteLine("Иванов Сергей 100 30 78 13");
                    sw.WriteLine("Петров Антон 90 16 98 14");
                    sw.WriteLine("Сидоров Юрий 100 70 30 21");
                }
                Console.WriteLine($"Создан файл данных {filename}");
            }
        }
    }
}
