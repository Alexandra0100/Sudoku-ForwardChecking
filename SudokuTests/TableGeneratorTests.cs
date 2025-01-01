using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_IA_project; 
using System.Collections.Generic;

namespace SudokuTests
{
    [TestClass]
    public class TableGeneratorTests
    {
        [TestMethod]
        public void TestGenerateValidTable()
        {
            var table = TableGenerator.GenerateValidTable();
            Assert.IsNotNull(table);
            Assert.IsTrue(IsSudokuTableValid(table));
        }

        [TestMethod]
        public void TestSudokuTableGenerator_Difficulty()
        {
            int difficulty = 40; 
            var table = TableGenerator.SudokuTableGenerator(difficulty);
            Assert.IsNotNull(table);
            int emptyCells = CountEmptyCells(table);
            Assert.AreEqual(difficulty, emptyCells);
        }

        [TestMethod]
        public void TestGenerateTableCompleteness()
        {
            var table = TableGenerator.GenerateValidTable();
            Assert.IsNotNull(table);

            foreach (var cell in table)
            {
                Assert.AreNotEqual(0, cell);
            }
        }

        [TestMethod]
        public void TestUpdatePossibleValues()
        {
            TableGenerator.InitialPossibleValues();
            TableGenerator.UpdatePossibileValues(0, 0, 5, false);

            Assert.IsFalse(TableGenerator.possibleValues[0, 1, 4]); 
            Assert.IsFalse(TableGenerator.possibleValues[1, 0, 4]);
            Assert.IsFalse(TableGenerator.possibleValues[1, 1, 4]);
        }

        
        private bool IsSudokuTableValid(int[,] table)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsValidRow(table, i) || !IsValidColumn(table, i) || !IsValidBlock(table, i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidRow(int[,] table, int row)
        {
            var seen = new HashSet<int>();
            for (int col = 0; col < 9; col++)
            {
                if (table[row, col] != 0)
                {
                    if (!seen.Add(table[row, col])) return false;
                }
            }
            return true;
        }

        private bool IsValidColumn(int[,] table, int column)
        {
            var seen = new HashSet<int>();
            for (int row = 0; row < 9; row++)
            {
                if (table[row, column] != 0)
                {
                    if (!seen.Add(table[row, column])) return false;
                }
            }
            return true;
        }

        private bool IsValidBlock(int[,] table, int block)
        {
            var seen = new HashSet<int>();
            int startRow = (block / 3) * 3;
            int startCol = (block % 3) * 3;
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (table[row, col] != 0)
                    {
                        if (!seen.Add(table[row, col])) return false;
                    }
                }
            }
            return true;
        }

        private int CountEmptyCells(int[,] table)
        {
            int count = 0;
            foreach (var cell in table)
            {
                if (cell == 0) count++;
            }
            return count;
        }
    }
}
