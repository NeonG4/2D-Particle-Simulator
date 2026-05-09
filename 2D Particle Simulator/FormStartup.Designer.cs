namespace _2D_Particle_Simulator
{
    partial class FormStartup
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
            labelNumberOfParticles = new Label();
            textBoxParticleCount = new TextBox();
            textBoxBoundaryX = new TextBox();
            labelBoundaries = new Label();
            labelBoundaryBy = new Label();
            textBoxBoundaryY = new TextBox();
            buttonCreateSimulator = new Button();
            textBoxAverageDensity = new TextBox();
            labelDensity = new Label();
            labelParticleTypes = new Label();
            numericUpDownParticleTypeCount = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownParticleTypeCount).BeginInit();
            SuspendLayout();
            // 
            // labelNumberOfParticles
            // 
            labelNumberOfParticles.AutoSize = true;
            labelNumberOfParticles.ForeColor = Color.White;
            labelNumberOfParticles.Location = new Point(12, 9);
            labelNumberOfParticles.Name = "labelNumberOfParticles";
            labelNumberOfParticles.Size = new Size(107, 20);
            labelNumberOfParticles.TabIndex = 0;
            labelNumberOfParticles.Text = "Particle Count: ";
            // 
            // textBoxParticleCount
            // 
            textBoxParticleCount.Location = new Point(144, 6);
            textBoxParticleCount.Name = "textBoxParticleCount";
            textBoxParticleCount.Size = new Size(136, 27);
            textBoxParticleCount.TabIndex = 1;
            textBoxParticleCount.Text = "1000";
            // 
            // textBoxBoundaryX
            // 
            textBoxBoundaryX.Location = new Point(144, 37);
            textBoxBoundaryX.Name = "textBoxBoundaryX";
            textBoxBoundaryX.Size = new Size(54, 27);
            textBoxBoundaryX.TabIndex = 2;
            textBoxBoundaryX.Text = "512";
            textBoxBoundaryX.TextChanged += textBoxBoundaryX_TextChanged;
            // 
            // labelBoundaries
            // 
            labelBoundaries.AutoSize = true;
            labelBoundaries.ForeColor = Color.White;
            labelBoundaries.Location = new Point(12, 40);
            labelBoundaries.Name = "labelBoundaries";
            labelBoundaries.Size = new Size(106, 20);
            labelBoundaries.TabIndex = 2;
            labelBoundaries.Text = "Boundary Size:";
            // 
            // labelBoundaryBy
            // 
            labelBoundaryBy.AutoSize = true;
            labelBoundaryBy.ForeColor = Color.White;
            labelBoundaryBy.Location = new Point(204, 40);
            labelBoundaryBy.Name = "labelBoundaryBy";
            labelBoundaryBy.Size = new Size(16, 20);
            labelBoundaryBy.TabIndex = 4;
            labelBoundaryBy.Text = "x";
            // 
            // textBoxBoundaryY
            // 
            textBoxBoundaryY.Location = new Point(226, 37);
            textBoxBoundaryY.Name = "textBoxBoundaryY";
            textBoxBoundaryY.Size = new Size(54, 27);
            textBoxBoundaryY.TabIndex = 3;
            textBoxBoundaryY.Text = "256";
            // 
            // buttonCreateSimulator
            // 
            buttonCreateSimulator.Location = new Point(164, 163);
            buttonCreateSimulator.Name = "buttonCreateSimulator";
            buttonCreateSimulator.Size = new Size(94, 29);
            buttonCreateSimulator.TabIndex = 5;
            buttonCreateSimulator.Text = "Start Sim";
            buttonCreateSimulator.UseVisualStyleBackColor = true;
            buttonCreateSimulator.Click += buttonCreateSimulator_Click;
            // 
            // textBoxAverageDensity
            // 
            textBoxAverageDensity.Location = new Point(144, 70);
            textBoxAverageDensity.Name = "textBoxAverageDensity";
            textBoxAverageDensity.ReadOnly = true;
            textBoxAverageDensity.Size = new Size(136, 27);
            textBoxAverageDensity.TabIndex = 6;
            // 
            // labelDensity
            // 
            labelDensity.AutoSize = true;
            labelDensity.ForeColor = Color.White;
            labelDensity.Location = new Point(12, 73);
            labelDensity.Name = "labelDensity";
            labelDensity.Size = new Size(120, 20);
            labelDensity.TabIndex = 8;
            labelDensity.Text = "Average Density:";
            // 
            // labelParticleTypes
            // 
            labelParticleTypes.AutoSize = true;
            labelParticleTypes.ForeColor = Color.White;
            labelParticleTypes.Location = new Point(12, 107);
            labelParticleTypes.Name = "labelParticleTypes";
            labelParticleTypes.Size = new Size(101, 20);
            labelParticleTypes.TabIndex = 10;
            labelParticleTypes.Text = "Particle Types:";
            // 
            // numericUpDownParticleTypeCount
            // 
            numericUpDownParticleTypeCount.Location = new Point(144, 107);
            numericUpDownParticleTypeCount.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numericUpDownParticleTypeCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownParticleTypeCount.Name = "numericUpDownParticleTypeCount";
            numericUpDownParticleTypeCount.Size = new Size(136, 27);
            numericUpDownParticleTypeCount.TabIndex = 4;
            numericUpDownParticleTypeCount.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // FormStartup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(numericUpDownParticleTypeCount);
            Controls.Add(labelParticleTypes);
            Controls.Add(labelDensity);
            Controls.Add(textBoxAverageDensity);
            Controls.Add(buttonCreateSimulator);
            Controls.Add(textBoxBoundaryY);
            Controls.Add(labelBoundaryBy);
            Controls.Add(textBoxBoundaryX);
            Controls.Add(labelBoundaries);
            Controls.Add(textBoxParticleCount);
            Controls.Add(labelNumberOfParticles);
            ForeColor = Color.Black;
            KeyPreview = true;
            Name = "FormStartup";
            ShowIcon = false;
            Text = "Startup Particle Simulator";
            TransparencyKey = Color.DodgerBlue;
            ((System.ComponentModel.ISupportInitialize)numericUpDownParticleTypeCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNumberOfParticles;
        private TextBox textBoxParticleCount;
        private TextBox textBoxBoundaryX;
        private Label labelBoundaries;
        private Label labelBoundaryBy;
        private TextBox textBoxBoundaryY;
        private Button buttonCreateSimulator;
        private TextBox textBoxAverageDensity;
        private Label labelDensity;
        private Label labelParticleTypes;
        private NumericUpDown numericUpDownParticleTypeCount;
    }
}