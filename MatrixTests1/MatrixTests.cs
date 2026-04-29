using MatrixCalculator.Logic;
using Microsoft.ApplicationInsights;
using NUnit.Framework;
using System;

namespace MatrixCalculator.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        private const double EPS = 1e-6;

        // ========== 1. ТЕСТЫ КОНСТРУКТОРОВ ==========
        //
        [Test]
        public void Constructor_ValidDimensions_CreatesMatrix()
        {
            var m = new Matrix(3, 4);
            Assert.Multiple(() =>
            {
                Assert.That(m.Rows, Is.EqualTo(3));
                Assert.That(m.Cols, Is.EqualTo(4));
            });
        }

        [Test]
        public void Constructor_InvalidRows_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Matrix(0, 5));
            Assert.Throws<ArgumentException>(() => new Matrix(101, 5));
        }

        [Test]
        public void Constructor_InvalidCols_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Matrix(5, 0));
            Assert.Throws<ArgumentException>(() => new Matrix(5, 101));
        }

        [Test]
        public void Constructor_FromArray_CreatesCopy()
        {
            double[,] data = { { 1, 2 }, { 3, 4 } };
            var m = new Matrix(data);
            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[1, 1], Is.EqualTo(4));
        }

        [Test]
        public void Indexer_SetAndGet_WorksCorrectly()
        {
            var m = new Matrix(2, 2);
            m[0, 0] = 10;
            m[1, 1] = 20;
            Assert.That(m[0, 0], Is.EqualTo(10));
            Assert.That(m[1, 1], Is.EqualTo(20));
        }

        // ========== 2. ТЕСТЫ СЛОЖЕНИЯ И ВЫЧИТАНИЯ ==========

        [Test]
        public void Add_ValidMatrices_ReturnsSum()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
            var result = Matrix.Add(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(result[0, 0], Is.EqualTo(6));
                Assert.That(result[0, 1], Is.EqualTo(8));
                Assert.That(result[1, 0], Is.EqualTo(10));
                Assert.That(result[1, 1], Is.EqualTo(12));
            });
        }

        [Test]
        public void Add_DifferentSizes_ThrowsException()
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(3, 2);
            Assert.Throws<InvalidOperationException>(() => Matrix.Add(a, b));
        }

        [Test]
        public void Subtract_ValidMatrices_ReturnsDifference()
        {
            var a = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
            var b = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var result = Matrix.Subtract(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(result[0, 0], Is.EqualTo(4));
                Assert.That(result[0, 1], Is.EqualTo(4));
                Assert.That(result[1, 0], Is.EqualTo(4));
                Assert.That(result[1, 1], Is.EqualTo(4));
            });
        }

        [Test]
        public void Subtract_DifferentSizes_ThrowsException()
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(2, 2);
            Assert.Throws<InvalidOperationException>(() => Matrix.Subtract(a, b));
        }

        // ========== 3. ТЕСТЫ УМНОЖЕНИЯ ==========

        [Test]
        public void MultiplyByScalar_Valid_ReturnsScaledMatrix()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var result = Matrix.MultiplyByScalar(a, 2);

            Assert.Multiple(() =>
            {
                Assert.That(result[0, 0], Is.EqualTo(2));
                Assert.That(result[0, 1], Is.EqualTo(4));
                Assert.That(result[1, 0], Is.EqualTo(6));
                Assert.That(result[1, 1], Is.EqualTo(8));
            });
        }

        [Test]
        public void MultiplyByScalar_ZeroScalar_ReturnsZeroMatrix()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var result = Matrix.MultiplyByScalar(a, 0);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    Assert.That(result[i, j], Is.EqualTo(0));
        }

        [Test]
        public void Multiply_ValidMatrices_ReturnsProduct()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new double[,] { { 2, 0 }, { 1, 2 } });
            var result = Matrix.Multiply(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(result[0, 0], Is.EqualTo(4));
                Assert.That(result[0, 1], Is.EqualTo(4));
                Assert.That(result[1, 0], Is.EqualTo(10));
                Assert.That(result[1, 1], Is.EqualTo(8));
            });
        }

        [Test]
        public void Multiply_IncompatibleDimensions_ThrowsException()
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(4, 5);
            Assert.Throws<InvalidOperationException>(() => Matrix.Multiply(a, b));
        }

        [Test]
        public void Multiply_2x3by3x2_Returns2x2()
        {
            var a = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            var b = new Matrix(new double[,] { { 7, 8 }, { 9, 10 }, { 11, 12 } });
            var result = Matrix.Multiply(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(result.Rows, Is.EqualTo(2));
                Assert.That(result.Cols, Is.EqualTo(2));
                Assert.That(result[0, 0], Is.EqualTo(58).Within(EPS));
                Assert.That(result[0, 1], Is.EqualTo(64).Within(EPS));
            });
        }

        // ========== 4. ТЕСТЫ ТРАНСПОНИРОВАНИЯ ==========

        [Test]
        public void Transpose_3x2_Returns2x3()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            var result = a.Transpose();

            Assert.Multiple(() =>
            {
                Assert.That(result.Rows, Is.EqualTo(2));
                Assert.That(result.Cols, Is.EqualTo(3));
                Assert.That(result[0, 0], Is.EqualTo(1));
                Assert.That(result[0, 1], Is.EqualTo(3));
                Assert.That(result[0, 2], Is.EqualTo(5));
                Assert.That(result[1, 0], Is.EqualTo(2));
                Assert.That(result[1, 1], Is.EqualTo(4));
                Assert.That(result[1, 2], Is.EqualTo(6));
            });
        }

        [Test]
        public void Transpose_SquareMatrix_WorksCorrectly()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var result = a.Transpose();

            Assert.That(result[0, 1], Is.EqualTo(3));
            Assert.That(result[1, 0], Is.EqualTo(2));
        }

        [Test]
        public void IsSquare_SquareMatrix_ReturnsTrue()
        {
            var m = new Matrix(5, 5);
            Assert.That(m.IsSquare(), Is.True);
        }

        [Test]
        public void IsSquare_NonSquareMatrix_ReturnsFalse()
        {
            var m = new Matrix(3, 5);
            Assert.That(m.IsSquare(), Is.False);
        }

        // ========== 5. ТЕСТЫ РЕКУРСИВНОГО ОПРЕДЕЛИТЕЛЯ ==========

        [Test]
        public void RecursiveDeterminant_1x1_ReturnsElement()
        {
            var m = new Matrix(new double[,] { { 5 } });
            double det = MatrixDeterminant.RecursiveDeterminant(m);
            Assert.That(det, Is.EqualTo(5).Within(EPS));
        }

        [Test]
        public void RecursiveDeterminant_2x2_ReturnsCorrect()
        {
            var m = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            double det = MatrixDeterminant.RecursiveDeterminant(m);
            Assert.That(det, Is.EqualTo(-2).Within(EPS));
        }

        [Test]
        public void RecursiveDeterminant_3x3_ReturnsCorrect()
        {
            var m = new Matrix(new double[,] { { 1, 2, 3 }, { 0, 1, 4 }, { 5, 6, 0 } });
            double det = MatrixDeterminant.RecursiveDeterminant(m);
            Assert.That(det, Is.EqualTo(1).Within(EPS));
        }

        [Test]
        public void RecursiveDeterminant_Identity4x4_ReturnsOne()
        {
            var m = new Matrix(4, 4);
            for (int i = 0; i < 4; i++)
                m[i, i] = 1;

            double det = MatrixDeterminant.RecursiveDeterminant(m);
            Assert.That(det, Is.EqualTo(1).Within(EPS));
        }

        [Test]
        public void RecursiveDeterminant_ZeroMatrix_ReturnsZero()
        {
            var m = new Matrix(3, 3);
            double det = MatrixDeterminant.RecursiveDeterminant(m);
            Assert.That(det, Is.EqualTo(0).Within(EPS));
        }

        [Test]
        public void RecursiveDeterminant_NonSquare_ThrowsException()
        {
            var m = new Matrix(2, 3);
            Assert.Throws<InvalidOperationException>(() => MatrixDeterminant.RecursiveDeterminant(m));
        }

        [Test]
        public void RecursiveDeterminant_TooLarge_ThrowsException()
        {
            var m = new Matrix(11, 11);
            Assert.Throws<InvalidOperationException>(() => MatrixDeterminant.RecursiveDeterminant(m));
        }

        // ========== 6. ТЕСТЫ МЕТОДА ГАУССА (ОПРЕДЕЛИТЕЛЬ) ==========

        [Test]
        public void GaussianDeterminant_2x2_ReturnsCorrect()
        {
            var m = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            double det = GaussianElimination.DeterminantByGaussian(m);
            Assert.That(det, Is.EqualTo(-2).Within(EPS));
        }

        [Test]
        public void GaussianDeterminant_3x3_ReturnsCorrect()
        {
            var m = new Matrix(new double[,] { { 2, -1, 0 }, { -1, 2, -1 }, { 0, -1, 2 } });
            double det = GaussianElimination.DeterminantByGaussian(m);
            Assert.That(det, Is.EqualTo(4).Within(EPS));
        }

        [Test]
        public void GaussianDeterminant_Singular_ReturnsZero()
        {
            var m = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            double det = GaussianElimination.DeterminantByGaussian(m);
            Assert.That(det, Is.EqualTo(0).Within(EPS));
        }

        [Test]
        public void GaussianDeterminant_5x5_MatchesRecursive()
        {
            double[,] data = {
                { 2, 1, 0, 0, 1 },
                { 1, 3, 1, 0, 0 },
                { 0, 1, 4, 1, 0 },
                { 0, 0, 1, 5, 1 },
                { 1, 0, 0, 1, 6 }
            };
            var m = new Matrix(data);

            double detGauss = GaussianElimination.DeterminantByGaussian(m);
            double detRec = MatrixDeterminant.RecursiveDeterminant(m);

            Assert.That(detGauss, Is.EqualTo(detRec).Within(EPS));
        }

        [Test]
        public void GaussianDeterminant_ZeroRow_ReturnsZero()
        {
            var m = new Matrix(new double[,] { { 0, 0, 0 }, { 1, 2, 3 }, { 4, 5, 6 } });
            double det = GaussianElimination.DeterminantByGaussian(m);
            Assert.That(det, Is.EqualTo(0).Within(EPS));
        }

        // ========== 7. ТЕСТЫ РЕШЕНИЯ СЛАУ ==========

        [Test]
        public void SolveLinearSystem_2x2_ReturnsCorrect()
        {
            var a = new Matrix(new double[,] { { 3, 2 }, { 1, 2 } });
            double[] b = { 7, 3 };
            double[] x = GaussianElimination.SolveLinearSystem(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(x[0], Is.EqualTo(2).Within(EPS));
                Assert.That(x[1], Is.EqualTo(0.5).Within(EPS));
            });
        }

        [Test]
        public void SolveLinearSystem_3x3_ReturnsCorrect()
        {
            var a = new Matrix(new double[,] {
                { 2, 1, -1 },
                { -3, -1, 2 },
                { -2, 1, 2 }
            });
            double[] b = { 8, -11, -3 };
            double[] x = GaussianElimination.SolveLinearSystem(a, b);

            Assert.Multiple(() =>
            {
                Assert.That(x[0], Is.EqualTo(2).Within(EPS));
                Assert.That(x[1], Is.EqualTo(3).Within(EPS));
                Assert.That(x[2], Is.EqualTo(-1).Within(EPS));
            });
        }

        [Test]
        public void SolveLinearSystem_Singular_ThrowsException()
        {
            var a = new Matrix(new double[,] { { 1, 2 }, { 2, 4 } });
            double[] b = { 3, 6 };
            Assert.Throws<InvalidOperationException>(() => GaussianElimination.SolveLinearSystem(a, b));
        }

        [Test]
        public void SolveLinearSystem_NonSquare_ThrowsException()
        {
            var a = new Matrix(2, 3);
            double[] b = { 1, 2 };
            Assert.Throws<InvalidOperationException>(() => GaussianElimination.SolveLinearSystem(a, b));
        }

        // ========== 8. ТЕСТЫ ОБРАТНОЙ МАТРИЦЫ ==========

        [Test]
        public void Inverse_2x2_ProductIsIdentity()
        {
            var a = new Matrix(new double[,] { { 4, 7 }, { 2, 6 } });
            var inv = GaussianElimination.InverseGaussJordan(a);
            var product = Matrix.Multiply(a, inv);

            Assert.Multiple(() =>
            {
                Assert.That(product[0, 0], Is.EqualTo(1).Within(EPS));
                Assert.That(product[0, 1], Is.EqualTo(0).Within(EPS));
                Assert.That(product[1, 0], Is.EqualTo(0).Within(EPS));
                Assert.That(product[1, 1], Is.EqualTo(1).Within(EPS));
            });
        }

        [Test]
        public void Inverse_3x3_ProductIsIdentity()
        {
            var a = new Matrix(new double[,] {
                { 4, 1, 2 },
                { 2, 3, 1 },
                { 1, 2, 3 }
            });
            var inv = GaussianElimination.InverseGaussJordan(a);
            var product = Matrix.Multiply(a, inv);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.That(product[i, j], Is.EqualTo(i == j ? 1 : 0).Within(EPS));
        }

        [Test]
        public void Inverse_Singular_ThrowsException()
        {
            var a = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Assert.Throws<InvalidOperationException>(() => GaussianElimination.InverseGaussJordan(a));
        }

        [Test]
        public void Inverse_NonSquare_ThrowsException()
        {
            var a = new Matrix(2, 3);
            Assert.Throws<InvalidOperationException>(() => GaussianElimination.InverseGaussJordan(a));
        }

        [Test]
        public void Inverse_Identity_ReturnsIdentity()
        {
            var a = new Matrix(3, 3);
            for (int i = 0; i < 3; i++) a[i, i] = 1;

            var inv = GaussianElimination.InverseGaussJordan(a);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.That(inv[i, j], Is.EqualTo(i == j ? 1 : 0).Within(EPS));
        }

        // ========== 9. ГРАНИЧНЫЕ СЛУЧАИ ==========

        [Test]
        public void EdgeCase_1x1_OperationsWork()
        {
            var a = new Matrix(new double[,] { { 10 } });
            var b = new Matrix(new double[,] { { 5 } });

            var sum = Matrix.Add(a, b);
            var prod = Matrix.Multiply(a, b);

            Assert.That(sum[0, 0], Is.EqualTo(15));
            Assert.That(prod[0, 0], Is.EqualTo(50));
        }

        [Test]
        public void EdgeCase_NegativeNumbers_WorkCorrectly()
        {
            var a = new Matrix(new double[,] { { -1, -2 }, { -3, -4 } });
            var b = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });
            var sum = Matrix.Add(a, b);

            Assert.That(sum[0, 0], Is.EqualTo(4));
            Assert.That(sum[1, 1], Is.EqualTo(4));
        }

        [Test]
        public void EdgeCase_DecimalNumbers_WorkCorrectly()
        {
            var a = new Matrix(new double[,] { { 1.5, 2.5 }, { 3.5, 4.5 } });
            var b = new Matrix(new double[,] { { 0.5, 0.5 }, { 0.5, 0.5 } });
            var sum = Matrix.Add(a, b);

            Assert.That(sum[0, 0], Is.EqualTo(2.0).Within(EPS));
            Assert.That(sum[0, 1], Is.EqualTo(3.0).Within(EPS));
        }

        [Test]
        public void EdgeCase_MaxSize_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new Matrix(100, 100));
        }

        // ========== 10. ТЕСТЫ ПРОИЗВОДИТЕЛЬНОСТИ ==========

        [Test]
        public void Performance_50x50Multiplication_Completes()
        {
            var a = new Matrix(50, 50);
            var b = new Matrix(50, 50);

            for (int i = 0; i < 50; i++)
            {
                a[i, i] = 1;
                b[i, i] = 1;
            }

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var result = Matrix.Multiply(a, b);
            sw.Stop();

            Assert.That(result.Rows, Is.EqualTo(50));
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(5000));
        }

        [Test]
        public void Performance_100x100Determinant_Completes()
        {
            var a = new Matrix(100, 100);
            for (int i = 0; i < 100; i++)
                a[i, i] = 1;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            double det = GaussianElimination.DeterminantByGaussian(a);
            sw.Stop();

            Assert.That(det, Is.EqualTo(1).Within(EPS));
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(2000));
        }
    }
}