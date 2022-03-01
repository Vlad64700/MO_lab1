using System;
using System.IO;
namespace MO_Lab1
{
    public class IntervalSearch
    {
        public Function func { get; set; }
        double A { get; set; }
        const double EPS = 1E-7;
        const double DELTA = 1E-8;

        // на вход ссылка на функцию и начальная точка
        public IntervalSearch(Function f, double A)
        {
            this.A = A;
            func = f;
        }

        public double[] StartSolver()
        {
            //поток для записи в файлик
            var file = new StreamWriter("OutIntervalSearch.txt", false);

            //переменная для хранения х с трех итераций
            double[] x = new double[3];
            x[0] = A;
            x[1] = A;
            x[2] = A;
            int countIteration = 0;
            double h = 0;

            //шаг 1. определяем направление поиска.
            if ( func(x[2])>func(x[2]+DELTA) )
            {
                countIteration = 1;
                x[2] = x[2] + DELTA;
                h = DELTA;
                //запись в файл
                file.Write($"{x[2]} {func(x[2])}\n");
            }
            else if (func(x[2]) > func(x[2] - DELTA))
            {
                x[2] = x[2] - DELTA;
                h = -DELTA;
                //запись в файл
                file.Write($"{x[2]} {func(x[2])}\n");
            }
            //шаг 2
            h *= 2;
            //перезаписываем х
            x[0] = x[1];
            x[1] = x[2];
            x[2] = x[1] + h;
            //запись в файл
            file.Write($"{x[2]} {func(x[2])}\n");

            //шаги 2 и 3
            while ( func(x[1])>func(x[2]) )
            {
                countIteration++;
                h *= 2;
                //перезаписываем х
                x[0] = x[1];
                x[1] = x[2];
                x[2] = x[1] + h;
                //запись в файл
                file.Write($"{x[2]} {func(x[2])}\n");
            }

            double[] result = new double[2];
            result[0] = x[0];
            result[1] = x[2];
            //запись в файл
            file.Write($"\nИнтервал содержащий минимум {result[0]} {result[1]}");
            file.Close();
            return result;
        }
    }
}
