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
            numericUpDownCycles = new NumericUpDown();
            labelCyclesPerFrame = new Label();
            buttonPausePlay = new Button();
            buttonRandomize = new Button();
            buttonRandomizeAttraction = new Button();
            labelAttractionKey = new Label();
            dataGridAttraction = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCycles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAttraction).BeginInit();
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
            // numericUpDownCycles
            // 
            numericUpDownCycles.Location = new Point(125, 67);
            numericUpDownCycles.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownCycles.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCycles.Name = "numericUpDownCycles";
            numericUpDownCycles.Size = new Size(46, 27);
            numericUpDownCycles.TabIndex = 4;
            numericUpDownCycles.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelCyclesPerFrame
            // 
            labelCyclesPerFrame.AutoSize = true;
            labelCyclesPerFrame.ForeColor = SystemColors.ControlLightLight;
            labelCyclesPerFrame.Location = new Point(12, 69);
            labelCyclesPerFrame.Name = "labelCyclesPerFrame";
            labelCyclesPerFrame.Size = new Size(107, 20);
            labelCyclesPerFrame.TabIndex = 5;
            labelCyclesPerFrame.Text = "Cycles Per Tick:";
            // 
            // buttonPausePlay
            // 
            buttonPausePlay.Location = new Point(11, 104);
            buttonPausePlay.Name = "buttonPausePlay";
            buttonPausePlay.Size = new Size(108, 29);
            buttonPausePlay.TabIndex = 6;
            buttonPausePlay.Text = "Pause";
            buttonPausePlay.UseVisualStyleBackColor = true;
            buttonPausePlay.Click += buttonPausePlay_Click;
            // 
            // buttonRandomize
            // 
            buttonRandomize.Location = new Point(11, 139);
            buttonRandomize.Name = "buttonRandomize";
            buttonRandomize.Size = new Size(108, 29);
            buttonRandomize.TabIndex = 8;
            buttonRandomize.Text = "Randomize";
            buttonRandomize.UseVisualStyleBackColor = true;
            buttonRandomize.Click += buttonRandomize_Click;
            // 
            // buttonRandomizeAttraction
            // 
            buttonRandomizeAttraction.Location = new Point(0, 0);
            buttonRandomizeAttraction.Name = "buttonRandomizeAttraction";
            buttonRandomizeAttraction.Size = new Size(150, 29);
            buttonRandomizeAttraction.TabIndex = 10;
            buttonRandomizeAttraction.Text = "Randomize Table";
            buttonRandomizeAttraction.UseVisualStyleBackColor = true;
            buttonRandomizeAttraction.Click += buttonRandomizeAttraction_Click;
            // 
            // labelAttractionKey
            // 
            labelAttractionKey.AutoSize = true;
            labelAttractionKey.ForeColor = SystemColors.ControlLightLight;
            labelAttractionKey.Location = new Point(12, 178);
            labelAttractionKey.Name = "labelAttractionKey";
            labelAttractionKey.Size = new Size(38, 20);
            labelAttractionKey.TabIndex = 9;
            labelAttractionKey.Text = "Key";
            // 
            // dataGridAttraction
            // 
            dataGridAttraction.AllowUserToAddRows = false;
            dataGridAttraction.AllowUserToDeleteRows = false;
            dataGridAttraction.AllowUserToResizeRows = false;
            dataGridAttraction.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridAttraction.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAttraction.Location = new Point(0, 0);
            dataGridAttraction.Margin = new Padding(3, 4, 3, 4);
            dataGridAttraction.Name = "dataGridAttraction";
            dataGridAttraction.ReadOnly = true;
            dataGridAttraction.RowHeadersWidth = 60;
            dataGridAttraction.Size = new Size(800, 200);
            dataGridAttraction.TabIndex = 7;
            // 
            // FormParticleSim
            // 
            AcceptButton = buttonPausePlay;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 553);
            Controls.Add(labelAttractionKey);
            Controls.Add(buttonRandomizeAttraction);
            Controls.Add(buttonRandomize);
            Controls.Add(buttonPausePlay);
            Controls.Add(dataGridAttraction);
            Controls.Add(labelCyclesPerFrame);
            Controls.Add(numericUpDownCycles);
            Controls.Add(labelAverageSpeed);
            Controls.Add(labelParticleCount);
            Controls.Add(labelDensity);
            DoubleBuffered = true;
            ForeColor = Color.Black;
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormParticleSim";
            ShowIcon = false;
            Text = "Particle Sim";
            TransparencyKey = Color.DarkSlateGray;
            FormClosed += FormParticleSim_FormClosed;
            Paint += FormParticleSim_Paint;
            Enter += FormParticleSim_Enter;
            KeyDown += FormParticleSim_KeyDown;
            KeyUp += FormParticleSim_KeyUp;
            ((System.ComponentModel.ISupportInitialize)numericUpDownCycles).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAttraction).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timerTick;
        private Label labelDensity;
        private Label labelParticleCount;
        private Label labelAverageSpeed;
        private NumericUpDown numericUpDownCycles;
        private Label labelCyclesPerFrame;
        private Button buttonPausePlay;
        private Button buttonRandomize;
        private Button buttonRandomizeAttraction;
        private Label labelAttractionKey;
        private DataGridView dataGridAttraction;
    }
}
