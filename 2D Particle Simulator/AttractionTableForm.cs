using System;
using System.Windows.Forms;

namespace _2D_Particle_Simulator
{
    internal sealed class AttractionTableForm : Form
    {
        private readonly DataGridView grid;

        public AttractionTableForm(float[,] attractionLevels)
        {
            if (attractionLevels == null)
            {
                throw new ArgumentNullException(nameof(attractionLevels));
            }

            Text = "Attraction Levels";
            StartPosition = FormStartPosition.CenterParent;
            ShowIcon = false;

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                RowHeadersWidth = 60
            };

            InitializeGrid(attractionLevels);
            Controls.Add(grid);
        }

        private void InitializeGrid(float[,] attractionLevels)
        {
            int size = attractionLevels.GetLength(0);
            grid.ColumnCount = size;
            grid.RowCount = size;
            grid.DefaultCellStyle.Format = "0.###";

            for (int i = 0; i < size; i++)
            {
                grid.Columns[i].Name = $"Type {i}";
                grid.Rows[i].HeaderCell.Value = $"Type {i}";
                for (int j = 0; j < size; j++)
                {
                    grid.Rows[i].Cells[j].Value = attractionLevels[i, j];
                }
            }
        }
    }
}
