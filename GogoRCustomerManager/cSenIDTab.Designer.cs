namespace GogoRCustomerManager
{
    partial class cSenIDTab
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
            this.cSenIDData = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cSenIDData)).BeginInit();
            this.SuspendLayout();
            // 
            // cSenIDData
            // 
            this.cSenIDData.AllowUserToAddRows = false;
            this.cSenIDData.AllowUserToDeleteRows = false;
            this.cSenIDData.AllowUserToResizeColumns = false;
            this.cSenIDData.AllowUserToResizeRows = false;
            this.cSenIDData.BackgroundColor = System.Drawing.Color.White;
            this.cSenIDData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cSenIDData.Location = new System.Drawing.Point(5, 5);
            this.cSenIDData.MultiSelect = false;
            this.cSenIDData.Name = "cSenIDData";
            this.cSenIDData.ReadOnly = true;
            this.cSenIDData.RowTemplate.Height = 23;
            this.cSenIDData.Size = new System.Drawing.Size(168, 242);
            this.cSenIDData.TabIndex = 0;
            this.cSenIDData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cSenIDData_CellClick);
            this.cSenIDData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cSenIDData_CellContentDoubleClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(5, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 57);
            this.button1.TabIndex = 1;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(93, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 57);
            this.button2.TabIndex = 2;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cSenIDTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 315);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cSenIDData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "cSenIDTab";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cSenIDTab";
            this.Activated += new System.EventHandler(this.cSenIDTab_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.cSenIDData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView cSenIDData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}