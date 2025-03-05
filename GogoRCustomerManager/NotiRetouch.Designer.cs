namespace GogoRCustomerManager
{
    partial class NotiRetouch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotiRetouch));
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.ProgramNotiBtn = new System.Windows.Forms.Button();
            this.UpadateBtn = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.Retouch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox.Location = new System.Drawing.Point(12, 79);
            this.TextBox.Name = "TextBox";
            this.TextBox.ReadOnly = true;
            this.TextBox.Size = new System.Drawing.Size(1046, 501);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = resources.GetString("TextBox.Text");
            this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // ProgramNotiBtn
            // 
            this.ProgramNotiBtn.BackColor = System.Drawing.Color.Indigo;
            this.ProgramNotiBtn.FlatAppearance.BorderSize = 0;
            this.ProgramNotiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgramNotiBtn.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ProgramNotiBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProgramNotiBtn.Location = new System.Drawing.Point(12, 10);
            this.ProgramNotiBtn.Name = "ProgramNotiBtn";
            this.ProgramNotiBtn.Size = new System.Drawing.Size(128, 30);
            this.ProgramNotiBtn.TabIndex = 1;
            this.ProgramNotiBtn.Text = "프로그램 알림";
            this.ProgramNotiBtn.UseVisualStyleBackColor = false;
            this.ProgramNotiBtn.Click += new System.EventHandler(this.ProgramNotiBtn_Click);
            // 
            // UpadateBtn
            // 
            this.UpadateBtn.BackColor = System.Drawing.Color.Indigo;
            this.UpadateBtn.FlatAppearance.BorderSize = 0;
            this.UpadateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpadateBtn.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UpadateBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UpadateBtn.Location = new System.Drawing.Point(12, 44);
            this.UpadateBtn.Name = "UpadateBtn";
            this.UpadateBtn.Size = new System.Drawing.Size(128, 30);
            this.UpadateBtn.TabIndex = 1;
            this.UpadateBtn.Text = "업데이트 내역";
            this.UpadateBtn.UseVisualStyleBackColor = false;
            this.UpadateBtn.Click += new System.EventHandler(this.UpadateBtn_Click);
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Apply.BackColor = System.Drawing.Color.Indigo;
            this.Apply.FlatAppearance.BorderSize = 0;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Apply.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Apply.Location = new System.Drawing.Point(959, 44);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(99, 30);
            this.Apply.TabIndex = 1;
            this.Apply.Text = "적용";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Retouch
            // 
            this.Retouch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Retouch.BackColor = System.Drawing.Color.Indigo;
            this.Retouch.FlatAppearance.BorderSize = 0;
            this.Retouch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Retouch.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Retouch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Retouch.Location = new System.Drawing.Point(854, 44);
            this.Retouch.Name = "Retouch";
            this.Retouch.Size = new System.Drawing.Size(99, 30);
            this.Retouch.TabIndex = 1;
            this.Retouch.Text = "수정하기";
            this.Retouch.UseVisualStyleBackColor = false;
            this.Retouch.Click += new System.EventHandler(this.Retouch_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(412, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 34);
            this.label1.TabIndex = 2;
            this.label1.Text = "프로그램 공지 수정";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotiRetouch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1070, 587);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpadateBtn);
            this.Controls.Add(this.Retouch);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.ProgramNotiBtn);
            this.Controls.Add(this.TextBox);
            this.Name = "NotiRetouch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "프로그램 공지 수정";
            this.Load += new System.EventHandler(this.NotiRetouch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.Button ProgramNotiBtn;
        private System.Windows.Forms.Button UpadateBtn;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Retouch;
        private System.Windows.Forms.Label label1;
    }
}