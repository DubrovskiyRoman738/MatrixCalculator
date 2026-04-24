using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MatrixCalculator.Logic;

namespace MatrixCalculator.Forms
{
    public partial class MainForm : Form
    {
        // Текущие матрицы
        private Matrix matrixA;
        private Matrix matrixB;
        private Matrix resultMatrix;

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaultMatrices();
            AttachEventHandlers();
        }

        private void AttachEventHandlers()
        {
            // Кнопки изменения размера
            btnResizeA.Click += BtnResizeA_Click;
            btnResizeB.Click += BtnResizeB_Click;

            // Кнопки операций
            btnAdd.Click += BtnAdd_Click;
            btnSubtract.Click += BtnSubtract_Click;
            btnMultiply.Click += BtnMultiply_Click;
            btnMultiplyScalar.Click += BtnMultiplyScalar_Click;
            btnTranspose.Click += BtnTranspose_Click;
            btnDeterminant.Click += BtnDeterminant_Click;
            btnInverse.Click += BtnInverse_Click;

            // Кнопки загрузки/сохранения
            btnLoadA.Click += BtnLoadA_Click;
            btnLoadB.Click += BtnLoadB_Click;
            btnRandomA.Click += BtnRandomA_Click;
            btnRandomB.Click += BtnRandomB_Click;
            btnSaveResult.Click += BtnSaveResult_Click;

            // Кнопки СЛАУ
            btnResizeSLAU.Click += BtnResizeSLAU_Click;
            btnSolveSLAU.Click += BtnSolveSLAU_Click;

            // Кнопки обратной матрицы
            btnResizeInv.Click += BtnResizeInv_Click;
            btnComputeInverse.Click += BtnComputeInverse_Click;
            btnCheckInverse.Click += BtnCheckInverse_Click;
        }

        private void InitializeDefaultMatrices()
        {
            // Устанавливаем размер 3x3 для матриц A и B
            nudRowsA.Value = 3;
            nudColsA.Value = 3;
            nudRowsB.Value = 3;
            nudColsB.Value = 3;

            BtnResizeA_Click(null, null);
            BtnResizeB_Click(null, null);

            // Заполняем тестовыми данными матрицу A
            dgvMatrixA.Rows[0].Cells[0].Value = 1;
            dgvMatrixA.Rows[0].Cells[1].Value = 2;
            dgvMatrixA.Rows[0].Cells[2].Value = 3;
            dgvMatrixA.Rows[1].Cells[0].Value = 4;
            dgvMatrixA.Rows[1].Cells[1].Value = 5;
            dgvMatrixA.Rows[1].Cells[2].Value = 6;
            dgvMatrixA.Rows[2].Cells[0].Value = 7;
            dgvMatrixA.Rows[2].Cells[1].Value = 8;
            dgvMatrixA.Rows[2].Cells[2].Value = 9;

            // Заполняем тестовыми данными матрицу B
            dgvMatrixB.Rows[0].Cells[0].Value = 9;
            dgvMatrixB.Rows[0].Cells[1].Value = 8;
            dgvMatrixB.Rows[0].Cells[2].Value = 7;
            dgvMatrixB.Rows[1].Cells[0].Value = 6;
            dgvMatrixB.Rows[1].Cells[1].Value = 5;
            dgvMatrixB.Rows[1].Cells[2].Value = 4;
            dgvMatrixB.Rows[2].Cells[0].Value = 3;
            dgvMatrixB.Rows[2].Cells[1].Value = 2;
            dgvMatrixB.Rows[2].Cells[2].Value = 1;
        }

