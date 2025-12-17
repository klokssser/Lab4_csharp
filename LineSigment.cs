// Задания 6 и 7 
// Класс LineSegment (Отрезок на координатной прямой)
public class LineSegment
{
    private double _x;
    private double _y;

    public LineSegment(double x, double y)
    {
        // Храним так, чтобы _x было левой границей, _y правой
        if (x <= y)
        {
            _x = x;
            _y = y;
        }
        else
        {
            _x = y;
            _y = x;
        }
    }

    public double X => _x;
    public double Y => _y;

    public override string ToString()
    {
        return $"[{_x}; {_y}]";
    }

    // ЗАДАНИЕ 6: Метод для определения пересечения с другим отрезком
    public bool Intersects(LineSegment other)
    {
        if (other == null) return false;
        // Условие пересечения 1D отрезков: Max(Starts) <= Min(Ends)
        double maxStart = Math.Max(_x, other._x);
        double minEnd = Math.Min(_y, other._y);
        return maxStart <= minEnd;
    }

    // --- ЗАДАНИЕ 7: Перегрузка операций ---

    // 1. Унарный !: вычислить длину отрезка
    public static double operator !(LineSegment segment)
    {
        return Math.Abs(segment._y - segment._x);
    }

    // 2. Унарный ++: расширить отрезок на 1 влево и вправо
    public static LineSegment operator ++(LineSegment segment)
    {
        segment._x -= 1;
        segment._y += 1;
        return segment;
    }

    // 3. Операции приведения типа
    // int (неявная) – целая часть координаты х
    public static implicit operator int(LineSegment segment)
    {
        return (int)segment._x;
    }

    // double (явная) – координата y
    public static explicit operator double(LineSegment segment)
    {
        return segment._y;
    }

    // 4. Бинарные операции

    // - LineSegment, целое число (левосторонняя: уменьшается координата х)
    public static LineSegment operator -(LineSegment segment, int value)
    {
        return new LineSegment(segment._x - value, segment._y);
    }

    // - целое число, LineSegment (правосторонняя: уменьшается координата y)
    public static LineSegment operator -(int value, LineSegment segment)
    {
        return new LineSegment(segment._x, segment._y - value);
    }

    // логическое меньше ('<') – результат равен true, если отрезки пересекаются
    public static bool operator <(LineSegment a, LineSegment b)
    {
        return a.Intersects(b);
    }

    // Оператор > должен быть реализован в паре c <.
    // Пусть он возвращает true, если отрезки НЕ пересекаются (инверсия <).
    public static bool operator >(LineSegment a, LineSegment b)
    {
        return !a.Intersects(b);
    }
}
