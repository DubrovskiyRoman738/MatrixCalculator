using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MatrixCalculator.Logic;

namespace MatrixCalculator.Forms
{
    public partial class MainForm : Form
    {
        private Matrix matrixA;
        private Matrix matrixB;
        private Matrix resultMatrix;

        public MainForm()
        {
            InitializeComponent();
            InitializeDefaultMatrices();
            AttachEventHandlers();
            AddToLog("═══════════════════════════════════════════════════════════════════════════════════════════════════");
            AddToLog("✅ ПРОГРАММА ЗАПУЩЕНА | Калькулятор матричных операций");
            AddToLog($"📅 Дата и время: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            AddToLog("═══════════════════════════════════════════════════════════════════════════════════════════════════");
            AddToLog("");
        }

        // ========== МЕТОДЫ ЛОГИРОВАНИЯ ==========
        private void AddToLog(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => AddToLog(message)));
                return;
            }
            rtbLog.AppendText(message + "\r\n");
            rtbLog.ScrollToCaret();
        }

        private void AddMatrixToLog(string name, Matrix m)
        {
            AddToLog($"┌─ {name} ({m.Rows}×{m.Cols}) ─────────────────────────────────────────────");
            for (int i = 0; i < m.Rows; i++)
            {
                string row = "│ ";
                for (int j = 0; j < m.Cols; j++)
                    row += $"{m[i, j],10:F3} ";
                AddToLog(row);
            }
            AddToLog($"└─────────────────────────────────────────────────────────────────────────");
        }

        private void AddSeparatorToLog()
        {
            AddToLog("─────────────────────────────────────────────────────────────────────────────");
        }

        private void AttachEventHandlers()
        {
            btnResizeA.Click += BtnResizeA_Click;
            btnResizeB.Click += BtnResizeB_Click;
            btnAdd.Click += BtnAdd_Click;
            btnSubtract.Click += BtnSubtract_Click;
            btnMultiply.Click += BtnMultiply_Click;
            btnMultiplyScalar.Click += BtnMultiplyScalar_Click;
            btnTranspose.Click += BtnTranspose_Click;
            btnDeterminant.Click += BtnDeterminant_Click;
            btnInverse.Click += BtnInverse_Click;
            btnB_MultiplyScalar.Click += BtnB_MultiplyScalar_Click;
            btnB_Transpose.Click += BtnB_Transpose_Click;
            btnB_Determinant.Click += BtnB_Determinant_Click;
            btnB_Inverse.Click += BtnB_Inverse_Click;
            btnLoadA.Click += BtnLoadA_Click;
            btnLoadB.Click += BtnLoadB_Click;
            btnRandomA.Click += BtnRandomA_Click;
            btnRandomB.Click += BtnRandomB_Click;
            btnSaveResult.Click += BtnSaveResult_Click;
            btnResizeSLAU.Click += BtnResizeSLAU_Click;
            btnSolveSLAU.Click += BtnSolveSLAU_Click;
            btnSLAU_Random.Click += BtnSLAU_Random_Click;
            btnSLAU_Load.Click += BtnSLAU_Load_Click;
            btnResizeInv.Click += BtnResizeInv_Click;
            btnComputeInverse.Click += BtnComputeInverse_Click;
            btnCheckInverse.Click += BtnCheckInverse_Click;
            btnInv_Random.Click += BtnInv_Random_Click;
            btnInv_Load.Click += BtnInv_Load_Click;

            // Кнопки лога
            btnClearLog.Click += (s, e) => { rtbLog.Clear(); AddToLog("📋 Лог очищен"); };
            btnSaveLog.Click += (s, e) => SaveLogToFile();
        }

        private void SaveLogToFile()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы (*.txt)|*.txt";
                sfd.FileName = $"MatrixLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, rtbLog.Text);
                    AddToLog($"💾 Лог сохранён в файл: {sfd.FileName}");
                }
            }
        }

        private void InitializeDefaultMatrices()
        {
            nudRowsA.Value = 3;
            nudColsA.Value = 3;
            nudRowsB.Value = 3;
            nudColsB.Value = 3;

            BtnResizeA_Click(null, null);
            BtnResizeB_Click(null, null);

            dgvMatrixA.Rows[0].Cells[0].Value = 1; dgvMatrixA.Rows[0].Cells[1].Value = 2; dgvMatrixA.Rows[0].Cells[2].Value = 3;
            dgvMatrixA.Rows[1].Cells[0].Value = 4; dgvMatrixA.Rows[1].Cells[1].Value = 5; dgvMatrixA.Rows[1].Cells[2].Value = 6;
            dgvMatrixA.Rows[2].Cells[0].Value = 7; dgvMatrixA.Rows[2].Cells[1].Value = 8; dgvMatrixA.Rows[2].Cells[2].Value = 9;

            dgvMatrixB.Rows[0].Cells[0].Value = 9; dgvMatrixB.Rows[0].Cells[1].Value = 8; dgvMatrixB.Rows[0].Cells[2].Value = 7;
            dgvMatrixB.Rows[1].Cells[0].Value = 6; dgvMatrixB.Rows[1].Cells[1].Value = 5; dgvMatrixB.Rows[1].Cells[2].Value = 4;
            dgvMatrixB.Rows[2].Cells[0].Value = 3; dgvMatrixB.Rows[2].Cells[1].Value = 2; dgvMatrixB.Rows[2].Cells[2].Value = 1;

            AddToLog("📊 Начальные матрицы инициализированы (размер 3×3)");
        }

        private void UpdateMatrixFromGrid(DataGridView dgv, ref Matrix matrix)
        {
            int rows = dgv.RowCount;
            int cols = dgv.ColumnCount;
            double[,] data = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgv.Rows[i].Cells[j].Value;
                    data[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double v)) ? v : 0;
                }
            matrix = new Matrix(data);
        }

        private void DisplayResult(Matrix result)
        {
            dgvResult.RowCount = result.Rows;
            dgvResult.ColumnCount = result.Cols;
            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Cols; j++)
                    dgvResult.Rows[i].Cells[j].Value = result[i, j].ToString("F3");
        }

        private void SetStatus(string message, bool isError = false)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            AddToLog(message);
        }

        // ==================== РАЗМЕРЫ ====================
        private void BtnResizeA_Click(object sender, EventArgs e)
        {
            int rows = (int)nudRowsA.Value;
            int cols = (int)nudColsA.Value;
            dgvMatrixA.RowCount = rows;
            dgvMatrixA.ColumnCount = cols;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (dgvMatrixA.Rows[i].Cells[j].Value == null)
                        dgvMatrixA.Rows[i].Cells[j].Value = "0";
            AddToLog($"📏 Размер матрицы A изменён на {rows}×{cols}");
        }

        private void BtnResizeB_Click(object sender, EventArgs e)
        {
            int rows = (int)nudRowsB.Value;
            int cols = (int)nudColsB.Value;
            dgvMatrixB.RowCount = rows;
            dgvMatrixB.ColumnCount = cols;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (dgvMatrixB.Rows[i].Cells[j].Value == null)
                        dgvMatrixB.Rows[i].Cells[j].Value = "0";
            AddToLog($"📏 Размер матрицы B изменён на {rows}×{cols}");
        }

        // ==================== УМНОЖЕНИЕ НА ЧИСЛО ====================
        private void BtnMultiplyScalar_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                if (!double.TryParse(txtScalar.Text, out double scalar))
                {
                    SetStatus("✗ Введите корректное число", true);
                    return;
                }
                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog($"║                    УМНОЖЕНИЕ МАТРИЦЫ A НА ЧИСЛО {scalar}                             ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Исходная матрица A", matrixA);

                AddToLog($"\n🔢 ПОШАГОВЫЙ РАСЧЁТ: Умножаем каждый элемент на {scalar}");
                for (int i = 0; i < matrixA.Rows; i++)
                    for (int j = 0; j < matrixA.Cols; j++)
                        AddToLog($"   A[{i}][{j}] = {matrixA[i, j]} × {scalar} = {matrixA[i, j] * scalar}");

                resultMatrix = Matrix.MultiplyByScalar(matrixA, scalar);

                AddToLog("\n✅ РЕЗУЛЬТАТ УМНОЖЕНИЯ:");
                AddMatrixToLog($"Результат B = A × {scalar}", resultMatrix);
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Матрица A умножена на {scalar}");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== СЛОЖЕНИЕ ====================
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                              СЛОЖЕНИЕ МАТРИЦ A + B                                  ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Матрица A", matrixA);
                AddMatrixToLog("Матрица B", matrixB);

                AddToLog("\n🔢 ПОШАГОВЫЙ РАСЧЁТ СЛОЖЕНИЯ:");
                for (int i = 0; i < matrixA.Rows; i++)
                    for (int j = 0; j < matrixA.Cols; j++)
                        AddToLog($"   C[{i}][{j}] = {matrixA[i, j]} + {matrixB[i, j]} = {matrixA[i, j] + matrixB[i, j]}");

                resultMatrix = Matrix.Add(matrixA, matrixB);

                AddToLog("\n✅ РЕЗУЛЬТАТ СЛОЖЕНИЯ:");
                AddMatrixToLog("Результат C = A + B", resultMatrix);
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus("✓ Сложение выполнено успешно");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ВЫЧИТАНИЕ ====================
        private void BtnSubtract_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                              ВЫЧИТАНИЕ МАТРИЦ A - B                                  ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Матрица A", matrixA);
                AddMatrixToLog("Матрица B", matrixB);

                AddToLog("\n🔢 ПОШАГОВЫЙ РАСЧЁТ ВЫЧИТАНИЯ:");
                for (int i = 0; i < matrixA.Rows; i++)
                    for (int j = 0; j < matrixA.Cols; j++)
                        AddToLog($"   C[{i}][{j}] = {matrixA[i, j]} - {matrixB[i, j]} = {matrixA[i, j] - matrixB[i, j]}");

                resultMatrix = Matrix.Subtract(matrixA, matrixB);

                AddToLog("\n✅ РЕЗУЛЬТАТ ВЫЧИТАНИЯ:");
                AddMatrixToLog("Результат C = A - B", resultMatrix);
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus("✓ Вычитание выполнено успешно");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== УМНОЖЕНИЕ МАТРИЦ ====================
        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                              УМНОЖЕНИЕ МАТРИЦ A × B                                  ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Матрица A", matrixA);
                AddMatrixToLog("Матрица B", matrixB);

                AddToLog($"\n🔢 ПОШАГОВЫЙ РАСЧЁТ УМНОЖЕНИЯ");
                AddToLog("   Формула: C[i][j] = Σ(k=1..n) A[i][k] × B[k][j]\n");

                var sw = Stopwatch.StartNew();
                resultMatrix = Matrix.Multiply(matrixA, matrixB);
                sw.Stop();

                for (int i = 0; i < resultMatrix.Rows; i++)
                {
                    for (int j = 0; j < resultMatrix.Cols; j++)
                    {
                        string calc = $"   C[{i}][{j}] = ";
                        double sum = 0;
                        for (int k = 0; k < matrixA.Cols; k++)
                        {
                            double term = matrixA[i, k] * matrixB[k, j];
                            sum += term;
                            calc += $"{matrixA[i, k]}×{matrixB[k, j]}";
                            if (k < matrixA.Cols - 1) calc += " + ";
                            else calc += $" = {sum}";
                        }
                        AddToLog(calc);
                    }
                }

                AddToLog($"\n✅ РЕЗУЛЬТАТ УМНОЖЕНИЯ (время: {sw.ElapsedMilliseconds} мс):");
                AddMatrixToLog("Результат C = A × B", resultMatrix);
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Умножение выполнено за {sw.ElapsedMilliseconds} мс");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ТРАНСПОНИРОВАНИЕ ====================
        private void BtnTranspose_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                           ТРАНСПОНИРОВАНИЕ МАТРИЦЫ A                                ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Исходная матрица A", matrixA);

                AddToLog("\n🔄 ПОШАГОВЫЙ РАСЧЁТ ТРАНСПОНИРОВАНИЯ:");
                AddToLog("   Меняем строки и столбцы местами: Aᵀ[j][i] = A[i][j]\n");
                for (int i = 0; i < matrixA.Rows; i++)
                    for (int j = 0; j < matrixA.Cols; j++)
                        AddToLog($"   Aᵀ[{j}][{i}] = A[{i}][{j}] = {matrixA[i, j]}");

                resultMatrix = matrixA.Transpose();

                AddToLog("\n✅ РЕЗУЛЬТАТ ТРАНСПОНИРОВАНИЯ:");
                AddMatrixToLog($"Результат Aᵀ ({resultMatrix.Rows}×{resultMatrix.Cols})", resultMatrix);
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus("✓ Транспонирование A выполнено");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ОПРЕДЕЛИТЕЛЬ ====================
        private void BtnDeterminant_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                if (!matrixA.IsSquare())
                {
                    SetStatus("✗ Определитель только для квадратной матрицы", true);
                    return;
                }

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                              ВЫЧИСЛЕНИЕ ОПРЕДЕЛИТЕЛЯ                                ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Матрица A", matrixA);

                double det;
                var sw = Stopwatch.StartNew();

                if (matrixA.Rows <= 10)
                {
                    AddToLog($"\n📐 Метод: Рекурсивное разложение по первой строке (матрица {matrixA.Rows}×{matrixA.Rows})");

                    if (matrixA.Rows == 2)
                    {
                        AddToLog("   Формула для матрицы 2×2: det = a₁₁×a₂₂ - a₁₂×a₂₁");
                        AddToLog($"   Расчёт: det = {matrixA[0, 0]}×{matrixA[1, 1]} - {matrixA[0, 1]}×{matrixA[1, 0]}");
                        double step1 = matrixA[0, 0] * matrixA[1, 1];
                        double step2 = matrixA[0, 1] * matrixA[1, 0];
                        AddToLog($"          = {step1} - {step2}");
                        det = step1 - step2;
                        AddToLog($"          = {det}");
                    }
                    else if (matrixA.Rows == 3)
                    {
                        AddToLog("   Формула для матрицы 3×3 (правило Саррюса):");
                        AddToLog("   det = a₁₁×a₂₂×a₃₃ + a₁₂×a₂₃×a₃₁ + a₁₃×a₂₁×a₃₂");
                        AddToLog("         - a₁₃×a₂₂×a₃₁ - a₁₁×a₂₃×a₃₂ - a₁₂×a₂₁×a₃₃\n");

                        double term1 = matrixA[0, 0] * matrixA[1, 1] * matrixA[2, 2];
                        double term2 = matrixA[0, 1] * matrixA[1, 2] * matrixA[2, 0];
                        double term3 = matrixA[0, 2] * matrixA[1, 0] * matrixA[2, 1];
                        double term4 = matrixA[0, 2] * matrixA[1, 1] * matrixA[2, 0];
                        double term5 = matrixA[0, 0] * matrixA[1, 2] * matrixA[2, 1];
                        double term6 = matrixA[0, 1] * matrixA[1, 0] * matrixA[2, 2];

                        AddToLog($"   Положительные члены:");
                        AddToLog($"     + {matrixA[0, 0]}×{matrixA[1, 1]}×{matrixA[2, 2]} = {term1}");
                        AddToLog($"     + {matrixA[0, 1]}×{matrixA[1, 2]}×{matrixA[2, 0]} = {term2}");
                        AddToLog($"     + {matrixA[0, 2]}×{matrixA[1, 0]}×{matrixA[2, 1]} = {term3}");
                        AddToLog($"   Отрицательные члены:");
                        AddToLog($"     - {matrixA[0, 2]}×{matrixA[1, 1]}×{matrixA[2, 0]} = {term4}");
                        AddToLog($"     - {matrixA[0, 0]}×{matrixA[1, 2]}×{matrixA[2, 1]} = {term5}");
                        AddToLog($"     - {matrixA[0, 1]}×{matrixA[1, 0]}×{matrixA[2, 2]} = {term6}");

                        det = term1 + term2 + term3 - term4 - term5 - term6;
                        AddToLog($"\n   Результат: det = {term1} + {term2} + {term3} - {term4} - {term5} - {term6} = {det}");
                    }
                    else
                    {
                        det = MatrixDeterminant.RecursiveDeterminant(matrixA);
                        AddToLog($"\n   Рекурсивное разложение выполнено");
                        AddToLog($"   Результат: det(A) = {det:F6}");
                    }
                }
                else
                {
                    AddToLog($"\n📐 Метод: Гаусса (приведение к треугольному виду, матрица {matrixA.Rows}×{matrixA.Rows})");
                    AddToLog("   ШАГ 1: Приводим матрицу к верхнетреугольному виду");
                    AddToLog("   ШАГ 2: Определитель = произведение диагональных элементов\n");
                    det = GaussianElimination.DeterminantByGaussian(matrixA);
                    AddToLog($"   Результат: det(A) = {det:F6}");
                }
                sw.Stop();
                txtDeterminantResult.Text = det.ToString("F6");

                AddToLog($"\n⏱ Время выполнения: {sw.ElapsedMilliseconds} мс");
                AddSeparatorToLog();
                SetStatus($"✓ Определитель A = {det:F6} (время: {sw.ElapsedMilliseconds} мс)");
            }
            catch (Exception ex)
            {
                txtDeterminantResult.Text = "Ошибка";
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        // ==================== ОБРАТНАЯ МАТРИЦА ====================
        private void BtnInverse_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                if (!matrixA.IsSquare())
                {
                    SetStatus("✗ Обратная только для квадратной матрицы", true);
                    return;
                }

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                         ВЫЧИСЛЕНИЕ ОБРАТНОЙ МАТРИЦЫ A⁻¹                            ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddMatrixToLog("Исходная матрица A", matrixA);

                AddToLog("\n🔧 Метод: Гаусса-Жордана");
                AddToLog("   Составляем расширенную матрицу [A|E]");
                AddToLog("   Приводим левую часть к единичной матрице");
                AddToLog("   Правая часть становится обратной матрицей\n");

                var sw = Stopwatch.StartNew();
                resultMatrix = GaussianElimination.InverseGaussJordan(matrixA);
                sw.Stop();

                AddToLog("\n✅ РЕЗУЛЬТАТ - ОБРАТНАЯ МАТРИЦА A⁻¹:");
                AddMatrixToLog("A⁻¹", resultMatrix);

                var product = Matrix.Multiply(matrixA, resultMatrix);
                double maxError = 0;
                for (int i = 0; i < product.Rows; i++)
                    for (int j = 0; j < product.Cols; j++)
                    {
                        double expected = (i == j) ? 1.0 : 0.0;
                        maxError = Math.Max(maxError, Math.Abs(product[i, j] - expected));
                    }

                AddToLog($"\n🔍 Проверка: A × A⁻¹ = I\n   Погрешность: {maxError:E6} {(maxError < 1e-6 ? "✅" : "❌")}");
                AddToLog($"⏱ Время выполнения: {sw.ElapsedMilliseconds} мс");
                AddSeparatorToLog();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Обратная матрица A найдена за {sw.ElapsedMilliseconds} мс");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ОПЕРАЦИИ ДЛЯ МАТРИЦЫ B (упрощённо с логом) ====================
        private void BtnB_MultiplyScalar_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                if (!double.TryParse(txtBScalar.Text, out double scalar))
                {
                    SetStatus("✗ Введите корректное число", true);
                    return;
                }
                AddToLog($"\n🔄 Умножение матрицы B на число {scalar}");
                resultMatrix = Matrix.MultiplyByScalar(matrixB, scalar);
                DisplayResult(resultMatrix);
                SetStatus($"✓ Матрица B умножена на {scalar}");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnB_Transpose_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                AddToLog($"\n🔄 Транспонирование матрицы B");
                resultMatrix = matrixB.Transpose();
                DisplayResult(resultMatrix);
                SetStatus("✓ Транспонирование B выполнено");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnB_Determinant_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                if (!matrixB.IsSquare())
                {
                    SetStatus("✗ Только для квадратной матрицы", true);
                    return;
                }
                double det;
                if (matrixB.Rows <= 10)
                    det = MatrixDeterminant.RecursiveDeterminant(matrixB);
                else
                    det = GaussianElimination.DeterminantByGaussian(matrixB);
                txtBDeterminantResult.Text = det.ToString("F6");
                AddToLog($"\n📐 Определитель B = {det:F6}");
                SetStatus($"✓ Определитель B = {det:F6}");
            }
            catch (Exception ex)
            {
                txtBDeterminantResult.Text = "Ошибка";
                SetStatus($"✗ Ошибка: {ex.Message}", true);
            }
        }

        private void BtnB_Inverse_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                if (!matrixB.IsSquare())
                {
                    SetStatus("✗ Только для квадратной матрицы", true);
                    return;
                }
                AddToLog($"\n🔄 Вычисление обратной матрицы B⁻¹");
                resultMatrix = GaussianElimination.InverseGaussJordan(matrixB);
                DisplayResult(resultMatrix);
                SetStatus($"✓ Обратная матрица B найдена");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ЗАГРУЗКА/СОХРАНЕНИЕ ====================
        private void BtnLoadA_Click(object sender, EventArgs e) => LoadMatrixFromFile(dgvMatrixA, "A");
        private void BtnLoadB_Click(object sender, EventArgs e) => LoadMatrixFromFile(dgvMatrixB, "B");

        private void LoadMatrixFromFile(DataGridView dgv, string name)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt";
                ofd.Title = $"Загрузить матрицу {name}";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName);
                        int rows = lines.Length;
                        int cols = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        dgv.RowCount = rows;
                        dgv.ColumnCount = cols;
                        for (int i = 0; i < rows; i++)
                        {
                            var vals = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < cols && j < vals.Length; j++)
                                if (double.TryParse(vals[j], out double v)) dgv.Rows[i].Cells[j].Value = v;
                        }
                        AddToLog($"📂 Матрица {name} загружена из файла '{Path.GetFileName(ofd.FileName)}' ({rows}×{cols})");
                        SetStatus($"✓ Матрица {name} загружена");
                    }
                    catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
                }
            }
        }

        private void BtnRandomA_Click(object sender, EventArgs e) => FillRandomMatrix(dgvMatrixA, "A");
        private void BtnRandomB_Click(object sender, EventArgs e) => FillRandomMatrix(dgvMatrixB, "B");

        private void FillRandomMatrix(DataGridView dgv, string name)
        {
            Random rnd = new Random();
            AddToLog($"🎲 Генерация случайной матрицы {name} (от -10 до 10)...");
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                    dgv.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
            SetStatus($"✓ Матрица {name} заполнена случайными числами");
        }

        private void BtnSaveResult_Click(object sender, EventArgs e)
        {
            if (resultMatrix == null)
            {
                SetStatus("✗ Нет результата для сохранения", true);
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы (*.txt)|*.txt|CSV (*.csv)|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            for (int i = 0; i < resultMatrix.Rows; i++)
                            {
                                for (int j = 0; j < resultMatrix.Cols; j++)
                                {
                                    sw.Write(resultMatrix[i, j].ToString("F3"));
                                    if (j < resultMatrix.Cols - 1) sw.Write("\t");
                                }
                                sw.WriteLine();
                            }
                        AddToLog($"💾 Результат сохранён в файл '{Path.GetFileName(sfd.FileName)}'");
                        SetStatus($"✓ Результат сохранён");
                    }
                    catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
                }
            }
        }

        // ==================== СЛАУ ====================
        private void BtnResizeSLAU_Click(object sender, EventArgs e)
        {
            int size = (int)nudSLAUSize.Value;

            dgvSLAU_A.RowCount = size;
            dgvSLAU_A.ColumnCount = size;
            dgvSLAU_b.RowCount = size;
            dgvSLAU_b.ColumnCount = 1;
            dgvSLAU_Result.RowCount = size;
            dgvSLAU_Result.ColumnCount = 1;

            dgvSLAU_b.ColumnHeadersVisible = false;
            dgvSLAU_Result.ColumnHeadersVisible = false;

            // Инициализируем пустые ячейки нулями
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (dgvSLAU_A.Rows[i].Cells[j].Value == null)
                        dgvSLAU_A.Rows[i].Cells[j].Value = "0";
                }
                if (dgvSLAU_b.Rows[i].Cells[0].Value == null)
                    dgvSLAU_b.Rows[i].Cells[0].Value = "0";
                dgvSLAU_Result.Rows[i].Cells[0].Value = "";
            }

            AddToLog($"📏 Размер системы СЛАУ изменён на {size}×{size}");
        }

        private void BtnSLAU_Random_Click(object sender, EventArgs e)
        {
            int size = (int)nudSLAUSize.Value;
            Random rnd = new Random();
            BtnResizeSLAU_Click(null, null);
            AddToLog($"🎲 Генерация случайной системы СЛАУ {size}×{size}...");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    dgvSLAU_A.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
                dgvSLAU_b.Rows[i].Cells[0].Value = rnd.Next(-10, 11);
            }
            SetStatus("✓ Случайная система сгенерирована");
        }

        private void BtnSLAU_Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt";
                ofd.Title = "Загрузить систему уравнений";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName);
                        int size = lines.Length;

                        // ВАЖНО: Сначала устанавливаем размер!
                        nudSLAUSize.Value = size;

                        // Устанавливаем размеры DataGridView
                        dgvSLAU_A.RowCount = size;
                        dgvSLAU_A.ColumnCount = size;
                        dgvSLAU_b.RowCount = size;
                        dgvSLAU_b.ColumnCount = 1;
                        dgvSLAU_Result.RowCount = size;
                        dgvSLAU_Result.ColumnCount = 1;

                        AddToLog($"📂 Загрузка системы СЛАУ из файла '{Path.GetFileName(ofd.FileName)}'...");

                        for (int i = 0; i < size; i++)
                        {
                            // Разбиваем строку на числа
                            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                            // Заполняем матрицу A
                            for (int j = 0; j < size && j < parts.Length - 1; j++)
                            {
                                if (double.TryParse(parts[j], out double value))
                                    dgvSLAU_A.Rows[i].Cells[j].Value = value;
                                else
                                    dgvSLAU_A.Rows[i].Cells[j].Value = 0;
                            }

                            // Заполняем вектор b (последний элемент в строке)
                            if (parts.Length > size && double.TryParse(parts[size], out double bValue))
                                dgvSLAU_b.Rows[i].Cells[0].Value = bValue;
                            else if (parts.Length == size + 1 && double.TryParse(parts[size], out bValue))
                                dgvSLAU_b.Rows[i].Cells[0].Value = bValue;
                            else
                                dgvSLAU_b.Rows[i].Cells[0].Value = 0;
                        }

                        // Очищаем результат
                        for (int i = 0; i < size; i++)
                            dgvSLAU_Result.Rows[i].Cells[0].Value = "";

                        AddToLog($"✅ Система {size}×{size} загружена из файла");
                        SetStatus($"✓ Система загружена из файла (размер {size}×{size})");
                    }
                    catch (Exception ex)
                    {
                        SetStatus($"✗ Ошибка загрузки: {ex.Message}", true);
                        AddToLog($"❌ Ошибка: {ex.Message}");
                    }
                }
            }
        }

        private void BtnSolveSLAU_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudSLAUSize.Value;

                // ПРОВЕРКА: убеждаемся, что DataGridView имеет правильный размер
                if (dgvSLAU_A.RowCount != size || dgvSLAU_A.ColumnCount != size)
                {
                    BtnResizeSLAU_Click(null, null);
                }

                AddToLog("");
                AddToLog("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                AddToLog("║                         РЕШЕНИЕ СИСТЕМЫ ЛИНЕЙНЫХ УРАВНЕНИЙ                          ║");
                AddToLog("╚══════════════════════════════════════════════════════════════════════════════════════╝");
                AddToLog($"Размер системы: {size}×{size}\n");

                double[,] a = new double[size, size];
                double[] b = new double[size];

                AddToLog("📋 Исходная система:");
                for (int i = 0; i < size; i++)
                {
                    string eq = "   ";
                    for (int j = 0; j < size; j++)
                    {
                        // ПРОВЕРКА: читаем значение из ячейки
                        var cellValue = dgvSLAU_A.Rows[i].Cells[j].Value;
                        a[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                        eq += $"{a[i, j],8:F2} x{j + 1}";
                        if (j < size - 1) eq += " + ";
                    }

                    var bCellValue = dgvSLAU_b.Rows[i].Cells[0].Value;
                    b[i] = (bCellValue != null && double.TryParse(bCellValue.ToString(), out double bVal)) ? bVal : 0;
                    eq += $" = {b[i],8:F2}";
                    AddToLog(eq);
                }

                AddToLog("\n🔧 МЕТОД ГАУССА:");
                AddToLog("   ШАГ 1: Прямой ход (приведение к треугольному виду)");

                var sw = Stopwatch.StartNew();
                double[] x = GaussianElimination.SolveLinearSystem(new Matrix(a), b);
                sw.Stop();

                AddToLog("\n   ШАГ 2: Обратный ход");
                AddToLog("\n✅ РЕШЕНИЕ СИСТЕМЫ:");
                for (int i = 0; i < size; i++)
                {
                    dgvSLAU_Result.Rows[i].Cells[0].Value = x[i].ToString("F6");
                    AddToLog($"   x{i + 1} = {x[i]:F6}");
                }

                AddToLog($"\n⏱ Время выполнения: {sw.ElapsedMilliseconds} мс");
                AddToLog("─────────────────────────────────────────────────────────────────────────────");
                SetStatus("✓ СЛАУ решена успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"✗ Ошибка решения СЛАУ: {ex.Message}", true);
                AddToLog($"❌ Ошибка: {ex.Message}");
            }
        }

        // ==================== ОБРАТНАЯ МАТРИЦА ====================
        private void BtnResizeInv_Click(object sender, EventArgs e)
        {
            int size = (int)nudInvSize.Value;

            dgvInverse_A.RowCount = size;
            dgvInverse_A.ColumnCount = size;
            dgvInverse_Result.RowCount = size;
            dgvInverse_Result.ColumnCount = size;

            // Инициализируем пустые ячейки нулями
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (dgvInverse_A.Rows[i].Cells[j].Value == null)
                        dgvInverse_A.Rows[i].Cells[j].Value = "0";

            AddToLog($"📏 Размер матрицы для обращения изменён на {size}×{size}");
        }

        private void BtnInv_Random_Click(object sender, EventArgs e)
        {
            int size = (int)nudInvSize.Value;
            Random rnd = new Random();
            BtnResizeInv_Click(null, null);
            AddToLog($"🎲 Генерация случайной матрицы {size}×{size}...");
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dgvInverse_A.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
            SetStatus("✓ Случайная матрица сгенерирована");
        }

        private void BtnInv_Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt";
                ofd.Title = "Загрузить матрицу для обращения";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName);
                        int size = lines.Length;

                        // ВАЖНО: Сначала устанавливаем размер!
                        nudInvSize.Value = size;

                        // Устанавливаем размеры DataGridView
                        dgvInverse_A.RowCount = size;
                        dgvInverse_A.ColumnCount = size;
                        dgvInverse_Result.RowCount = size;
                        dgvInverse_Result.ColumnCount = size;

                        AddToLog($"📂 Загрузка матрицы из файла '{Path.GetFileName(ofd.FileName)}'...");

                        for (int i = 0; i < size; i++)
                        {
                            string[] vals = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < size && j < vals.Length; j++)
                            {
                                if (double.TryParse(vals[j], out double value))
                                    dgvInverse_A.Rows[i].Cells[j].Value = value;
                                else
                                    dgvInverse_A.Rows[i].Cells[j].Value = 0;
                            }
                        }

                        // Очищаем результат
                        for (int i = 0; i < size; i++)
                            for (int j = 0; j < size; j++)
                                dgvInverse_Result.Rows[i].Cells[j].Value = "";

                        txtCheckResult.Text = "";

                        AddToLog($"✅ Матрица {size}×{size} загружена из файла");
                        SetStatus($"✓ Матрица загружена из файла (размер {size}×{size})");
                    }
                    catch (Exception ex)
                    {
                        SetStatus($"✗ Ошибка загрузки: {ex.Message}", true);
                        AddToLog($"❌ Ошибка: {ex.Message}");
                    }
                }
            }
        }

        private void BtnComputeInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;

                // ПРОВЕРКА: убеждаемся, что DataGridView имеет правильный размер
                if (dgvInverse_A.RowCount != size || dgvInverse_A.ColumnCount != size)
                {
                    BtnResizeInv_Click(null, null);
                }

                AddToLog($"\n🔄 Вычисление обратной матрицы (размер {size}×{size})...");

                double[,] data = new double[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var cellValue = dgvInverse_A.Rows[i].Cells[j].Value;
                        data[i, j] = (cellValue != null && double.TryParse(cellValue.ToString(), out double val)) ? val : 0;
                    }
                }

                // Выводим матрицу в лог
                AddToLog("📋 Исходная матрица A:");
                for (int i = 0; i < size; i++)
                {
                    string row = "   ";
                    for (int j = 0; j < size; j++)
                        row += $"{data[i, j],10:F3} ";
                    AddToLog(row);
                }

                var sw = Stopwatch.StartNew();
                var inv = GaussianElimination.InverseGaussJordan(new Matrix(data));
                sw.Stop();

                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        dgvInverse_Result.Rows[i].Cells[j].Value = inv[i, j].ToString("F6");

                AddToLog("\n✅ РЕЗУЛЬТАТ - ОБРАТНАЯ МАТРИЦА A⁻¹:");
                for (int i = 0; i < size; i++)
                {
                    string row = "   ";
                    for (int j = 0; j < size; j++)
                        row += $"{inv[i, j],10:F6} ";
                    AddToLog(row);
                }

                AddToLog($"⏱ Время выполнения: {sw.ElapsedMilliseconds} мс");
                AddToLog("─────────────────────────────────────────────────────────────────────────────");

                txtCheckResult.Text = "✓ Обратная матрица вычислена. Нажмите 'Проверить'.";
                SetStatus("✓ Обратная матрица вычислена");
            }
            catch (Exception ex)
            {
                txtCheckResult.Text = $"✗ Ошибка: {ex.Message}";
                SetStatus($"✗ Ошибка вычисления обратной матрицы", true);
                AddToLog($"❌ Ошибка: {ex.Message}");
            }
        }

        private void BtnCheckInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;
                AddToLog($"\n🔍 Проверка обратной матрицы A × A⁻¹ = I...");
                double[,] orig = new double[size, size];
                double[,] inv = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        orig[i, j] = Convert.ToDouble(dgvInverse_A.Rows[i].Cells[j].Value);
                        inv[i, j] = Convert.ToDouble(dgvInverse_Result.Rows[i].Cells[j].Value);
                    }
                var product = Matrix.Multiply(new Matrix(orig), new Matrix(inv));
                double maxErr = 0;
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        double expected = (i == j) ? 1 : 0;
                        maxErr = Math.Max(maxErr, Math.Abs(product[i, j] - expected));
                    }
                if (maxErr < 1e-6)
                {
                    txtCheckResult.Text = $"✓ ПРОВЕРКА ПРОЙДЕНА! Погрешность: {maxErr:E6}";
                    AddToLog($"✅ Проверка пройдена! Погрешность: {maxErr:E6}");
                }
                else
                {
                    txtCheckResult.Text = $"✗ ПРОВЕРКА НЕ ПРОЙДЕНА! Погрешность: {maxErr:E6}";
                    AddToLog($"❌ Проверка не пройдена! Погрешность: {maxErr:E6}");
                }
            }
            catch (Exception ex) { txtCheckResult.Text = $"✗ Ошибка: {ex.Message}"; }
        }
    }
}