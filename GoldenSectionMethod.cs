using System;
using System.IO;

namespace MO_Lab1
{
    public class GoldenSectionMethod
    {
        public Function func { get; set; }
        double A { get; set; }
        double B { get; set; }
        const double EPS = 1E-7;
        const double DELTA = 1E-4;

        //на вход ссылка на функцию и граничные точки
        public GoldenSectionMethod(Function f, double A, double B)
        {
            this.A = A;
            this.B = B;
            func = f;
        }

        public double StartSolver()
        {
            //поток для записи в файл
            var file = new StreamWriter("OutGoldenSectionMethod.txt",false);

            double[] x = new double[2];
            double[] a = new double[2];
            double[] b = new double[2];
            double[] funcResult = new double[2]; // для результата
            a[1] = A;
            b[1] = B;
            int countIteration = 0;
            
            x[0] = (a[1] + 0.381966011d * (b[1] - a[1]));
            x[1] = (a[1] + 0.6180033989d * (b[1] - a[1]));
            funcResult[0] = func(x[0]);
            funcResult[1] = func(x[1]);
            while (b[1] - a[1] > EPS)
            {
                //запись в файл
                file.Write($"{x[0]} ");
                file.Write($"{x[1]} ");
                file.Write($"{func(x[0])} ");
                file.Write($"{func(x[1])} ");

                if (funcResult[0] < funcResult[1])
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    b[1] = x[1];
                    x[1] = x[0];
                    x[0] = (a[1] + 0.381966011d * (b[1] - a[1]));
                    //вычисляем ОДНО новое значение функции, другое перезаписываем
                    funcResult[1] = funcResult[0];
                    funcResult[0] = func(x[0]);
                }
                else
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    a[1] = x[0];
                    x[0] = x[1];
                    x[1] = (a[1] + 0.6180033989d * (b[1] - a[1]));
                    //вычисляем ОДНО новое значение функции, другое перезаписываем
                    funcResult[0] = funcResult[1];
                    funcResult[1] = func(x[1]);
                }
                countIteration++;
                //запись в файл
                file.Write($"{a[1]} ");
                file.Write($"{b[1]} ");
                file.Write($"{b[1] - a[1]} ");
                file.Write($"{(b[0] - a[0]) / (b[1] - a[1])}\n");
            }
            file.Write($"\nТочка минимума: {a[1]}\n");
            file.Write($"Значение функции: {func(a[1])}\n");
            file.Write($"Количество итераций: {countIteration}\n");
            file.Write($"Количество вычислений целевой функции: {countIteration+1}");
            file.Close();
            return func(a[1]);
        }
    }
}
