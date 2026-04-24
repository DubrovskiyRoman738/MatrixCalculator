using System;

namespace MatrixCalculator.Logic
{
    public static class MatrixDeterminant
    {
        // Рекурсивное разложение по первой строке
        public static double RecursiveDeterminant(Matrix m)
        {
            if (!m.IsSquare())
                throw new InvalidOperationException("Определитель только для квадратных матриц");
            if (m.Rows > 10)
                throw new InvalidOperationException("Рекурсивный метод только для матриц ≤10x10");

            return RecursiveDet(m.Data, m.Rows);
        }

        private static double RecursiveDet(double[,] matrix, int n)
        {
            if (n == 1) return matrix[0, 0];
            if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            double det = 0;
            for (int j = 0; j < n; j++)
            {
                var subMatrix = GetSubMatrix(matrix, 0, j, n);
                double sign = (j % 2 == 0) ? 1 : -1;
                det += sign * matrix[0, j] * RecursiveDet(subMatrix, n - 1);
            }
            return det;
        }

        private static double[,] GetSubMatrix(double[,] matrix, int excludeRow, int excludeCol, int n)
        {
            var sub = new double[n - 1, n - 1];
            int r = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == excludeRow) continue;
                int c = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == excludeCol) continue;
                    sub[r, c] = matrix[i, j];
                    c++;
                }
                r++;
            }
            return sub;
        }
    }
}