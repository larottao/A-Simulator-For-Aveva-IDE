namespace ASimulatorForAveva
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.mixerSimulationTimer = new System.Windows.Forms.Timer(this.components);
            this.HumanReadableRichTextBox = new System.Windows.Forms.RichTextBox();
            this.startStopButton = new System.Windows.Forms.Button();
            this.checkBoxRunSimulation = new System.Windows.Forms.CheckBox();
            this.buttonRestartMBTCP = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonRedeployGalaxy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mixerSimulationTimer
            // 
            this.mixerSimulationTimer.Interval = 1000;
            this.mixerSimulationTimer.Tick += new System.EventHandler(this.mixerSimulationTimer_Tick);
            // 
            // HumanReadableRichTextBox
            // 
            this.HumanReadableRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HumanReadableRichTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HumanReadableRichTextBox.Location = new System.Drawing.Point(14, 98);
            this.HumanReadableRichTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.HumanReadableRichTextBox.Name = "HumanReadableRichTextBox";
            this.HumanReadableRichTextBox.Size = new System.Drawing.Size(982, 215);
            this.HumanReadableRichTextBox.TabIndex = 0;
            this.HumanReadableRichTextBox.Text = "";
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(17, 33);
            this.startStopButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(186, 36);
            this.startStopButton.TabIndex = 1;
            this.startStopButton.Text = "Start Modbus Server";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // checkBoxRunSimulation
            // 
            this.checkBoxRunSimulation.AutoSize = true;
            this.checkBoxRunSimulation.Enabled = false;
            this.checkBoxRunSimulation.Location = new System.Drawing.Point(29, 40);
            this.checkBoxRunSimulation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxRunSimulation.Name = "checkBoxRunSimulation";
            this.checkBoxRunSimulation.Size = new System.Drawing.Size(140, 23);
            this.checkBoxRunSimulation.TabIndex = 2;
            this.checkBoxRunSimulation.Text = "Run Simulation";
            this.checkBoxRunSimulation.UseVisualStyleBackColor = true;
            this.checkBoxRunSimulation.CheckedChanged += new System.EventHandler(this.checkBoxRunSimulation_CheckedChanged);
            // 
            // buttonRestartMBTCP
            // 
            this.buttonRestartMBTCP.Location = new System.Drawing.Point(23, 33);
            this.buttonRestartMBTCP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRestartMBTCP.Name = "buttonRestartMBTCP";
            this.buttonRestartMBTCP.Size = new System.Drawing.Size(186, 36);
            this.buttonRestartMBTCP.TabIndex = 3;
            this.buttonRestartMBTCP.Text = "Restart MBTCP";
            this.buttonRestartMBTCP.UseVisualStyleBackColor = true;
            this.buttonRestartMBTCP.Click += new System.EventHandler(this.buttonRestartMBTCP_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checkBoxRunSimulation);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(324, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulates a little industrial process";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 33);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Describe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.startStopButton);
            this.groupBox2.Location = new System.Drawing.Point(342, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(235, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Respond to AVEVA pooling";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonRedeployGalaxy);
            this.groupBox3.Controls.Add(this.buttonRestartMBTCP);
            this.groupBox3.Location = new System.Drawing.Point(583, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(413, 82);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Useful when driver connection fails";
            // 
            // buttonRedeployGalaxy
            // 
            this.buttonRedeployGalaxy.Enabled = false;
            this.buttonRedeployGalaxy.Location = new System.Drawing.Point(217, 32);
            this.buttonRedeployGalaxy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRedeployGalaxy.Name = "buttonRedeployGalaxy";
            this.buttonRedeployGalaxy.Size = new System.Drawing.Size(186, 36);
            this.buttonRedeployGalaxy.TabIndex = 4;
            this.buttonRedeployGalaxy.Text = "Redeploy Galaxy";
            this.buttonRedeployGalaxy.UseVisualStyleBackColor = true;
            this.buttonRedeployGalaxy.Click += new System.EventHandler(this.buttonRedeployGalaxy_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 335);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.HumanReadableRichTextBox);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainForm";
            this.Text = "ASimulatorForAveva - Quick / Dirty / Buggy Proof of Concept !!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer mixerSimulationTimer;
        private System.Windows.Forms.RichTextBox HumanReadableRichTextBox;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.CheckBox checkBoxRunSimulation;
        private System.Windows.Forms.Button buttonRestartMBTCP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRedeployGalaxy;
    }
}

