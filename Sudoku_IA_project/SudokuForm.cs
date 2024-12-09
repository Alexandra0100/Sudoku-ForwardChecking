using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_IA_project
{
    public partial class SudokuForm : Form
    {
        private int[,] table;
        private bool isFillingAutomatically = false;
        public SudokuForm()
        {
            InitializeComponent();
            playButton.Font = new Font("Arial", 10);
            cleanButton.Font = new Font("Arial", 10);
            exitButton.Font = new Font("Arial", 10);
            validateMoveButton.Font = new Font("Arial", 10);
            InitializeDataGridView();


        }
        /// <summary>
        /// Functie de initializare a tabelului
        /// </summary>
        private void InitializeDataGridView()
        {
            sudokuDataGridView.Rows.Clear();
            for (int i = 0; i < 9; i++)
            {
                sudokuDataGridView.Rows.Add();
            }
        }

        /// <summary>
        /// Functie care afiseaza tabela Sudoku pe interfata.
        /// </summary>
        private void SetTableOnDataGridView()
        {
            isFillingAutomatically = true;
            InitializeDataGridView();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (table[i, j] != 0)
                    {
                        sudokuDataGridView.Rows[i].Cells[j].Value = table[i, j].ToString();
                        sudokuDataGridView.Rows[i].Cells[j].ReadOnly = true;
                        sudokuDataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Green;
                        sudokuDataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGray;


                    }
                    else
                    {
                        sudokuDataGridView.Rows[i].Cells[j].Value = "";
                        sudokuDataGridView.Rows[i].Cells[j].ReadOnly = false;
                    }
                    sudokuDataGridView.Rows[i].Cells[j].Style.Font = new Font("Arial", 22, FontStyle.Bold);
                    sudokuDataGridView.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            isFillingAutomatically = false;
        }
        /// <summary>
        /// Functie pentru curatarea tabelului. Pune valori nule in toate casutele
        /// </summary>
        private void CleanTableOnDataGridView()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (sudokuDataGridView.Rows[row].Cells[column].Style.BackColor != System.Drawing.Color.LightGray)
                    {
                        sudokuDataGridView.Rows[row].Cells[column].Value = "";
                        table[row, column] = 0;
                    }
                }
            }
        }
        /// <summary>
        ///  <summary>
        /// Callback pt butonul de generarea al tablei
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            table = TableGenerator.SudokuTableGenerator(50);
            SetTableOnDataGridView();
            sudokuDataGridView.AllowUserToAddRows = false;

        }



        /// <summary>
        /// Callback pt butonul de stergere a numerelor din tabel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cleanButton_Click(object sender, EventArgs e)
        {
            CleanTableOnDataGridView();
        }

        /// <summary>
        /// Functie de iesire din program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void validateMoveButton_Click(object sender, EventArgs e)
        {
            int countRed = 0;
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var cellValue = sudokuDataGridView.Rows[row].Cells[column].Value;

                    if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                    {
                        table[row, column] = 0;
                        continue;
                    }

                    else if (!int.TryParse(cellValue.ToString(), out int value) || value < 1 || value > 9)
                    {
                        MessageBox.Show($"Valoare invalidă în celula ({row + 1}, {column + 1}).");
                        return;
                    }
                    else if (!IsMoveValid(row, column, value) && (sudokuDataGridView.Rows[row].Cells[column].Style.BackColor != System.Drawing.Color.LightGray) && (sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor != System.Drawing.Color.Green))
                    {
                        sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Red;
                        countRed++;
                        //MessageBox.Show($"Valoare din celula ({row + 1}, {column + 1}) este invalida.");
                        
                    }
                    else
                    {
                        sudokuDataGridView.Rows[row].Cells[column].ReadOnly = true;
                        if (sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor != System.Drawing.Color.Green)
                        {
                            sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                }
            }
            if (countRed == 0)
            {
                MessageBox.Show("Tabela este validă! Felicitări!");
            }
        }
        /// <summary>
        /// Functie de validare a caracterelor introduse in celule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isFillingAutomatically)
                return;

            int row = e.RowIndex;
            int column = e.ColumnIndex;
            int cellValueChecked;

            //verificam daca sunt valizi indecsii
            if (row < 0 || column < 0 || row >= 9 || column >= 9)
                return;

            var cellValue = sudokuDataGridView.Rows[row].Cells[column].Value;

            //daca celula e goala nu modific nimic
            if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
            {
                table[e.RowIndex, e.ColumnIndex] = 0;
                return;
            }

            // verific daca am caractere numerice
            if (!int.TryParse(cellValue.ToString(), out cellValueChecked))
            {
                MessageBox.Show("Introdu o valoare numerica.");
                sudokuDataGridView.Rows[row].Cells[column].Value = "";
            }
            //verific daca valorile sunt in intervalul [1,9]
            else if (cellValueChecked < 1 || cellValueChecked > 9)
            {
                MessageBox.Show("Introdu o valoare numerica intre 1 si 9.");
                sudokuDataGridView.Rows[row].Cells[column].Value = "";
            }
            table[row, column] = cellValueChecked;
        }

        private List<int> GetPossibleValuesFromForwardChecking(int row, int column)
        {
            List<int> possibleValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (TableGenerator.possibleValues[row, column, i])
                {
                    possibleValues.Add(i);
                }
            }
            return possibleValues;
        }

        private bool IsMoveValid(int row, int column, int value)
        {
            
            for (int i = 0; i < 9; i++)
            {
                if (i != column && table[row, i] == value)
                    return false;
            }

            for (int i = 0; i < 9; i++)
            {
                if (i != row && table[i, column] == value)
                    return false;
            }

            int startRow = (row / 3) * 3;
            int startCol = (column / 3) * 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if ((i != row || j != column) && table[i, j] == value)
                        return false;
                }
            }

            return true;
        }
    }

}
