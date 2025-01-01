 private void validateMoveButton_Click(object sender, EventArgs e)
 {
// P
// nota pentru adnotarile de complexitate: 
//      "casute verzi" = celule completate corect
//      "casute rosii" = celule completate gresit
// IN
    int countRed = 0;
    int countGreen = 0;
    int countAllGreen = 0;
// P0
// P -> P0: s-au initializat variabilele de intrare
     for (int row = 0; row < 9; row++)       // C1
     {
         for (int column = 0; column < 9; column++)     // C2
         { 
// P0 & C1 & C2 -> P1: daca conditiile sunt indeplinite, se ajunge in bucla C1, respectiv C2
// P1
             var cellValue = sudokuDataGridView.Rows[row].Cells[column].Value; // A1
// P2: variabilei cellValue i se atribuie o celula din grid.
             if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))       // C3
             {
// P2 & C3 -> P3: daca celula e libera, se intra in C3
// P3
                 table[row, column] = 0; // A2
// P4: se initializeaza cu 0 respectiva celula
                 continue;     // A3 -> P1: Sare la urmatoarea iteratie a buclei C2 (sau C1, daca column == 8)   
             }

             else if (!int.TryParse(cellValue.ToString(), out int value) || value < 1 || value > 9)     // C4
             {
// P1 & (not C3) & C4 -> P5: se verifica un caz de eroare
// P5
                 MessageBox.Show($"Valoare invalidă în celula ({row + 1}, {column + 1}).");    // A4
// P6: se afiseaza un Message Box cu eroarea respectiva
                 return;    // A5 -> Iesire imediata din metoda.
             }

             else if (!IsMoveValid(row, column, value) && (sudokuDataGridView.Rows[row].Cells[column].Style.BackColor != System.Drawing.Color.LightGray) && (sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor != System.Drawing.Color.Green))      // C5
             {
// P1 & (not C3) & (not C4) & C5 -> P7: cazul in care nu e o miscare valida.
// P7
                 sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Red;     // A7
// P8: se coloreaza cu rosu celula
                 countRed++;    // A8
                 //MessageBox.Show($"Valoare din celula ({row + 1}, {column + 1}) este invalida.");
// P9: creste numarul de "casute rosii" 
             }
             else 
             {
// P1 & (not C3) & (not C4) & (not C5) -> P10: cazul in care e ok sa scrii in celula
// P10
                sudokuDataGridView.Rows[row].Cells[column].ReadOnly = true;     // A9
// P11: Gridul e facut read-only
                 if (sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor != System.Drawing.Color.Green)     //   C6
                 {
 // P11 & C6 -> P12: e o valoare corecta
 // P12
                     countAllGreen++;   // A10
// P13: creste numarul de "casute verzi" 
                     sudokuDataGridView.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Green;   // A11
// P14
                     if(sudokuDataGridView.Rows[row].Cells[column].Style.BackColor != System.Drawing.Color.LightGray)    // C7
// P14 & C7 -> P15:  daca nu e casuta gri (nescrisa inca), inseamna ca e verde
// P15
                         countGreen++;   // A12
// P16: creste numarul de "casute verzi" 
                 }
             }
         }
     }
// P17 : iesire din bucla "for" C1
     if(countAllGreen == 81)   // C8
     {
// P17 & C8 -> P18: toate casutele completate corect
// P18 
         MessageBox.Show("Felicitari! Ai castigat!\nAi ajuns la final si toate valorile sunt corecte!");  // A13
// P19: afisare message box 
     }
     
     else if (countRed == 0 && countGreen != 0)   // C9
     {
// P17 & (not C8) & C9 -> P20: nu sunt inca "casute rosii", iar cele verzi exista ( > 0)
// P20
         MessageBox.Show("Pana aici este bine. Poti continua.");  // A14
// P21: afisare message box 
     }

     else if(countRed != 0 && countGreen == 0) // C10
     {
// P17 & (not C8) & (not C9) & C10 -> P22: au aparut casute rosii
// P22
        MessageBox.Show("Din pacate ai gresit "+ countRed+" valori."); // A15
// P23: afisare message box 
     }

     else if (countRed != 0 && countGreen != 0)  // C11
     {
// P17 & (not C8) & (not C9) & (not C10) & C11 -> P24: exista celule gresite (si celule corecte)
// P24
         MessageBox.Show("Ups... Ai gresit "+ countRed + " valori.\nMai incearca.");  // A16
// P25: afisare message box 
     }

     else if (countRed == 0 && countGreen == 0)  // C12
     {
// P17 & (not C8) & (not C9) & (not C10) & (not C11) & C12 -> P26: celule necompletate
// P26
        MessageBox.Show("Inca nu ai completat nicio celula.\nTe rugam sa validezi dupa ce ai completat cateva celule.");   // A17
// P27: afisare message box 
     }
// Q: incheierea functiei
// P --A--> Q
 }