        /// <summary>
        /// Считывает данные из DataGridView в объект Matrix
        /// </summary>
        private void UpdateMatrixFromGrid(DataGridView dgv, ref Matrix matrix)
        {
            int rows = dgv.RowCount;
            int cols = dgv.ColumnCount;
            double[,] data = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgv.Rows[i].Cells[j].Value;
                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        data[i, j] = value;
                    }
                    else
                    {
                        data[i, j] = 0;
                    }
                }
            }
            matrix = new Matrix(data);
        }

        /// <summary>
        /// Отображает результат в DataGridView
        /// </summary>
        private void DisplayResult(Matrix result)
        {
            dgvResult.RowCount = result.Rows;
            dgvResult.ColumnCount = result.Cols;

            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    dgvResult.Rows[i].Cells[j].Value = result[i, j].ToString("F3");
                }
            }
        }

        /// <summary>
        /// Обновление статуса
        /// </summary>
        private void SetStatus(string message, bool isError = false)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }

        // ==================== ОБРАБОТЧИКИ КНОПОК ====================

        private void BtnResizeA_Click(object sender, EventArgs e)
        {
            int rows = (int)nudRowsA.Value;
            int cols = (int)nudColsA.Value;

            dgvMatrixA.RowCount = rows;
            dgvMatrixA.ColumnCount = cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (dgvMatrixA.Rows[i].Cells[j].Value == null)
                    {
                        dgvMatrixA.Rows[i].Cells[j].Value = "0";
                    }
                }
            }
        }

        private void BtnResizeB_Click(object sender, EventArgs e)
        {
            int rows = (int)nudRowsB.Value;
            int cols = (int)nudColsB.Value;

            dgvMatrixB.RowCount = rows;
            dgvMatrixB.ColumnCount = cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (dgvMatrixB.Rows[i].Cells[j].Value == null)
                    {
                        dgvMatrixB.Rows[i].Cells[j].Value = "0";
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                resultMatrix = Matrix.Add(matrixA, matrixB);
                DisplayResult(resultMatrix);
                SetStatus("✓ Сложение выполнено успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnSubtract_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                resultMatrix = Matrix.Subtract(matrixA, matrixB);
                DisplayResult(resultMatrix);
                SetStatus("✓ Вычитание выполнено успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);

                var stopwatch = Stopwatch.StartNew();
                resultMatrix = Matrix.Multiply(matrixA, matrixB);
                stopwatch.Stop();

                DisplayResult(resultMatrix);
                SetStatus($"✓ Умножение выполнено за {stopwatch.ElapsedMilliseconds} мс");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnMultiplyScalar_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);

                if (!double.TryParse(txtScalar.Text, out double scalar))
                {
                    SetStatus("✗ Введите корректное число в поле скаляра", true);
                    return;
                }

                resultMatrix = Matrix.MultiplyByScalar(matrixA, scalar);
                DisplayResult(resultMatrix);
                SetStatus($"✓ Умножение на {scalar} выполнено");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnTranspose_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                resultMatrix = matrixA.Transpose();
                DisplayResult(resultMatrix);
                SetStatus("✓ Транспонирование выполнено");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnDeterminant_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);

                if (!matrixA.IsSquare())
                {
                    SetStatus("✗ Определитель можно вычислить только для квадратной матрицы", true);
                    return;
                }

                double determinant;
                var stopwatch = Stopwatch.StartNew();

                if (matrixA.Rows <= 10)
                {
                    determinant = MatrixDeterminant.RecursiveDeterminant(matrixA);
                    SetStatus($"📐 Рекурсивный метод (размер {matrixA.Rows}×{matrixA.Rows})");
                }
                else
                {
                    determinant = GaussianElimination.DeterminantByGaussian(matrixA);
                    SetStatus($"📐 Метод Гаусса (размер {matrixA.Rows}×{matrixA.Rows})");
                }

                stopwatch.Stop();
                txtDeterminantResult.Text = determinant.ToString("F6");
                SetStatus($"Определитель = {determinant:F6}, время: {stopwatch.ElapsedMilliseconds} мс");
            }
            catch (Exception ex)
            {
                txtDeterminantResult.Text = "Ошибка";
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnInverse_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);

                if (!matrixA.IsSquare())
                {
                    SetStatus("✗ Обратную матрицу можно вычислить только для квадратной матрицы", true);
                    return;
                }

                var stopwatch = Stopwatch.StartNew();
                resultMatrix = GaussianElimination.InverseGaussJordan(matrixA);
                stopwatch.Stop();

                DisplayResult(resultMatrix);
                SetStatus($"✓ Обратная матрица найдена за {stopwatch.ElapsedMilliseconds} мс");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnLoadA_Click(object sender, EventArgs e)
        {
            LoadMatrixFromFile(dgvMatrixA, "A");
        }

        private void BtnLoadB_Click(object sender, EventArgs e)
        {
            LoadMatrixFromFile(dgvMatrixB, "B");
        }

        private void LoadMatrixFromFile(DataGridView dgv, string matrixName)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.Title = $"Загрузить матрицу {matrixName}";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);
                        int rows = lines.Length;
                        int cols = lines[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

                        dgv.RowCount = rows;
                        dgv.ColumnCount = cols;

                        for (int i = 0; i < rows; i++)
                        {
                            string[] values = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < cols && j < values.Length; j++)
                            {
                                if (double.TryParse(values[j], out double value))
                                {
                                    dgv.Rows[i].Cells[j].Value = value;
                                }
                            }
                        }

                        SetStatus($"✓ Матрица {matrixName} загружена из файла");
                    }
                    catch (Exception ex)
                    {
                        SetStatus($"✗ Ошибка загрузки: {ex.Message}", true);
                    }
                }
            }
        }

        private void BtnRandomA_Click(object sender, EventArgs e)
        {
            FillRandomMatrix(dgvMatrixA);
            SetStatus("✓ Матрица A заполнена случайными числами");
        }

        private void BtnRandomB_Click(object sender, EventArgs e)
        {
            FillRandomMatrix(dgvMatrixB);
            SetStatus("✓ Матрица B заполнена случайными числами");
        }

        private void FillRandomMatrix(DataGridView dgv)
        {
            Random random = new Random();

            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    dgv.Rows[i].Cells[j].Value = random.Next(-10, 11);
                }
            }
        }

        private void BtnSaveResult_Click(object sender, EventArgs e)
        {
            if (resultMatrix == null)
            {
                SetStatus("✗ Нет результата для сохранения", true);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|CSV файлы (*.csv)|*.csv";
                saveFileDialog.Title = "Сохранить результат";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            for (int i = 0; i < resultMatrix.Rows; i++)
                            {
                                for (int j = 0; j < resultMatrix.Cols; j++)
                                {
                                    writer.Write(resultMatrix[i, j].ToString("F3"));
                                    if (j < resultMatrix.Cols - 1)
                                    {
                                        writer.Write("\t");
                                    }
                                }
                                writer.WriteLine();
                            }
                        }

                        SetStatus($"✓ Результат сохранён в {saveFileDialog.FileName}");
                    }
                    catch (Exception ex)
                    {
                        SetStatus($"✗ Ошибка сохранения: {ex.Message}", true);
                    }
                }
            }
        }

        // ==================== ВКЛАДКА "РЕШЕНИЕ СЛАУ" ====================

        private void BtnResizeSLAU_Click(object sender, EventArgs e)
        {
            int size = (int)nudSLAUSize.Value;

            dgvSLAU_A.RowCount = size;
            dgvSLAU_A.ColumnCount = size;
            dgvSLAU_b.RowCount = size;
            dgvSLAU_Result.RowCount = size;

            // Настройка заголовков
            for (int i = 0; i < size; i++)
            {
                dgvSLAU_b.Rows[i].HeaderCell.Value = $"b{i + 1}";
                dgvSLAU_Result.Rows[i].HeaderCell.Value = $"x{i + 1}";

                for (int j = 0; j < size; j++)
                {
                    if (dgvSLAU_A.Rows[i].Cells[j].Value == null)
                    {
                        dgvSLAU_A.Rows[i].Cells[j].Value = "0";
                    }
                }
            }
        }

        private void BtnSolveSLAU_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudSLAUSize.Value;
                double[,] matrixData = new double[size, size];
                double[] vectorB = new double[size];

                // Считываем матрицу A
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var cellValue = dgvSLAU_A.Rows[i].Cells[j].Value;
                        matrixData[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                    }
                }

                // Считываем вектор b
                for (int i = 0; i < size; i++)
                {
                    var cellValue = dgvSLAU_b.Rows[i].Cells[0].Value;
                    vectorB[i] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                }

                var matrixA_SLAU = new Matrix(matrixData);
                double[] solution = GaussianElimination.SolveLinearSystem(matrixA_SLAU, vectorB);

                // Отображаем решение
                for (int i = 0; i < size; i++)
                {
                    dgvSLAU_Result.Rows[i].Cells[0].Value = solution[i].ToString("F6");
                }

                SetStatus("✓ СЛАУ решена успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка решения СЛАУ: {ex.Message}", true);
            }
        }

        // ==================== ВКЛАДКА "ОБРАТНАЯ МАТРИЦА" ====================

        private void BtnResizeInv_Click(object sender, EventArgs e)
        {
            int size = (int)nudInvSize.Value;

            dgvInverse_A.RowCount = size;
            dgvInverse_A.ColumnCount = size;
            dgvInverse_Result.RowCount = size;
            dgvInverse_Result.ColumnCount = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (dgvInverse_A.Rows[i].Cells[j].Value == null)
                    {
                        dgvInverse_A.Rows[i].Cells[j].Value = "0";
                    }
                }
            }
        }

        private void BtnComputeInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;
                double[,] matrixData = new double[size, size];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var cellValue = dgvInverse_A.Rows[i].Cells[j].Value;
                        matrixData[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                    }
                }

                var matrix = new Matrix(matrixData);
                var inverse = GaussianElimination.InverseGaussJordan(matrix);

                // Отображаем обратную матрицу
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        dgvInverse_Result.Rows[i].Cells[j].Value = inverse[i, j].ToString("F6");
                    }
                }

                txtCheckResult.Text = "✓ Обратная матрица вычислена. Нажмите 'Проверить' для верификации.";
                SetStatus("✓ Обратная матрица вычислена");
            }
            catch (Exception ex)
            {
                txtCheckResult.Text = $"✗ Ошибка: {ex.Message}";
                SetStatus($"✗ Ошибка вычисления обратной матрицы", true);
            }
        }

        private void BtnCheckInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;
                double[,] originalData = new double[size, size];
                double[,] inverseData = new double[size, size];

                // Считываем исходную матрицу
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var cellValue = dgvInverse_A.Rows[i].Cells[j].Value;
                        originalData[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                    }
                }

                // Считываем обратную матрицу
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var cellValue = dgvInverse_Result.Rows[i].Cells[j].Value;
                        inverseData[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                    }
                }

                var original = new Matrix(originalData);
                var inverse = new Matrix(inverseData);
                var product = Matrix.Multiply(original, inverse);

                // Проверяем, получилась ли единичная матрица
                bool isIdentity = true;
                double maxError = 0;

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        double expected = (i == j) ? 1.0 : 0.0;
                        double error = Math.Abs(product[i, j] - expected);
                        maxError = Math.Max(maxError, error);

                        if (error > 1e-6)
                        {
                            isIdentity = false;
                        }
                    }
                }

                if (isIdentity)
                {
                    txtCheckResult.Text = $"✓ ПРОВЕРКА ПРОЙДЕНА! A × A⁻¹ = I\nМаксимальная погрешность: {maxError:E6}";
                    SetStatus("✓ Проверка пройдена: произведение даёт единичную матрицу");
                }
                else
                {
                    txtCheckResult.Text = $"✗ ПРОВЕРКА НЕ ПРОЙДЕНА! A × A⁻¹ ≠ I\nМаксимальная погрешность: {maxError:E6}";
                    SetStatus("✗ Проверка не пройдена: произведение не даёт единичную матрицу", true);
                }
            }
            catch (Exception ex)
            {
                txtCheckResult.Text = $"✗ Ошибка проверки: {ex.Message}";
                SetStatus($"✗ Ошибка проверки: {ex.Message}", true);
            }
        }
    }
}