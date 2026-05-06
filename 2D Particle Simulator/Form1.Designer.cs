namespace _2D_Particle_Simulator
{
    partial class FormParticleSim
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
            components = new System.ComponentModel.Container();
            timerTick = new System.Windows.Forms.Timer(components);
            labelDensity = new Label();
            labelParticleCount = new Label();
            labelAverageSpeed = new Label();
            checkBoxOverylayDensity = new CheckBox();
            numericUpDownCycles = new NumericUpDown();
            labelCyclesPerFrame = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCycles).BeginInit();
            SuspendLayout();
            // 
            // timerTick
            // 
            timerTick.Enabled = true;
            timerTick.Interval = 13;
            timerTick.Tick += timerTick_Tick;
            // 
            // labelDensity
            // 
            labelDensity.AutoSize = true;
            labelDensity.BackColor = SystemColors.Desktop;
            labelDensity.ForeColor = SystemColors.ControlLightLight;
            labelDensity.Location = new Point(12, 29);
            labelDensity.Name = "labelDensity";
            labelDensity.Size = new Size(93, 20);
            labelDensity.TabIndex = 0;
            labelDensity.Text = "Density: TBD";
            // 
            // labelParticleCount
            // 
            labelParticleCount.AutoSize = true;
            labelParticleCount.BackColor = SystemColors.Desktop;
            labelParticleCount.ForeColor = SystemColors.ControlLightLight;
            labelParticleCount.Location = new Point(12, 9);
            labelParticleCount.Name = "labelParticleCount";
            labelParticleCount.Size = new Size(98, 20);
            labelParticleCount.TabIndex = 1;
            labelParticleCount.Text = "Particles: TBD";
            // 
            // labelAverageSpeed
            // 
            labelAverageSpeed.AutoSize = true;
            labelAverageSpeed.BackColor = SystemColors.Desktop;
            labelAverageSpeed.ForeColor = SystemColors.ControlLightLight;
            labelAverageSpeed.Location = new Point(12, 49);
            labelAverageSpeed.Name = "labelAverageSpeed";
            labelAverageSpeed.Size = new Size(145, 20);
            labelAverageSpeed.TabIndex = 2;
            labelAverageSpeed.Text = "Average Speed: TBD";
            // 
            // checkBoxOverylayDensity
            // 
            checkBoxOverylayDensity.AutoSize = true;
            checkBoxOverylayDensity.ForeColor = SystemColors.ControlLightLight;
            checkBoxOverylayDensity.Location = new Point(12, 72);
            checkBoxOverylayDensity.Name = "checkBoxOverylayDensity";
            checkBoxOverylayDensity.Size = new Size(134, 24);
            checkBoxOverylayDensity.TabIndex = 3;
            checkBoxOverylayDensity.Text = "Overlay Density";
            checkBoxOverylayDensity.UseVisualStyleBackColor = true;
            checkBoxOverylayDensity.CheckedChanged += checkBoxOverylayDensity_CheckedChanged;
            // 
            // numericUpDownCycles
            // 
            numericUpDownCycles.Location = new Point(125, 97);
            numericUpDownCycles.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownCycles.Name = "numericUpDownCycles";
            numericUpDownCycles.Size = new Size(46, 27);
            numericUpDownCycles.TabIndex = 4;
            // 
            // labelCyclesPerFrame
            // 
            labelCyclesPerFrame.AutoSize = true;
            labelCyclesPerFrame.ForeColor = SystemColors.ControlLightLight;
            labelCyclesPerFrame.Location = new Point(12, 99);
            labelCyclesPerFrame.Name = "labelCyclesPerFrame";
            labelCyclesPerFrame.Size = new Size(107, 20);
            labelCyclesPerFrame.TabIndex = 5;
            labelCyclesPerFrame.Text = "Cycles Per Tick:";
            // 
            // FormParticleSim
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 553);
            Controls.Add(labelCyclesPerFrame);
            Controls.Add(numericUpDownCycles);
            Controls.Add(checkBoxOverylayDensity);
            Controls.Add(labelAverageSpeed);
            Controls.Add(labelParticleCount);
            Controls.Add(labelDensity);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormParticleSim";
            ShowIcon = false;
            Text = "Particle Sim";
            TopMost = true;
            Paint += FormParticleSim_Paint;
            ((System.ComponentModel.ISupportInitialize)numericUpDownCycles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timerTick;
        private Label labelDensity;
        private Label labelParticleCount;
        private Label labelAverageSpeed;
        private CheckBox checkBoxOverylayDensity;
        private NumericUpDown numericUpDownCycles;
        private Label labelCyclesPerFrame;
    }
}
