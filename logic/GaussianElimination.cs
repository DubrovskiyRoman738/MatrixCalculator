using System;

namespace MatrixCalculator.Logic
{
    public static class GaussianElimination
    {
        // Определитель через приведение к треугольному виду (любого размера до 100x100)
        public static double DeterminantByGaussian(Matrix m)
        {
            if (!m.IsSquare())
                throw new InvalidOperationException("Определитель только для квадратных матриц");

            var temp = (double[,])m.Data.Clone();
            int n = m.Rows;
            double det = 1.0;

            for (int i = 0; i < n; i++)
            {
                // Поиск главного элемента
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                    if (Math.Abs(temp[k, i]) > Math.Abs(temp[maxRow, i]))
                        maxRow = k;

                if (Math.Abs(temp[maxRow, i]) < 1e-10)
                    return 0; // определитель 0

                if (maxRow != i)
                {
                    for (int j = i; j < n; j++)
                        (temp[i, j], temp[maxRow, j]) = (temp[maxRow, j], temp[i, j]);
                    det *= -1;
                }

                det *= temp[i, i];

                for (int k = i + 1; k < n; k++)
                {
                    double factor = temp[k, i] / temp[i, i];
                    for (int j = i; j < n; j++)
                        temp[k, j] -= factor * temp[i, j];
                }
            }
            return det;
        }

        // Решение СЛАУ A*x = b
        public static double[] SolveLinearSystem(Matrix a, double[] b)
        {
            if (!a.IsSquare() || a.Rows != b.Length)
                throw new InvalidOperationException("Несоответствие размеров");

            int n = a.Rows;
            var augmented = new double[n, n + 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    augmented[i, j] = a[i, j];
                augmented[i, n] = b[i];
            }

            // Прямой ход
            for (int i = 0; i < n; i++)
            {
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                    if (Math.Abs(augmented[k, i]) > Math.Abs(augmented[maxRow, i]))
                        maxRow = k;

                if (maxRow != i)
                    for (int j = i; j <= n; j++)
                        (augmented[i, j], augmented[maxRow, j]) = (augmented[maxRow, j], augmented[i, j]);

                if (Math.Abs(augmented[i, i]) < 1e-10)
                    throw new InvalidOperationException("Вырожденная система");

                for (int k = i + 1; k < n; k++)
                {
                    double factor = augmented[k, i] / augmented[i, i];
                    for (int j = i; j <= n; j++)
                        augmented[k, j] -= factor * augmented[i, j];
                }
            }

            // Обратный ход
            var x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                    sum += augmented[i, j] * x[j];
                x[i] = (augmented[i, n] - sum) / augmented[i, i];
            }
            return x;
        }

        // Обратная матрица методом Гаусса-Жордана
        public static Matrix InverseGaussJordan(Matrix m)
        {
            if (!m.IsSquare())
                throw new InvalidOperationException("Обратная только для квадратной матрицы");

            int n = m.Rows;
            var augmented = new double[n, 2 * n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    augmented[i, j] = m[i, j];
                augmented[i, n + i] = 1.0;
            }

            for (int i = 0; i < n; i++)
            {
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                    if (Math.Abs(augmented[k, i]) > Math.Abs(augmented[maxRow, i]))
                        maxRow = k;

                if (maxRow != i)
                    for (int j = 0; j < 2 * n; j++)
                        (augmented[i, j], augmented[maxRow, j]) = (augmented[maxRow, j], augmented[i, j]);

                double pivot = augmented[i, i];
                if (Math.Abs(pivot) < 1e-10)
                    throw new InvalidOperationException("Матрица вырождена, обратной не существует");

                for (int j = 0; j < 2 * n; j++)
                    augmented[i, j] /= pivot;

                for (int k = 0; k < n; k++)
                {
                    if (k == i) continue;
                    double factor = augmented[k, i];
                    for (int j = 0; j < 2 * n; j++)
                        augmented[k, j] -= factor * augmented[i, j];
                }
            }

            var inverse = new Matrix(n, n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inverse[i, j] = augmented[i, n + j];

            return inverse;
        }
    }
}