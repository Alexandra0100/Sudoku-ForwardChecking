using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_IA_project
{
    public class TableGenerator
    {
        private static int[,] table = new int[9, 9]; 
        private static Random random = new Random();
        public static bool[,,] possibleValues = new bool[9,9,9];

        /// <summary>
        /// Functie care genereaza o tabla valida(matrice), in caz contrar, returneaza null
        /// </summary>
        /// <returns>int[,]</returns>
        public static int[,] GenerateValidTable()
        {
            InitialPossibleValues();
            if (GenerateTable(0,0)) 
            {
                return table;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Functia foloseste algoritmul Forward Checking pentru a umple tabla
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>bool</returns>
        private static bool GenerateTable(int row, int column)
        {
            //se trece prin toata matricea
            if (column == 9)
            {
                column = 0;
                row++;
                if (row == 9)
                {
                    return true;
                }
            }

            //in cazul in care exista o valoare in celula, se trece la urmatoarea coloana
            if (table[row, column] != 0)
            {
                return GenerateTable(row, column + 1);
            }

            // se verifica toate posibilitatile pentru a umple o celula. valorile sunt alese aleator
            List<int> randomValues = Enumerable.Range(1, 9).OrderBy(x=> random.Next()).ToList();
            
            foreach(int nr in randomValues)
            {
                if (IsValid(row, column, nr)) // validez numarul pe care il voi pune in celula
                {
                    table[row, column] = nr;
                    UpdatePossibileValues(row, column,nr,false);
                    if (GenerateTable(row, column + 1))
                    {
                        return true;
                    }
                    table[row, column] = 0;
                    UpdatePossibileValues(row,column,nr,true);
                }
            }
            return false;
        }

        /// <summary>
        /// Functie care verifica daca un numar poate fi pus intr-o celula din tabla
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="nr"></param>
        /// <returns>bool</returns>
        private static bool IsValid(int row, int column, int nr)
        {
            //verific daca numarul este deja pe linie
            for(int i = 0; i < 9; i++)
            {
                if (i != column && table[row, i] == nr)
                {
                    return false;
                }
            //verific dacă numărul este deja pe coloană
                if (i != row && table[i, column] == nr)
                {
                    return false;
                }
            }

            // verific daca numarul este deja in patratelul de 3x3
            int startRowCell = (row / 3) * 3;
            int startColumnCell = (column / 3) * 3;
            for (int i = startRowCell; i < startRowCell + 3; i++)
            {
                for(int j = startColumnCell; j < startColumnCell + 3; j++)
                {
                    if (table[i, j] == nr)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Functie care creaza o tabla de Sudoku prin eliminarea unor valori alese random
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public static int[,] SudokuTableGenerator(int difficulty)
        {
            int[,] sudokuTable = GenerateValidTable();

            int removedCells = 0;

            // dificultatea indica numarul de celule goale
            while( removedCells < difficulty)
            {
                int row = random.Next(0,9);
                int column = random.Next(0,9);

                if(sudokuTable[row, column] != 0)
                {
                    sudokuTable[row, column] = 0;
                    removedCells++;
                }
            }
            return sudokuTable;
        }

        /// <summary>
        /// Functie care initializeaza toate valorile ca fiind posibile
        /// </summary>
        private static void InitialPossibleValues()
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        possibleValues[i, j,k] = true;
                    }
                }
            }
        }

        /// <summary>
        /// Functia updateaza lista cu valorile posibile pentru celulele necompletate
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="nr"></param>
        /// <param name="update"></param>
        private static void UpdatePossibileValues(int row, int column, int nr, bool update)
        {
            for (int i = 0; i < 9; i++)
            {
                if (update)
                {
                    possibleValues[row, i, nr - 1] = true;
                }
                else
                {
                    possibleValues[row, i, nr - 1] = false;
                }

                if (update)
                {
                    possibleValues[i, column, nr - 1] = true;
                }
                else
                {
                    possibleValues[i, column, nr - 1] = false;
                }
            }
            int startRowCell = (row / 3) * 3;
            int startColumnCell = (column / 3) * 3;
            for (int i = startRowCell; i < startRowCell + 3; i++)
            {
                for (int j = startColumnCell; j < startColumnCell + 3; j++)
                {
                    if (update)
                    {
                        possibleValues[i, j, nr - 1] = true;
                    }
                    else
                    {
                        possibleValues[i, j, nr - 1] = false;
                    }
                }

            }
        }
    }
}
