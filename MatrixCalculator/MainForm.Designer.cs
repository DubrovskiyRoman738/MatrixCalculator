namespace MatrixCalculator.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabPageOperations = new TabPage();
            grpMatrixA = new GroupBox();
            dgvMatrixA = new DataGridView();
            nudRowsA = new NumericUpDown();
            nudColsA = new NumericUpDown();
            btnResizeA = new Button();
            grpMatrixB = new GroupBox();
            dgvMatrixB = new DataGridView();
            nudRowsB = new NumericUpDown();
            nudColsB = new NumericUpDown();
            btnResizeB = new Button();
            grpResult = new GroupBox();
            dgvResult = new DataGridView();
            btnAdd = new Button();
            btnSubtract = new Button();
            btnMultiply = new Button();
            btnMultiplyScalar = new Button();
            btnTranspose = new Button();
            btnDeterminant = new Button();
            btnInverse = new Button();
            txtScalar = new TextBox();
            lblScalar = new Label();
            txtDeterminantResult = new TextBox();
            lblDet = new Label();
            btnLoadA = new Button();
            btnLoadB = new Button();
            btnRandomA = new Button();
            btnRandomB = new Button();
            btnSaveResult = new Button();
            lblStatus = new Label();
            tabPageSLAU = new TabPage();
            grpSLAU = new GroupBox();
            lblSLAU_Matrix = new Label();
            nudSLAUSize = new NumericUpDown();
            btnResizeSLAU = new Button();
            dgvSLAU_A = new DataGridView();
            lblSLAU_b = new Label();
            dgvSLAU_b = new DataGridView();
            btnSolveSLAU = new Button();
            lblSLAU_Result = new Label();
            dgvSLAU_Result = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            tabPageInverse = new TabPage();
            grpInverse = new GroupBox();
            lblInvSize = new Label();
            nudInvSize = new NumericUpDown();
            btnResizeInv = new Button();
            dgvInverse_A = new DataGridView();
            btnComputeInverse = new Button();
            btnCheckInverse = new Button();
            lblInvResult = new Label();
            dgvInverse_Result = new DataGridView();
            txtCheckResult = new TextBox();
            tabControl.SuspendLayout();
            tabPageOperations.SuspendLayout();
            grpMatrixA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrixA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRowsA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudColsA).BeginInit();
            grpMatrixB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrixB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRowsB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudColsB).BeginInit();
            grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResult).BeginInit();
            tabPageSLAU.SuspendLayout();
            grpSLAU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSLAUSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_A).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_b).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_Result).BeginInit();
            tabPageInverse.SuspendLayout();
            grpInverse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudInvSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInverse_A).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInverse_Result).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageOperations);
            tabControl.Controls.Add(tabPageSLAU);
            tabControl.Controls.Add(tabPageInverse);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1400, 750);
            tabControl.TabIndex = 0;
            // 
            // tabPageOperations
            // 
            tabPageOperations.Controls.Add(grpMatrixA);
            tabPageOperations.Controls.Add(grpMatrixB);
            tabPageOperations.Controls.Add(grpResult);
            tabPageOperations.Controls.Add(btnAdd);
            tabPageOperations.Controls.Add(btnSubtract);
            tabPageOperations.Controls.Add(btnMultiply);
            tabPageOperations.Controls.Add(btnMultiplyScalar);
            tabPageOperations.Controls.Add(btnTranspose);
            tabPageOperations.Controls.Add(btnDeterminant);
            tabPageOperations.Controls.Add(btnInverse);
            tabPageOperations.Controls.Add(txtScalar);
            tabPageOperations.Controls.Add(lblScalar);
            tabPageOperations.Controls.Add(txtDeterminantResult);
            tabPageOperations.Controls.Add(lblDet);
            tabPageOperations.Controls.Add(btnLoadA);
            tabPageOperations.Controls.Add(btnLoadB);
            tabPageOperations.Controls.Add(btnRandomA);
            tabPageOperations.Controls.Add(btnRandomB);
            tabPageOperations.Controls.Add(btnSaveResult);
            tabPageOperations.Controls.Add(lblStatus);
            tabPageOperations.Font = new Font("Segoe UI", 10F);
            tabPageOperations.Location = new Point(4, 32);
            tabPageOperations.Name = "tabPageOperations";
            tabPageOperations.Padding = new Padding(5);
            tabPageOperations.Size = new Size(1392, 714);
            tabPageOperations.TabIndex = 0;
            tabPageOperations.Text = "📊 Матричные операции";
            tabPageOperations.UseVisualStyleBackColor = true;
            // 
            // grpMatrixA
            // 
            grpMatrixA.Controls.Add(dgvMatrixA);
            grpMatrixA.Controls.Add(nudRowsA);
            grpMatrixA.Controls.Add(nudColsA);
            grpMatrixA.Controls.Add(btnResizeA);
            grpMatrixA.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpMatrixA.Location = new Point(10, 10);
            grpMatrixA.Name = "grpMatrixA";
            grpMatrixA.Size = new Size(440, 380);
            grpMatrixA.TabIndex = 0;
            grpMatrixA.TabStop = false;
            grpMatrixA.Text = "Матрица A";
            // 
            // dgvMatrixA
            // 
            dgvMatrixA.AllowUserToAddRows = false;
            dgvMatrixA.AllowUserToDeleteRows = false;
            dgvMatrixA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrixA.Location = new Point(10, 80);
            dgvMatrixA.Name = "dgvMatrixA";
            dgvMatrixA.RowHeadersWidth = 45;
            dgvMatrixA.Size = new Size(420, 290);
            dgvMatrixA.TabIndex = 0;
            // 
            // nudRowsA
            // 
            nudRowsA.Location = new Point(10, 40);
            nudRowsA.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRowsA.Name = "nudRowsA";
            nudRowsA.Size = new Size(70, 30);
            nudRowsA.TabIndex = 1;
            nudRowsA.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // nudColsA
            // 
            nudColsA.Location = new Point(100, 40);
            nudColsA.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudColsA.Name = "nudColsA";
            nudColsA.Size = new Size(70, 30);
            nudColsA.TabIndex = 2;
            nudColsA.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnResizeA
            // 
            btnResizeA.Location = new Point(190, 38);
            btnResizeA.Name = "btnResizeA";
            btnResizeA.Size = new Size(100, 36);
            btnResizeA.TabIndex = 3;
            btnResizeA.Text = "Изменить";
            btnResizeA.UseVisualStyleBackColor = true;
            // 
            // grpMatrixB
            // 
            grpMatrixB.Controls.Add(dgvMatrixB);
            grpMatrixB.Controls.Add(nudRowsB);
            grpMatrixB.Controls.Add(nudColsB);
            grpMatrixB.Controls.Add(btnResizeB);
            grpMatrixB.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpMatrixB.Location = new Point(470, 10);
            grpMatrixB.Name = "grpMatrixB";
            grpMatrixB.Size = new Size(440, 380);
            grpMatrixB.TabIndex = 1;
            grpMatrixB.TabStop = false;
            grpMatrixB.Text = "Матрица B";
            // 
            // dgvMatrixB
            // 
            dgvMatrixB.AllowUserToAddRows = false;
            dgvMatrixB.AllowUserToDeleteRows = false;
            dgvMatrixB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrixB.Location = new Point(10, 80);
            dgvMatrixB.Name = "dgvMatrixB";
            dgvMatrixB.RowHeadersWidth = 45;
            dgvMatrixB.Size = new Size(420, 290);
            dgvMatrixB.TabIndex = 0;
            // 
            // nudRowsB
            // 
            nudRowsB.Location = new Point(10, 40);
            nudRowsB.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRowsB.Name = "nudRowsB";
            nudRowsB.Size = new Size(70, 30);
            nudRowsB.TabIndex = 1;
            nudRowsB.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // nudColsB
            // 
            nudColsB.Location = new Point(100, 40);
            nudColsB.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudColsB.Name = "nudColsB";
            nudColsB.Size = new Size(70, 30);
            nudColsB.TabIndex = 2;
            nudColsB.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnResizeB
            // 
            btnResizeB.Location = new Point(190, 38);
            btnResizeB.Name = "btnResizeB";
            btnResizeB.Size = new Size(100, 36);
            btnResizeB.TabIndex = 3;
            btnResizeB.Text = "Изменить";
            btnResizeB.UseVisualStyleBackColor = true;
            // 
            // grpResult
            // 
            grpResult.Controls.Add(dgvResult);
            grpResult.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpResult.Location = new Point(930, 10);
            grpResult.Name = "grpResult";
            grpResult.Size = new Size(450, 380);
            grpResult.TabIndex = 2;
            grpResult.TabStop = false;
            grpResult.Text = "Результат";
            // 
            // dgvResult
            // 
            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResult.Location = new Point(10, 35);
            dgvResult.Name = "dgvResult";
            dgvResult.ReadOnly = true;
            dgvResult.RowHeadersWidth = 45;
            dgvResult.Size = new Size(430, 335);
            dgvResult.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(10, 410);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 45);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "➕ A + B";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnSubtract
            // 
            btnSubtract.Location = new Point(145, 410);
            btnSubtract.Name = "btnSubtract";
            btnSubtract.Size = new Size(120, 45);
            btnSubtract.TabIndex = 4;
            btnSubtract.Text = "➖ A - B";
            btnSubtract.UseVisualStyleBackColor = true;
            // 
            // btnMultiply
            // 
            btnMultiply.Location = new Point(280, 410);
            btnMultiply.Name = "btnMultiply";
            btnMultiply.Size = new Size(120, 45);
            btnMultiply.TabIndex = 5;
            btnMultiply.Text = "✖️ A × B";
            btnMultiply.UseVisualStyleBackColor = true;
            // 
            // btnMultiplyScalar
            // 
            btnMultiplyScalar.Location = new Point(415, 410);
            btnMultiplyScalar.Name = "btnMultiplyScalar";
            btnMultiplyScalar.Size = new Size(130, 45);
            btnMultiplyScalar.TabIndex = 6;
            btnMultiplyScalar.Text = "🔢 A × число";
            btnMultiplyScalar.UseVisualStyleBackColor = true;
            // 
            // btnTranspose
            // 
            btnTranspose.Location = new Point(560, 410);
            btnTranspose.Name = "btnTranspose";
            btnTranspose.Size = new Size(140, 45);
            btnTranspose.TabIndex = 7;
            btnTranspose.Text = "🔄 Транспонировать A";
            btnTranspose.UseVisualStyleBackColor = true;
            // 
            // btnDeterminant
            // 
            btnDeterminant.Location = new Point(715, 410);
            btnDeterminant.Name = "btnDeterminant";
            btnDeterminant.Size = new Size(140, 45);
            btnDeterminant.TabIndex = 8;
            btnDeterminant.Text = "📐 Определитель A";
            btnDeterminant.UseVisualStyleBackColor = true;
            // 
            // btnInverse
            // 
            btnInverse.Location = new Point(870, 410);
            btnInverse.Name = "btnInverse";
            btnInverse.Size = new Size(130, 45);
            btnInverse.TabIndex = 9;
            btnInverse.Text = "🔁 Обратная A⁻¹";
            btnInverse.UseVisualStyleBackColor = true;
            // 
            // txtScalar
            // 
            txtScalar.Location = new Point(479, 467);
            txtScalar.Name = "txtScalar";
            txtScalar.Size = new Size(80, 30);
            txtScalar.TabIndex = 11;
            txtScalar.Text = "2";
            // 
            // lblScalar
            // 
            lblScalar.AutoSize = true;
            lblScalar.Location = new Point(415, 470);
            lblScalar.Name = "lblScalar";
            lblScalar.Size = new Size(70, 23);
            lblScalar.TabIndex = 10;
            lblScalar.Text = "Скаляр:";
            // 
            // txtDeterminantResult
            // 
            txtDeterminantResult.Location = new Point(818, 467);
            txtDeterminantResult.Name = "txtDeterminantResult";
            txtDeterminantResult.ReadOnly = true;
            txtDeterminantResult.Size = new Size(182, 30);
            txtDeterminantResult.TabIndex = 13;
            // 
            // lblDet
            // 
            lblDet.AutoSize = true;
            lblDet.Location = new Point(715, 470);
            lblDet.Name = "lblDet";
            lblDet.Size = new Size(127, 23);
            lblDet.TabIndex = 12;
            lblDet.Text = "Определитель:";
            // 
            // btnLoadA
            // 
            btnLoadA.Location = new Point(10, 510);
            btnLoadA.Name = "btnLoadA";
            btnLoadA.Size = new Size(140, 35);
            btnLoadA.TabIndex = 14;
            btnLoadA.Text = "📂 Загрузить A";
            btnLoadA.UseVisualStyleBackColor = true;
            // 
            // btnLoadB
            // 
            btnLoadB.Location = new Point(160, 510);
            btnLoadB.Name = "btnLoadB";
            btnLoadB.Size = new Size(140, 35);
            btnLoadB.TabIndex = 15;
            btnLoadB.Text = "📂 Загрузить B";
            btnLoadB.UseVisualStyleBackColor = true;
            // 
            // btnRandomA
            // 
            btnRandomA.Location = new Point(10, 555);
            btnRandomA.Name = "btnRandomA";
            btnRandomA.Size = new Size(140, 35);
            btnRandomA.TabIndex = 16;
            btnRandomA.Text = "🎲 Случайная A";
            btnRandomA.UseVisualStyleBackColor = true;
            // 
            // btnRandomB
            // 
            btnRandomB.Location = new Point(160, 555);
            btnRandomB.Name = "btnRandomB";
            btnRandomB.Size = new Size(140, 35);
            btnRandomB.TabIndex = 17;
            btnRandomB.Text = "🎲 Случайная B";
            btnRandomB.UseVisualStyleBackColor = true;
            // 
            // btnSaveResult
            // 
            btnSaveResult.Location = new Point(10, 600);
            btnSaveResult.Name = "btnSaveResult";
            btnSaveResult.Size = new Size(200, 35);
            btnSaveResult.TabIndex = 18;
            btnSaveResult.Text = "💾 Сохранить результат";
            btnSaveResult.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.ForeColor = Color.Green;
            lblStatus.Location = new Point(10, 650);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1370, 50);
            lblStatus.TabIndex = 19;
            lblStatus.Text = "✅ Готов";
            // 
            // tabPageSLAU
            // 
            tabPageSLAU.Controls.Add(grpSLAU);
            tabPageSLAU.Location = new Point(4, 32);
            tabPageSLAU.Name = "tabPageSLAU";
            tabPageSLAU.Padding = new Padding(5);
            tabPageSLAU.Size = new Size(1392, 714);
            tabPageSLAU.TabIndex = 1;
            tabPageSLAU.Text = "📐 Решение СЛАУ";
            tabPageSLAU.UseVisualStyleBackColor = true;
            // 
            // grpSLAU
            // 
            grpSLAU.Controls.Add(lblSLAU_Matrix);
            grpSLAU.Controls.Add(nudSLAUSize);
            grpSLAU.Controls.Add(btnResizeSLAU);
            grpSLAU.Controls.Add(dgvSLAU_A);
            grpSLAU.Controls.Add(lblSLAU_b);
            grpSLAU.Controls.Add(dgvSLAU_b);
            grpSLAU.Controls.Add(btnSolveSLAU);
            grpSLAU.Controls.Add(lblSLAU_Result);
            grpSLAU.Controls.Add(dgvSLAU_Result);
            grpSLAU.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpSLAU.Location = new Point(10, 10);
            grpSLAU.Name = "grpSLAU";
            grpSLAU.Size = new Size(1370, 690);
            grpSLAU.TabIndex = 0;
            grpSLAU.TabStop = false;
            grpSLAU.Text = "Система линейных уравнений A·x = b";
            // 
            // lblSLAU_Matrix
            // 
            lblSLAU_Matrix.AutoSize = true;
            lblSLAU_Matrix.Font = new Font("Segoe UI", 9F);
            lblSLAU_Matrix.Location = new Point(10, 35);
            lblSLAU_Matrix.Name = "lblSLAU_Matrix";
            lblSLAU_Matrix.Size = new Size(181, 20);
            lblSLAU_Matrix.TabIndex = 0;
            lblSLAU_Matrix.Text = "Матрица A (квадратная):";
            // 
            // nudSLAUSize
            // 
            nudSLAUSize.Location = new Point(212, 32);
            nudSLAUSize.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudSLAUSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSLAUSize.Name = "nudSLAUSize";
            nudSLAUSize.Size = new Size(70, 30);
            nudSLAUSize.TabIndex = 1;
            nudSLAUSize.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnResizeSLAU
            // 
            btnResizeSLAU.Location = new Point(292, 30);
            btnResizeSLAU.Name = "btnResizeSLAU";
            btnResizeSLAU.Size = new Size(120, 34);
            btnResizeSLAU.TabIndex = 2;
            btnResizeSLAU.Text = "Изменить размер";
            btnResizeSLAU.UseVisualStyleBackColor = true;
            // 
            // dgvSLAU_A
            // 
            dgvSLAU_A.AllowUserToAddRows = false;
            dgvSLAU_A.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSLAU_A.Location = new Point(10, 70);
            dgvSLAU_A.Name = "dgvSLAU_A";
            dgvSLAU_A.RowHeadersWidth = 45;
            dgvSLAU_A.Size = new Size(600, 450);
            dgvSLAU_A.TabIndex = 3;
            // 
            // lblSLAU_b
            // 
            lblSLAU_b.AutoSize = true;
            lblSLAU_b.Font = new Font("Segoe UI", 9F);
            lblSLAU_b.Location = new Point(630, 70);
            lblSLAU_b.Name = "lblSLAU_b";
            lblSLAU_b.Size = new Size(73, 20);
            lblSLAU_b.TabIndex = 4;
            lblSLAU_b.Text = "Вектор b:";
            // 
            // dgvSLAU_b
            // 
            dgvSLAU_b.AllowUserToAddRows = false;
            dgvSLAU_b.ColumnHeadersHeight = 29;
            dgvSLAU_b.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1 });
            dgvSLAU_b.Location = new Point(630, 100);
            dgvSLAU_b.Name = "dgvSLAU_b";
            dgvSLAU_b.RowHeadersWidth = 45;
            dgvSLAU_b.Size = new Size(103, 420);
            dgvSLAU_b.TabIndex = 5;
            // 
            // btnSolveSLAU
            // 
            btnSolveSLAU.Font = new Font("Segoe UI", 11F);
            btnSolveSLAU.Location = new Point(760, 100);
            btnSolveSLAU.Name = "btnSolveSLAU";
            btnSolveSLAU.Size = new Size(180, 60);
            btnSolveSLAU.TabIndex = 6;
            btnSolveSLAU.Text = "\U0001f9ee Решить систему";
            btnSolveSLAU.UseVisualStyleBackColor = true;
            // 
            // lblSLAU_Result
            // 
            lblSLAU_Result.AutoSize = true;
            lblSLAU_Result.Font = new Font("Segoe UI", 9F);
            lblSLAU_Result.Location = new Point(760, 180);
            lblSLAU_Result.Name = "lblSLAU_Result";
            lblSLAU_Result.Size = new Size(85, 20);
            lblSLAU_Result.TabIndex = 7;
            lblSLAU_Result.Text = "Решение x:";
            // 
            // dgvSLAU_Result
            // 
            dgvSLAU_Result.AllowUserToAddRows = false;
            dgvSLAU_Result.ColumnHeadersHeight = 29;
            dgvSLAU_Result.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn2 });
            dgvSLAU_Result.Location = new Point(760, 210);
            dgvSLAU_Result.Name = "dgvSLAU_Result";
            dgvSLAU_Result.ReadOnly = true;
            dgvSLAU_Result.RowHeadersWidth = 45;
            dgvSLAU_Result.Size = new Size(180, 310);
            dgvSLAU_Result.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // tabPageInverse
            // 
            tabPageInverse.Controls.Add(grpInverse);
            tabPageInverse.Location = new Point(4, 32);
            tabPageInverse.Name = "tabPageInverse";
            tabPageInverse.Padding = new Padding(5);
            tabPageInverse.Size = new Size(1392, 714);
            tabPageInverse.TabIndex = 2;
            tabPageInverse.Text = "🔄 Обратная матрица";
            tabPageInverse.UseVisualStyleBackColor = true;
            // 
            // grpInverse
            // 
            grpInverse.Controls.Add(lblInvSize);
            grpInverse.Controls.Add(nudInvSize);
            grpInverse.Controls.Add(btnResizeInv);
            grpInverse.Controls.Add(dgvInverse_A);
            grpInverse.Controls.Add(btnComputeInverse);
            grpInverse.Controls.Add(btnCheckInverse);
            grpInverse.Controls.Add(lblInvResult);
            grpInverse.Controls.Add(dgvInverse_Result);
            grpInverse.Controls.Add(txtCheckResult);
            grpInverse.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpInverse.Location = new Point(10, 10);
            grpInverse.Name = "grpInverse";
            grpInverse.Size = new Size(1370, 690);
            grpInverse.TabIndex = 0;
            grpInverse.TabStop = false;
            grpInverse.Text = "Вычисление обратной матрицы";
            // 
            // lblInvSize
            // 
            lblInvSize.AutoSize = true;
            lblInvSize.Font = new Font("Segoe UI", 9F);
            lblInvSize.Location = new Point(10, 35);
            lblInvSize.Name = "lblInvSize";
            lblInvSize.Size = new Size(223, 20);
            lblInvSize.TabIndex = 0;
            lblInvSize.Text = "Размер матрицы (квадратная):";
            // 
            // nudInvSize
            // 
            nudInvSize.Location = new Point(210, 32);
            nudInvSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudInvSize.Name = "nudInvSize";
            nudInvSize.Size = new Size(70, 30);
            nudInvSize.TabIndex = 1;
            nudInvSize.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnResizeInv
            // 
            btnResizeInv.Location = new Point(290, 30);
            btnResizeInv.Name = "btnResizeInv";
            btnResizeInv.Size = new Size(100, 34);
            btnResizeInv.TabIndex = 2;
            btnResizeInv.Text = "Изменить";
            btnResizeInv.UseVisualStyleBackColor = true;
            // 
            // dgvInverse_A
            // 
            dgvInverse_A.AllowUserToAddRows = false;
            dgvInverse_A.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInverse_A.Location = new Point(10, 70);
            dgvInverse_A.Name = "dgvInverse_A";
            dgvInverse_A.RowHeadersWidth = 45;
            dgvInverse_A.Size = new Size(500, 450);
            dgvInverse_A.TabIndex = 3;
            // 
            // btnComputeInverse
            // 
            btnComputeInverse.Font = new Font("Segoe UI", 11F);
            btnComputeInverse.Location = new Point(530, 70);
            btnComputeInverse.Name = "btnComputeInverse";
            btnComputeInverse.Size = new Size(220, 60);
            btnComputeInverse.TabIndex = 4;
            btnComputeInverse.Text = "🔢 Вычислить обратную";
            btnComputeInverse.UseVisualStyleBackColor = true;
            // 
            // btnCheckInverse
            // 
            btnCheckInverse.Font = new Font("Segoe UI", 11F);
            btnCheckInverse.Location = new Point(530, 145);
            btnCheckInverse.Name = "btnCheckInverse";
            btnCheckInverse.Size = new Size(220, 60);
            btnCheckInverse.TabIndex = 5;
            btnCheckInverse.Text = "✅ Проверить A × A⁻¹ = I";
            btnCheckInverse.UseVisualStyleBackColor = true;
            // 
            // lblInvResult
            // 
            lblInvResult.AutoSize = true;
            lblInvResult.Font = new Font("Segoe UI", 9F);
            lblInvResult.Location = new Point(530, 220);
            lblInvResult.Name = "lblInvResult";
            lblInvResult.Size = new Size(169, 20);
            lblInvResult.TabIndex = 6;
            lblInvResult.Text = "Обратная матрица A⁻¹:";
            // 
            // dgvInverse_Result
            // 
            dgvInverse_Result.AllowUserToAddRows = false;
            dgvInverse_Result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInverse_Result.Location = new Point(530, 250);
            dgvInverse_Result.Name = "dgvInverse_Result";
            dgvInverse_Result.ReadOnly = true;
            dgvInverse_Result.RowHeadersWidth = 45;
            dgvInverse_Result.Size = new Size(500, 270);
            dgvInverse_Result.TabIndex = 7;
            // 
            // txtCheckResult
            // 
            txtCheckResult.Font = new Font("Consolas", 10F);
            txtCheckResult.Location = new Point(530, 530);
            txtCheckResult.Multiline = true;
            txtCheckResult.Name = "txtCheckResult";
            txtCheckResult.ReadOnly = true;
            txtCheckResult.Size = new Size(800, 100);
            txtCheckResult.TabIndex = 8;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 750);
            Controls.Add(tabControl);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f9ee Калькулятор матричных операций";
            tabControl.ResumeLayout(false);
            tabPageOperations.ResumeLayout(false);
            tabPageOperations.PerformLayout();
            grpMatrixA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatrixA).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRowsA).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudColsA).EndInit();
            grpMatrixB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatrixB).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRowsB).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudColsB).EndInit();
            grpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResult).EndInit();
            tabPageSLAU.ResumeLayout(false);
            grpSLAU.ResumeLayout(false);
            grpSLAU.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSLAUSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_A).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_b).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSLAU_Result).EndInit();
            tabPageInverse.ResumeLayout(false);
            grpInverse.ResumeLayout(false);
            grpInverse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudInvSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInverse_A).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInverse_Result).EndInit();
            ResumeLayout(false);
        }

        // ================================
        // Поля формы
        // ================================
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageOperations;
        private System.Windows.Forms.TabPage tabPageSLAU;
        private System.Windows.Forms.TabPage tabPageInverse;

        // Вкладка "Матричные операции"
        private System.Windows.Forms.GroupBox grpMatrixA;
        private System.Windows.Forms.GroupBox grpMatrixB;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.DataGridView dgvMatrixA;
        private System.Windows.Forms.DataGridView dgvMatrixB;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.NumericUpDown nudRowsA;
        private System.Windows.Forms.NumericUpDown nudColsA;
        private System.Windows.Forms.NumericUpDown nudRowsB;
        private System.Windows.Forms.NumericUpDown nudColsB;
        private System.Windows.Forms.Button btnResizeA;
        private System.Windows.Forms.Button btnResizeB;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnMultiplyScalar;
        private System.Windows.Forms.Button btnTranspose;
        private System.Windows.Forms.Button btnDeterminant;
        private System.Windows.Forms.Button btnInverse;
        private System.Windows.Forms.TextBox txtScalar;
        private System.Windows.Forms.Label lblScalar;
        private System.Windows.Forms.TextBox txtDeterminantResult;
        private System.Windows.Forms.Label lblDet;
        private System.Windows.Forms.Button btnLoadA;
        private System.Windows.Forms.Button btnLoadB;
        private System.Windows.Forms.Button btnRandomA;
        private System.Windows.Forms.Button btnRandomB;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Label lblStatus;

        // Вкладка "Решение СЛАУ"
        private System.Windows.Forms.GroupBox grpSLAU;
        private System.Windows.Forms.Label lblSLAU_Matrix;
        private System.Windows.Forms.NumericUpDown nudSLAUSize;
        private System.Windows.Forms.Button btnResizeSLAU;
        private System.Windows.Forms.DataGridView dgvSLAU_A;
        private System.Windows.Forms.Label lblSLAU_b;
        private System.Windows.Forms.DataGridView dgvSLAU_b;
        private System.Windows.Forms.Button btnSolveSLAU;
        private System.Windows.Forms.Label lblSLAU_Result;
        private System.Windows.Forms.DataGridView dgvSLAU_Result;

        // Вкладка "Обратная матрица"
        private System.Windows.Forms.GroupBox grpInverse;
        private System.Windows.Forms.Label lblInvSize;
        private System.Windows.Forms.NumericUpDown nudInvSize;
        private System.Windows.Forms.Button btnResizeInv;
        private System.Windows.Forms.DataGridView dgvInverse_A;
        private System.Windows.Forms.Button btnComputeInverse;
        private System.Windows.Forms.Button btnCheckInverse;
        private System.Windows.Forms.Label lblInvResult;
        private System.Windows.Forms.DataGridView dgvInverse_Result;
        private System.Windows.Forms.TextBox txtCheckResult;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}