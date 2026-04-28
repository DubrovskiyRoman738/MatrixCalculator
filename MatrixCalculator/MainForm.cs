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

            // Кнопки операций для матрицы A
            btnAdd.Click += BtnAdd_Click;
            btnSubtract.Click += BtnSubtract_Click;
            btnMultiply.Click += BtnMultiply_Click;
            btnMultiplyScalar.Click += BtnMultiplyScalar_Click;
            btnTranspose.Click += BtnTranspose_Click;
            btnDeterminant.Click += BtnDeterminant_Click;
            btnInverse.Click += BtnInverse_Click;

            // Кнопки операций для матрицы B
            btnB_MultiplyScalar.Click += BtnB_MultiplyScalar_Click;
            btnB_Transpose.Click += BtnB_Transpose_Click;
            btnB_Determinant.Click += BtnB_Determinant_Click;
            btnB_Inverse.Click += BtnB_Inverse_Click;

            // Кнопки загрузки/сохранения
            btnLoadA.Click += BtnLoadA_Click;
            btnLoadB.Click += BtnLoadB_Click;
            btnRandomA.Click += BtnRandomA_Click;
            btnRandomB.Click += BtnRandomB_Click;
            btnSaveResult.Click += BtnSaveResult_Click;

            // Кнопки СЛАУ
            btnResizeSLAU.Click += BtnResizeSLAU_Click;
            btnSolveSLAU.Click += BtnSolveSLAU_Click;
            btnSLAU_Random.Click += BtnSLAU_Random_Click;
            btnSLAU_Load.Click += BtnSLAU_Load_Click;

            // Кнопки обратной матрицы
            btnResizeInv.Click += BtnResizeInv_Click;
            btnComputeInverse.Click += BtnComputeInverse_Click;
            btnCheckInverse.Click += BtnCheckInverse_Click;
            btnInv_Random.Click += BtnInv_Random_Click;
            btnInv_Load.Click += BtnInv_Load_Click;
        }

        private void InitializeDefaultMatrices()
        {
            nudRowsA.Value = 3;
            nudColsA.Value = 3;
            nudRowsB.Value = 3;
            nudColsB.Value = 3;

            BtnResizeA_Click(null, null);
            BtnResizeB_Click(null, null);

            // Матрица A
            dgvMatrixA.Rows[0].Cells[0].Value = 1;
            dgvMatrixA.Rows[0].Cells[1].Value = 2;
            dgvMatrixA.Rows[0].Cells[2].Value = 3;
            dgvMatrixA.Rows[1].Cells[0].Value = 4;
            dgvMatrixA.Rows[1].Cells[1].Value = 5;
            dgvMatrixA.Rows[1].Cells[2].Value = 6;
            dgvMatrixA.Rows[2].Cells[0].Value = 7;
            dgvMatrixA.Rows[2].Cells[1].Value = 8;
            dgvMatrixA.Rows[2].Cells[2].Value = 9;

            // Матрица B
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

        private void SetStatus(string message, bool isError = false)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
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
        }

        // ==================== ОПЕРАЦИИ A ====================
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
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
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
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                UpdateMatrixFromGrid(dgvMatrixB, ref matrixB);
                var sw = Stopwatch.StartNew();
                resultMatrix = Matrix.Multiply(matrixA, matrixB);
                sw.Stop();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Умножение выполнено за {sw.ElapsedMilliseconds} мс");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

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
                resultMatrix = Matrix.MultiplyByScalar(matrixA, scalar);
                DisplayResult(resultMatrix);
                SetStatus($"✓ Матрица A умножена на {scalar}");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnTranspose_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                resultMatrix = matrixA.Transpose();
                DisplayResult(resultMatrix);
                SetStatus("✓ Транспонирование A выполнено");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnDeterminant_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMatrixFromGrid(dgvMatrixA, ref matrixA);
                if (!matrixA.IsSquare())
                {
                    SetStatus("✗ Только для квадратной матрицы", true);
                    return;
                }
                double det;
                var sw = Stopwatch.StartNew();
                if (matrixA.Rows <= 10)
                    det = MatrixDeterminant.RecursiveDeterminant(matrixA);
                else
                    det = GaussianElimination.DeterminantByGaussian(matrixA);
                sw.Stop();
                txtDeterminantResult.Text = det.ToString("F6");
                SetStatus($"Определитель A = {det:F6}, время: {sw.ElapsedMilliseconds} мс");
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
                    SetStatus("✗ Только для квадратной матрицы", true);
                    return;
                }
                var sw = Stopwatch.StartNew();
                resultMatrix = GaussianElimination.InverseGaussJordan(matrixA);
                sw.Stop();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Обратная матрица A найдена за {sw.ElapsedMilliseconds} мс");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        // ==================== ОПЕРАЦИИ B ====================
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
                var sw = Stopwatch.StartNew();
                if (matrixB.Rows <= 10)
                    det = MatrixDeterminant.RecursiveDeterminant(matrixB);
                else
                    det = GaussianElimination.DeterminantByGaussian(matrixB);
                sw.Stop();
                txtBDeterminantResult.Text = det.ToString("F6");
                SetStatus($"Определитель B = {det:F6}, время: {sw.ElapsedMilliseconds} мс");
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
                var sw = Stopwatch.StartNew();
                resultMatrix = GaussianElimination.InverseGaussJordan(matrixB);
                sw.Stop();
                DisplayResult(resultMatrix);
                SetStatus($"✓ Обратная матрица B найдена за {sw.ElapsedMilliseconds} мс");
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
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
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
                        SetStatus($"✓ Матрица {name} загружена");
                    }
                    catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
                }
            }
        }

        private void BtnRandomA_Click(object sender, EventArgs e) => FillRandomMatrix(dgvMatrixA);
        private void BtnRandomB_Click(object sender, EventArgs e) => FillRandomMatrix(dgvMatrixB);

        private void FillRandomMatrix(DataGridView dgv)
        {
            Random rnd = new Random();
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                    dgv.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
            SetStatus("✓ Матрица заполнена случайными числами");
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
                        SetStatus($"✓ Сохранено в {sfd.FileName}");
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
            dgvSLAU_Result.RowCount = size;
            dgvSLAU_b.ColumnCount = 1;
            dgvSLAU_Result.ColumnCount = 1;
            dgvSLAU_b.ColumnHeadersVisible = false;
            dgvSLAU_Result.ColumnHeadersVisible = false;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    if (dgvSLAU_A.Rows[i].Cells[j].Value == null)
                        dgvSLAU_A.Rows[i].Cells[j].Value = "0";
                if (dgvSLAU_b.Rows[i].Cells[0].Value == null)
                    dgvSLAU_b.Rows[i].Cells[0].Value = "0";
                dgvSLAU_Result.Rows[i].Cells[0].Value = "";
            }
        }

        private void BtnSolveSLAU_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudSLAUSize.Value;
                double[,] a = new double[size, size];
                double[] b = new double[size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                        a[i, j] = Convert.ToDouble(dgvSLAU_A.Rows[i].Cells[j].Value);
                    b[i] = Convert.ToDouble(dgvSLAU_b.Rows[i].Cells[0].Value);
                }
                double[] x = GaussianElimination.SolveLinearSystem(new Matrix(a), b);
                for (int i = 0; i < size; i++)
                    dgvSLAU_Result.Rows[i].Cells[0].Value = x[i].ToString("F6");
                SetStatus("✓ СЛАУ решена успешно");
            }
            catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
        }

        private void BtnSLAU_Random_Click(object sender, EventArgs e)
        {
            int size = (int)nudSLAUSize.Value;
            Random rnd = new Random();
            BtnResizeSLAU_Click(null, null);
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
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName);
                        int size = lines.Length;
                        nudSLAUSize.Value = size;
                        BtnResizeSLAU_Click(null, null);
                        for (int i = 0; i < size; i++)
                        {
                            var parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < size && j < parts.Length; j++)
                                dgvSLAU_A.Rows[i].Cells[j].Value = double.Parse(parts[j]);
                        }
                        SetStatus("✓ Система загружена");
                    }
                    catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
                }
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
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (dgvInverse_A.Rows[i].Cells[j].Value == null)
                        dgvInverse_A.Rows[i].Cells[j].Value = "0";
        }

        private void BtnComputeInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;
                double[,] data = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        data[i, j] = Convert.ToDouble(dgvInverse_A.Rows[i].Cells[j].Value);
                var inv = GaussianElimination.InverseGaussJordan(new Matrix(data));
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        dgvInverse_Result.Rows[i].Cells[j].Value = inv[i, j].ToString("F6");
                txtCheckResult.Text = "✓ Обратная матрица вычислена. Нажмите 'Проверить'.";
                SetStatus("✓ Обратная матрица вычислена");
            }
            catch (Exception ex)
            {
                txtCheckResult.Text = $"✗ Ошибка: {ex.Message}";
                SetStatus($"✗ Ошибка", true);
            }
        }

        private void BtnCheckInverse_Click(object sender, EventArgs e)
        {
            try
            {
                int size = (int)nudInvSize.Value;
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
                    txtCheckResult.Text = $"✓ ПРОВЕРКА ПРОЙДЕНА! Погрешность: {maxErr:E6}";
                else
                    txtCheckResult.Text = $"✗ ПРОВЕРКА НЕ ПРОЙДЕНА! Погрешность: {maxErr:E6}";
            }
            catch (Exception ex) { txtCheckResult.Text = $"✗ Ошибка: {ex.Message}"; }
        }

        private void BtnInv_Random_Click(object sender, EventArgs e)
        {
            int size = (int)nudInvSize.Value;
            Random rnd = new Random();
            BtnResizeInv_Click(null, null);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dgvInverse_A.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dgvInverse_Result.Rows[i].Cells[j].Value = "";
            txtCheckResult.Text = "";
            SetStatus("✓ Случайная матрица сгенерирована");
        }

        private void BtnInv_Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName);
                        int size = lines.Length;
                        nudInvSize.Value = size;
                        BtnResizeInv_Click(null, null);
                        for (int i = 0; i < size; i++)
                        {
                            var vals = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < size && j < vals.Length; j++)
                                dgvInverse_A.Rows[i].Cells[j].Value = double.Parse(vals[j]);
                        }
                        SetStatus("✓ Матрица загружена");
                    }
                    catch (Exception ex) { SetStatus($"✗ Ошибка: {ex.Message}", true); }
                }
            }
        }
    }
}