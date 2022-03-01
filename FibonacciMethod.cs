using System;
using System.IO;

namespace MO_Lab1
{
    public class FibonacciMethod
    {
        public Function func { get; set; }
        double A { get; set; }
        double B { get; set; }
        const double EPS = 1E-7;
        const double DELTA = 1E-8;

        //на вход ссылка на функцию и граничные точки
        public FibonacciMethod(Function f, double A, double B)
        {
            this.A = A;
            this.B = B;
            func = f;
        }

        public double StartSolver()
        {
            //число итераций
            int n = GetCountIteration();
            //поток для записи в файл
            var file = new StreamWriter("OutFibonacciMethod.txt", false);

            double[] funcResult = new double[2]; // для результата
            double[] x = new double[2];
            double[] a = new double[2];
            double[] b = new double[2];
            a[1] = A;
            b[1] = B;
            int countIteration = 0;

            //вычисляем начальные значения х и значения функции в этих х
            x[0] = a[1] + ( GetNumberFibonacci(n)/GetNumberFibonacci(n+2) ) * (b[1]-a[1]);
            x[1] = a[1] + ( GetNumberFibonacci(n+1) / GetNumberFibonacci(n + 2) ) * (b[1] - a[1]);
            funcResult[0] = func(x[0]);
            funcResult[1] = func(x[1]);
            while (n>1)
            {
                //запись в файл
                file.Write($"{x[0]} ");
                file.Write($"{x[1]} ");
                file.Write($"{func(x[0])} ");
                file.Write($"{func(x[1])} ");
                if (funcResult[0] > funcResult[1])
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    a[1] = x[0];
                    x[0] = x[1];
                    x[1] = b[1] - (x[0] - a[1]);
                    //вычисляем новое значение функции, другое перезаписываем
                    funcResult[0] = funcResult[1];
                    funcResult[1] = func(x[1]);
                }
                else
                {
                    //сохраняем значения с предыдущих итераций
                    a[0] = a[1];
                    b[0] = b[1];

                    b[1] = x[1];
                    x[1] = x[0];
                    x[0] = a[1] + (b[1] - x[1]);
                    //вычисляем новое значение функции, другое перезаписываем
                    funcResult[1] = funcResult[0];
                    funcResult[0] = func(x[0]);
                }
                countIteration++;
                n--;
                //запись в файл
                file.Write($"{a[1]} ");
                file.Write($"{b[1]} ");
                file.Write($"{b[1] - a[1]} ");
                file.Write($"{(b[0] - a[0]) / (b[1] - a[1])}\n");
            }
            file.Write($"\nТочка минимума: { (x[0] + x[1])/2 }\n");
            file.Write($"Значение функции: {func( (x[0] + x[1]) / 2 )}\n");
            file.Write($"Количество итераций: {countIteration}\n");
            file.Write($"Количество вычислений целевой функции: {countIteration + 1}");
            file.Close();
            return func((x[0] + x[1]) / 2);
        }

        //получить число фибоначи, нумерация с ЕДЕНИЦЫ
        private double GetNumberFibonacci(int n)
        {
            int[] F = new int[3];
            F[0] = 1;
            F[1] = 1;
            F[2] = 2;

            if (n == 1)
                return F[0];
            if (n == 2)
                return F[1];
            if (n == 3)
                return F[2];

            while (n>3)
            {
                UpdateF(F); // вычисление нового числа фибоначи
                n--;
            }

            return (double)F[2];
        }

        //получить число итераций в методе
        private int GetCountIteration()
        {
           

            double d = (B - A) / EPS;
            int[] F = new int[3];
            int count = 1;
            F[0] = 1;
            F[1] = 1;
            F[2] = 2;

            //смотим не превзошли ли мы начальное число фибоначи
            if (d < F[2])
                return count;

            //если нет, то вычисляем новое
            while (d >= F[2])
            {
                UpdateF(F);
                count++;
            }

            return count;
        }

        //вычисление нового числа фибоначи и перезапись двух старых 
        private void UpdateF(int[] F) 
        {
            F[0] = F[1];
            F[1] = F[2];
            F[2] = F[1] + F[0];
        }
    }
}
