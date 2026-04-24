using System;
using System.Text;

namespace MatrixCalculator.Logic
{
    public class Matrix
    {
        public double[,] Data { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0 || rows > 100 || cols > 100)
                throw new ArgumentException("Размеры матрицы должны быть от 1 до 100");
            Rows = rows;
            Cols = cols;
            Data = new double[Rows, Cols];
        }

        public Matrix(double[,] data)
        {
            if (data.GetLength(0) > 100 || data.GetLength(1) > 100)
                throw new ArgumentException("Размер матрицы превышает 100x100");
            Data = (double[,])data.Clone();
            Rows = data.GetLength(0);
            Cols = data.GetLength(1);
        }

        public double this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }

        // Сложение
        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
                throw new InvalidOperationException("Размеры матриц не совпадают");
            var result = new Matrix(a.Rows, a.Cols);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Cols; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }

        // Вычитание
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
                throw new InvalidOperationException("Размеры матриц не совпадают");
            var result = new Matrix(a.Rows, a.Cols);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Cols; j++)
                    result[i, j] = a[i, j] - b[i, j];
            return result;
        }

        // Умножение на число
        public static Matrix MultiplyByScalar(Matrix a, double scalar)
        {
            var result = new Matrix(a.Rows, a.Cols);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Cols; j++)
                    result[i, j] = a[i, j] * scalar;
            return result;
        }

        // Умножение матриц
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
                throw new InvalidOperationException("Число столбцов A должно равняться числу строк B");
            var result = new Matrix(a.Rows, b.Cols);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < b.Cols; j++)
                    for (int k = 0; k < a.Cols; k++)
                        result[i, j] += a[i, k] * b[k, j];
            return result;
        }

        // Транспонирование
        public Matrix Transpose()
        {
            var result = new Matrix(Cols, Rows);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    result[j, i] = Data[i, j];
            return result;
        }

        // Форматированный вывод
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    sb.Append($"{Data[i, j],10:F3} ");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public bool IsSquare() => Rows == Cols;
    }
}