using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_IA_project
{
    public partial class SudokuForm : Form
    {
        private int[,] table;
        public SudokuForm()
        {
            InitializeComponent();
            playButton.Font = new Font("Arial", 10);
            cleanButton.Font = new Font("Arial", 10);
            exitButton.Font = new Font("Arial", 10);
            CleanTableOnDataGridView();

        }

        /// <summary>
        /// Functie care afiseaza tabela Sudoku pe interfata.
        /// </summary>
        private void SetTableOnDataGridView()
        {
            sudokuDataGridView.Rows.Clear();
            if (sudokuDataGridView.Rows.Count < 9)
            {
                for(int i = sudokuDataGridView.Rows.Count; i < 9; i++)
                {
                    sudokuDataGridView.Rows.Add();
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(table[i,j] != 0)
                    {
                        sudokuDataGridView.Rows[i].Cells[j].Value = table[i, j].ToString();
                        sudokuDataGridView.Rows[i].Cells[j].ReadOnly = true;
                        sudokuDataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Green;
                        sudokuDataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGray;
                        

                    }
                    else
                    {
                        sudokuDataGridView.Rows[i].Cells[j].Value ="";
                        sudokuDataGridView.Rows[i].Cells[j].ReadOnly = false;
                    }
                    sudokuDataGridView.Rows[i].Cells[j].Style.Font = new Font("Arial", 22, FontStyle.Bold);
                    sudokuDataGridView.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
        }

        private void CleanTableOnDataGridView()
        {
            sudokuDataGridView.Rows.Clear();
            for (int i = 0; i < 9; i++)
            {

                sudokuDataGridView.Rows.Add();
                
                for (int j = 0; j < 9; j++)
                {
                   sudokuDataGridView.Rows[i].Cells[j].Value = "";
                }

            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            table = TableGenerator.SudokuTableGenerator(50);
            SetTableOnDataGridView();
            sudokuDataGridView.AllowUserToAddRows = false;

        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            CleanTableOnDataGridView();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
