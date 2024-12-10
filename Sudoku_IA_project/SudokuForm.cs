using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
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
        private bool isFirstTableGeneration = true;
        private int lastSelectedRow = -1;
        private int lastSelectedColumn = -1;
        private int maxHints = 10;
        private static int nrHints = 0;


        public SudokuForm()
        {
            InitializeComponent();
            playButton.Font = new Font("Arial", 10);
            cleanButton.Font = new Font("Arial", 10);
            exitButton.Font = new Font("Arial", 10);
            validateMoveButton.Font = new Font("Arial", 10);
            getHintButton.Font = new Font("Arial", 10);
            HelpButton.Font = new Font("Arial", 10);

            cleanButton.Enabled = false;
            validateMoveButton.Enabled = false;
            getHintButton.Enabled = false;
            InitializeDataGridView();
            sudokuDataGridView.Enabled = false;


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
            sudokuDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            sudokuDataGridView.Paint += SudokuDataGridView_Paint;
            sudokuDataGridView.CellClick += sudokuDataGridView_CellClick;
        }

        /// <summary>
        /// Functie care afiseaza tabela Sudoku pe interfata.
        /// </summary>
        private void SetTableOnDataGridView()
        {
            isFillingAutomatically = true;
            sudokuDataGridView.Enabled = true;
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
                        sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Black;
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
            nrHints = 0;
            if (isFirstTableGeneration)
            {
                cleanButton.Enabled = true;
                validateMoveButton.Enabled = true;
                getHintButton.Enabled = true;
                table = TableGenerator.SudokuTableGenerator(50);
                SetTableOnDataGridView();
                sudokuDataGridView.AllowUserToAddRows = false;
                isFirstTableGeneration = false;
            }
            else
            {
                getHintButton.Enabled = true;
                cleanButton.Enabled = true;
                validateMoveButton.Enabled = true;
                CleanTableOnDataGridView();
                table = TableGenerator.SudokuTableGenerator(50);
                SetTableOnDataGridView();
                sudokuDataGridView.AllowUserToAddRows = false;
            }
            

        }



        /// <summary>
        /// Callback pt butonul de stergere a numerelor din tabel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cleanButton_Click(object sender, EventArgs e)
        {
            nrHints = 0;
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

        /// <summary>
        /// Functia pentru butonul de validare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateMoveButton_Click(object sender, EventArgs e)
        {
            int countRed = 0;
            int countGreen = 0;
            int countAllGreen = 0;
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
                            countAllGreen++;
                            sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Green;
                            if(sudokuDataGridView.Rows[row].Cells[column].Style.BackColor != System.Drawing.Color.LightGray)
                                countGreen++;
                        }
                    }
                }
            }
            if(countAllGreen == 81)
            {
                MessageBox.Show("Felicitari! Ai castigat!\nAi ajuns la final si toate valorile sunt corecte!");
            }
            else if (countRed == 0 && countGreen != 0)
            {
                MessageBox.Show("Pana aici este bine. Poti continua.");
            }
            else if(countRed != 0 && countGreen == 0)
            {
                MessageBox.Show("Din pacate ai gresit "+ countRed+" valori.");
            }
            else if (countRed != 0 && countGreen != 0)
            {
                MessageBox.Show("Ups... Ai gresit "+ countRed + " valori.\nMai incearca.");
            }
            else if (countRed == 0 && countGreen == 0)
            {
                MessageBox.Show("Inca nu ai completat nicio celula.\nTe rugam sa validezi dupa ce ai completat cateva celule.");
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

            if (row < 0 || column < 0 || row >= 9 || column >= 9)
                return;

            var cellValue = sudokuDataGridView.Rows[row].Cells[column].Value;

            if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
            {
                table[e.RowIndex, e.ColumnIndex] = 0;
                return;
            }
            
            if (!int.TryParse(cellValue.ToString(), out cellValueChecked))
            {
                MessageBox.Show("Introdu o valoare numerica.");
                sudokuDataGridView.Rows[row].Cells[column].Value = "";
            }
            else if (cellValueChecked < 1 || cellValueChecked > 9)
            {
                MessageBox.Show("Introdu o valoare numerica intre 1 si 9.");
                sudokuDataGridView.Rows[row].Cells[column].Value = "";
            }
            table[row, column] = cellValueChecked;
        }
        /// <summary>
        /// Functie de Forward Checking pentru valorile posibile
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private List<int> GetPossibleValuesFromForwardChecking(int row, int column)
        {
            List<int> possibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; ;

            for (int i = 0; i < 9; i++)
            {
                if (table[row, i] != 0) 
                {
                    possibleValues.Remove(table[row, i]);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (table[i, column] != 0) 
                {
                    possibleValues.Remove(table[i, column]); 
                }
            }

            int startRow = (row / 3) * 3;
            int startCol = (column / 3) * 3;
            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (table[i, j] != 0) 
                    {
                        possibleValues.Remove(table[i, j]); 
                    }
                }
            }

            return possibleValues;
        }
        /// <summary>
        /// Functie care valideaza mutarile
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Functie care deseneaza muchiile patratelor de 3x3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuDataGridView_Paint(object sender, PaintEventArgs e)
        {
            var gridPen = new Pen(Color.Black, 3); 
            int cellWidth = sudokuDataGridView.Columns[0].Width;
            int cellHeight = sudokuDataGridView.Rows[0].Height;
            

            for (int i = 1; i < 3; i++)
            {
                int y = i * 3 * cellHeight+3;
                e.Graphics.DrawLine(gridPen, 0, y, sudokuDataGridView.Width, y);
            }

            for (int i = 1; i < 3; i++)
            {
                int x = i * 3 * cellWidth+3*(i+1);
                e.Graphics.DrawLine(gridPen, x, 0, x, sudokuDataGridView.Height);
            }
        }
        private void sudokuDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lastSelectedRow = e.RowIndex;
            lastSelectedColumn = e.ColumnIndex;
        }
        /// <summary>
        /// Functie pentru butonul de hint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getHintButton_Click(object sender, EventArgs e)
        {
            if (lastSelectedRow == -1 || lastSelectedColumn == -1)
            {
                MessageBox.Show("Te rugăm să selectezi mai întâi o celulă.");
                return;
            }

            List<int> possibleValues = GetPossibleValuesFromForwardChecking(lastSelectedRow, lastSelectedColumn);

            if (possibleValues.Count > 0 && nrHints <= maxHints && sudokuDataGridView.Rows[lastSelectedRow].Cells[lastSelectedColumn].Style.ForeColor != System.Drawing.Color.Green)
            {
                Random random = new Random();
                int randomValue = possibleValues[random.Next(possibleValues.Count)];

                sudokuDataGridView.Rows[lastSelectedRow].Cells[lastSelectedColumn].Value = randomValue.ToString();
                sudokuDataGridView.Rows[lastSelectedRow].Cells[lastSelectedColumn].Style.ForeColor = System.Drawing.Color.Green;
                table[lastSelectedRow, lastSelectedColumn] = randomValue;
                nrHints++;
                MessageBox.Show("Ati folosit "+ nrHints + " din maximul de " + maxHints + " hinturi.");
            }
            else if(possibleValues.Count == 0) 
            {
                MessageBox.Show("Nu există valori posibile pentru această celulă.");
            }
            else if (sudokuDataGridView.Rows[lastSelectedRow].Cells[lastSelectedColumn].Style.ForeColor == System.Drawing.Color.Green)
            {
                MessageBox.Show("Aceasta celula are deja o valoare valida.");
            }
            else if(nrHints > maxHints)
            {
                MessageBox.Show("Ne pare rau.\nNu mai aveti dreptul la hinturi pentru acest joc.");
            }
            
            
        }
        /// <summary>
        /// Functie pentru butonul de help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reguli:\n\n-Pentru a incepe jocul este necesar ca jucatorul sa genereze o tabla, cu ajutorul butonului Generate table. Se poate genera oricand alta tabla, dar progresul anterior va fi pierdut.\n\n- Pentru a putea castiga, jucatorul trebuie sa completeze toate celulele goale cu valori valide. O valoare valida este una cuprinsa intre 1 si 9, care nu se repeta pe acelasi rand, coloana sau patratet de 3x3 marcat cu linii ingrosate.\n\n-Validarea se poate face cu butonul de validare, care va marca numerele valide cu verde, iar cele invalide cu rosu.\n\n-Daca se doreste stergerea tuturor valorilor completate pana in momentul respectiv, se va folosi butonul de Clena Table.\n\n-In cazul in care jucatorul are dificultati, se pot sugera valori de la butonul de Get Hint, in limita numarului maxim de hinturi oferit.\n\nMult succes!");
        }
    }
}
