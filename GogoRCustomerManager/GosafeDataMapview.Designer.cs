namespace GogoRCustomerManager
{
    partial class GosafeDataMapview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SensorDataGrid = new System.Windows.Forms.DataGridView();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.DatePickerTitle = new System.Windows.Forms.Label();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cSenIDFindButton = new System.Windows.Forms.Button();
            this.SensorDataGridTitle = new System.Windows.Forms.Label();
            this.ServerSelectTitle = new System.Windows.Forms.Label();
            this.cSenIDTitle = new System.Windows.Forms.Label();
            this.ServerSelect = new System.Windows.Forms.ComboBox();
            this.cSenIDTextBox = new System.Windows.Forms.TextBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timeunitCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SensorDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // startTime
            // 
            this.startTime.CustomFormat = "";
            this.startTime.Location = new System.Drawing.Point(73, 43);
            this.startTime.MinDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(170, 21);
            this.startTime.TabIndex = 1;
            this.startTime.Value = new System.DateTime(2025, 1, 6, 0, 0, 0, 0);
            this.startTime.ValueChanged += new System.EventHandler(this.startTime_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.splitContainer1.Location = new System.Drawing.Point(3, 114);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SensorDataGrid);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gMapControl);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(794, 334);
            this.splitContainer1.SplitterDistance = 388;
            this.splitContainer1.TabIndex = 2;
            // 
            // SensorDataGrid
            // 
            this.SensorDataGrid.AllowUserToAddRows = false;
            this.SensorDataGrid.AllowUserToDeleteRows = false;
            this.SensorDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SensorDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SensorDataGrid.Location = new System.Drawing.Point(0, 0);
            this.SensorDataGrid.MultiSelect = false;
            this.SensorDataGrid.Name = "SensorDataGrid";
            this.SensorDataGrid.ReadOnly = true;
            this.SensorDataGrid.RowTemplate.Height = 23;
            this.SensorDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SensorDataGrid.Size = new System.Drawing.Size(385, 331);
            this.SensorDataGrid.TabIndex = 1;
            this.SensorDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SensorDataGrid_CellContentClick);
            this.SensorDataGrid.SelectionChanged += new System.EventHandler(this.SensorDataGrid_SelectionChanged);
            // 
            // gMapControl
            // 
            this.gMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemory = 5;
            this.gMapControl.Location = new System.Drawing.Point(3, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(400, 331);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 0D;
            // 
            // DatePickerTitle
            // 
            this.DatePickerTitle.AutoSize = true;
            this.DatePickerTitle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DatePickerTitle.Location = new System.Drawing.Point(12, 46);
            this.DatePickerTitle.Name = "DatePickerTitle";
            this.DatePickerTitle.Size = new System.Drawing.Size(55, 15);
            this.DatePickerTitle.TabIndex = 3;
            this.DatePickerTitle.Text = "조회일자";
            // 
            // endTime
            // 
            this.endTime.CustomFormat = "";
            this.endTime.Location = new System.Drawing.Point(270, 43);
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(170, 21);
            this.endTime.TabIndex = 4;
            this.endTime.Value = new System.DateTime(2025, 1, 6, 0, 0, 0, 0);
            this.endTime.ValueChanged += new System.EventHandler(this.endTime_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(249, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "~";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cSenIDFindButton
            // 
            this.cSenIDFindButton.Location = new System.Drawing.Point(446, 41);
            this.cSenIDFindButton.Name = "cSenIDFindButton";
            this.cSenIDFindButton.Size = new System.Drawing.Size(93, 50);
            this.cSenIDFindButton.TabIndex = 6;
            this.cSenIDFindButton.Text = "센서번호 찾기";
            this.cSenIDFindButton.UseVisualStyleBackColor = true;
            this.cSenIDFindButton.Click += new System.EventHandler(this.CSenIDFindButton_Click);
            // 
            // SensorDataGridTitle
            // 
            this.SensorDataGridTitle.AutoSize = true;
            this.SensorDataGridTitle.BackColor = System.Drawing.Color.Transparent;
            this.SensorDataGridTitle.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.SensorDataGridTitle.Location = new System.Drawing.Point(1, 102);
            this.SensorDataGridTitle.Name = "SensorDataGridTitle";
            this.SensorDataGridTitle.Size = new System.Drawing.Size(45, 12);
            this.SensorDataGridTitle.TabIndex = 7;
            this.SensorDataGridTitle.Text = "센서Data";
            // 
            // ServerSelectTitle
            // 
            this.ServerSelectTitle.AutoSize = true;
            this.ServerSelectTitle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ServerSelectTitle.Location = new System.Drawing.Point(12, 19);
            this.ServerSelectTitle.Name = "ServerSelectTitle";
            this.ServerSelectTitle.Size = new System.Drawing.Size(55, 15);
            this.ServerSelectTitle.TabIndex = 8;
            this.ServerSelectTitle.Text = "접속서버";
            this.ServerSelectTitle.Click += new System.EventHandler(this.label4_Click);
            // 
            // cSenIDTitle
            // 
            this.cSenIDTitle.AutoSize = true;
            this.cSenIDTitle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cSenIDTitle.Location = new System.Drawing.Point(16, 73);
            this.cSenIDTitle.Name = "cSenIDTitle";
            this.cSenIDTitle.Size = new System.Drawing.Size(45, 15);
            this.cSenIDTitle.TabIndex = 9;
            this.cSenIDTitle.Text = "cSenID";
            // 
            // ServerSelect
            // 
            this.ServerSelect.Enabled = false;
            this.ServerSelect.FormattingEnabled = true;
            this.ServerSelect.Location = new System.Drawing.Point(73, 17);
            this.ServerSelect.Name = "ServerSelect";
            this.ServerSelect.Size = new System.Drawing.Size(170, 20);
            this.ServerSelect.TabIndex = 10;
            this.ServerSelect.Text = "CF";
            // 
            // cSenIDTextBox
            // 
            this.cSenIDTextBox.Location = new System.Drawing.Point(73, 70);
            this.cSenIDTextBox.Name = "cSenIDTextBox";
            this.cSenIDTextBox.Size = new System.Drawing.Size(170, 21);
            this.cSenIDTextBox.TabIndex = 11;
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(545, 41);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(59, 50);
            this.SelectButton.TabIndex = 12;
            this.SelectButton.Text = "조회";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(267, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "단위";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timeunitCombo
            // 
            this.timeunitCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeunitCombo.FormattingEnabled = true;
            this.timeunitCombo.Location = new System.Drawing.Point(304, 70);
            this.timeunitCombo.Name = "timeunitCombo";
            this.timeunitCombo.Size = new System.Drawing.Size(136, 20);
            this.timeunitCombo.TabIndex = 14;
            this.timeunitCombo.SelectedIndexChanged += new System.EventHandler(this.timeunitCombo_SelectedIndexChanged);
            // 
            // GosafeDataMapview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.timeunitCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.cSenIDTextBox);
            this.Controls.Add(this.ServerSelect);
            this.Controls.Add(this.cSenIDTitle);
            this.Controls.Add(this.ServerSelectTitle);
            this.Controls.Add(this.SensorDataGridTitle);
            this.Controls.Add(this.cSenIDFindButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.DatePickerTitle);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.startTime);
            this.Name = "GosafeDataMapview";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GosafeDataMapview";
            this.Load += new System.EventHandler(this.GosafeDataMapview_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SensorDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView SensorDataGrid;
        private System.Windows.Forms.Label DatePickerTitle;
        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SensorDataGridTitle;
        private System.Windows.Forms.Button cSenIDFindButton;
        private System.Windows.Forms.Label ServerSelectTitle;
        private System.Windows.Forms.Label cSenIDTitle;
        private System.Windows.Forms.ComboBox ServerSelect;
        private System.Windows.Forms.TextBox cSenIDTextBox;
        private System.Windows.Forms.Button SelectButton;
        private GMap.NET.WindowsForms.GMapControl gMapControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox timeunitCombo;
    }
}