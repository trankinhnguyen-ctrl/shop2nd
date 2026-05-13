namespace dosi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button1 = new Button();
            userControl11 = new KhoHang();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 88);
            panel1.TabIndex = 0;
            panel1.Click += button1_Click;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(12, 106);
            button1.Name = "button1";
            button1.Size = new Size(138, 64);
            button1.TabIndex = 1;
            button1.Text = "Kho Hàng";
            button1.UseVisualStyleBackColor = true;
            // 
            // userControl11
            // 
            userControl11.Location = new Point(156, 106);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(632, 334);
            userControl11.TabIndex = 2;
            userControl11.Load += userControl11_Load;
            // 
            // button2
            // 
            button2.Location = new Point(12, 190);
            button2.Name = "button2";
            button2.Size = new Size(138, 64);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(12, 260);
            button3.Name = "button3";
            button3.Size = new Size(138, 64);
            button3.TabIndex = 4;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 330);
            button4.Name = "button4";
            button4.Size = new Size(138, 64);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(userControl11);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private KhoHang userControl11;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
