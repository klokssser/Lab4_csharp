using System;

namespace Lab4
{
    // Задание 6 и 7. Класс LineSegment (Отрезок)
    public class LineSegment
    {
        // Поля: координаты начала и конца на координатной прямой
        // предполагаем, что X <= Y.
        // Если пользователь вводит наоборот, конструктор это исправит.
        private double x;
        private double y;

        public double X
        {
            get { return x; }
            set { x = value; Normalize(); }
        }

        public double Y
        {
            get { return y; }
            set { y = value; Normalize(); }
        }

        // Конструктор
        public LineSegment(double x, double y)
        {
            this.x = x;
            this.y = y;
            Normalize();
        }

        // Метод нормализации (гарантирует, что x <= y)
        private void Normalize()
        {
            if (x > y)
            {
                double temp = x;
                x = y;
                y = temp;
            }
        }

        // Перегрузка ToString
        public override string ToString()
        {
            return $"[{x:F2}; {y:F2}]";
        }

        // Задание 6: Определить, пересекаются ли заданные отрезки
        public bool IsIntersect(LineSegment other)
        {
            if (other == null) return false;
            // Условие пересечения: max(x1, x2) < min(y1, y2)
            return Math.Max(this.x, other.x) <= Math.Min(this.y, other.y);
        }

        // --- ЗАДАНИЕ 7: Перегрузка операций ---

        // 1. Унарный ! : вычислить длину диапазона (тип double)
        public static double operator !(LineSegment s)
        {
            return Math.Abs(s.y - s.x);
        }

        // 2. Унарный ++ : расширить отрезок на 1 вправо и влево
        public static LineSegment operator ++(LineSegment s)
        {
            // Расширяем: левую границу уменьшаем, правую увеличиваем
            return new LineSegment(s.x - 1, s.y + 1);
        }

        // 3. Неявное приведение int: целая часть координаты x
        public static implicit operator int(LineSegment s)
        {
            return (int)s.x;
        }

        // 4. Явное приведение double: координата y
        public static explicit operator double(LineSegment s)
        {
            return s.y;
        }

        // 5. Бинарный - (
        public static LineSegment operator -(LineSegment s, int val)
        {
            return new LineSegment(s.x - val, s.y);
        }

        public static LineSegment operator -(int val, LineSegment s)
        {
            return new LineSegment(s.x, s.y - val);
        }

        // 6. Бинарный <: результат true, если отрезки пересекаются
        // В C# операторы сравнения должны быть парными. Если есть <, должен быть >.
        public static bool operator <(LineSegment s1, LineSegment s2)
        {
            return s1.IsIntersect(s2);
        }
        
        public static bool operator >(LineSegment s1, LineSegment s2)
        {
            return s1.IsIntersect(s2);
        }
    }
}
