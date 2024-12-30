namespace GogoRCustomerManager
{
    partial class ManagerCheck
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
            this.id = new System.Windows.Forms.TextBox();
            this.pw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.id.BackColor = System.Drawing.Color.LightGray;
            this.id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.id.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.id.Location = new System.Drawing.Point(55, 46);
            this.id.MaxLength = 20;
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(104, 21);
            this.id.TabIndex = 0;
            this.id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FocusPw);
            // 
            // pw
            // 
            this.pw.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pw.BackColor = System.Drawing.Color.LightGray;
            this.pw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pw.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.pw.Location = new System.Drawing.Point(55, 81);
            this.pw.MaxLength = 20;
            this.pw.Name = "pw";
            this.pw.PasswordChar = '*';
            this.pw.Size = new System.Drawing.Size(104, 21);
            this.pw.TabIndex = 1;
            this.pw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(25, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(21, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "PW";
            // 
            // LoginButton
            // 
            this.LoginButton.Font = new System.Drawing.Font("나눔고딕", 9F);
            this.LoginButton.Location = new System.Drawing.Point(54, 118);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "인증";
            this.LoginButton.UseVisualStyleBackColor = true;
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("나눔고딕", 13F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(45, 14);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(95, 21);
            this.Title.TabIndex = 2;
            this.Title.Text = "관리자인증";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 159);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pw);
            this.Controls.Add(this.id);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagerCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.TextBox pw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label Title;
    }
}