using System;
using System.IO;

namespace MO_Lab1
{
    public class DichotomyMethod
    {
        public Function func { get; set; }
        double A { get; set; }
        double B { get; set; }
        const double EPS = 1E-7;
        const double DELTA = 1E-10;

        //на вход ссылка на функцию и граничные точки
        public DichotomyMethod(Function f, double A, double B)
        {
            this.A = A;
            this.B = B;
            func = f;
        }

        public double StartSolver()
        {
            //поток для записи в файлик
            var file = new StreamWriter("OutDichotomyMethod.txt", false);

            double[] x = new double[2];
            double[] a = new double[2];
            double[] b = new double[2];
            a[1] = A;
            b[1] = B;
            int countIteration = 0;

            while (b[1]-a[1]>EPS)
            {
                x[0] = ((a[1] + b[1] - DELTA) / 2.0);
                x[1] = ((a[1] + b[1] + DELTA) / 2.0);
                //запись в файл
                file.Write($"{x[0]} ");
                file.Write($"{x[1]} ");
                file.Write($"{func(x[0])} ");
                file.Write($"{func(x[1])} ");

                if (func(x[0]) < func(x[1]))
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    b[1] = x[1];

                }
                else
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    a[1] = x[0];
                }
                countIteration++;

                //запись в файл
                file.Write($"{a[1]} ");
                file.Write($"{b[1]} ");
                file.Write($"{b[1] - a[1]} ");
                file.Write($"{(b[0] - a[0])/(b[1] - a[1])}\n");
            }
            file.Write($"\nТочка минимума: {a[1]}\n");
            file.Write($"Значение функции: {func(a[1])}\n");
            file.Write($"Количество итераций: {countIteration}\n");
            file.Write($"Количество вычислений целевой функции: {countIteration*2}");
            file.Close();
            return func(a[1]);
        }
    }
}
