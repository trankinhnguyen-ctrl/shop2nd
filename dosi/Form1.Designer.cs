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
            Menu_panel = new Panel();
            button1 = new Button();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            panelMain = new Panel();
            panel1 = new Panel();
            Menu_panel.SuspendLayout();
            SuspendLayout();
            // 
            // Menu_panel
            // 
            Menu_panel.Controls.Add(button1);
            Menu_panel.Controls.Add(button4);
            Menu_panel.Controls.Add(button2);
            Menu_panel.Controls.Add(button3);
            Menu_panel.Dock = DockStyle.Left;
            Menu_panel.Location = new Point(0, 0);
            Menu_panel.Name = "Menu_panel";
            Menu_panel.Size = new Size(153, 568);
            Menu_panel.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(0, 197);
            button1.Name = "button1";
            button1.Size = new Size(132, 64);
            button1.TabIndex = 1;
            button1.Text = "Kho Hàng";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnKhoHang_Click;
            // 
            // button4
            // 
            button4.Location = new Point(0, 337);
            button4.Name = "button4";
            button4.Size = new Size(132, 61);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Location = new Point(0, 127);
            button2.Name = "button2";
            button2.Size = new Size(132, 64);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(0, 267);
            button3.Name = "button3";
            button3.Size = new Size(132, 64);
            button3.TabIndex = 4;
            button3.Text = "Khách Hàng";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnKhachHang_Click;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(153, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(756, 568);
            panelMain.TabIndex = 6;
            panelMain.Paint += panelMain_Paint;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(153, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(756, 125);
            panel1.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(909, 568);
            Controls.Add(panel1);
            Controls.Add(panelMain);
            Controls.Add(Menu_panel);
            Name = "Form1";
            Text = "Form1";
            Menu_panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Menu_panel;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Panel panelMain;
        private Panel panel1;
    }
}
