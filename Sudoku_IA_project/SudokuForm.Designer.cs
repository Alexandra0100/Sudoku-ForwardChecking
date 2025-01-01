namespace Sudoku_IA_project
{
    partial class SudokuForm
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
            this.sudokuDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playButton = new System.Windows.Forms.Button();
            this.cleanButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.validateMoveButton = new System.Windows.Forms.Button();
            this.getHintButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.generateSolutionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sudokuDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sudokuDataGridView
            // 
            this.sudokuDataGridView.AllowUserToDeleteRows = false;
            this.sudokuDataGridView.AllowUserToResizeColumns = false;
            this.sudokuDataGridView.AllowUserToResizeRows = false;
            this.sudokuDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sudokuDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.sudokuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sudokuDataGridView.ColumnHeadersVisible = false;
            this.sudokuDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.sudokuDataGridView.Location = new System.Drawing.Point(56, 21);
            this.sudokuDataGridView.Name = "sudokuDataGridView";
            this.sudokuDataGridView.RowHeadersWidth = 4;
            this.sudokuDataGridView.RowTemplate.Height = 24;
            this.sudokuDataGridView.Size = new System.Drawing.Size(489, 437);
            this.sudokuDataGridView.TabIndex = 0;
            this.sudokuDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SudokuDataGridView_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MaxInputLength = 1;
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MaxInputLength = 1;
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.MaxInputLength = 1;
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.MaxInputLength = 1;
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.MaxInputLength = 1;
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.MaxInputLength = 1;
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.MaxInputLength = 1;
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.MaxInputLength = 1;
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.MaxInputLength = 1;
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(608, 55);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(180, 49);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Generate table";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(608, 299);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(180, 49);
            this.cleanButton.TabIndex = 2;
            this.cleanButton.Text = "Clean table";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(608, 409);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(180, 49);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // validateMoveButton
            // 
            this.validateMoveButton.Location = new System.Drawing.Point(608, 110);
            this.validateMoveButton.Name = "validateMoveButton";
            this.validateMoveButton.Size = new System.Drawing.Size(180, 49);
            this.validateMoveButton.TabIndex = 4;
            this.validateMoveButton.Text = "Validate move";
            this.validateMoveButton.UseVisualStyleBackColor = true;
            this.validateMoveButton.Click += new System.EventHandler(this.validateMoveButton_Click);
            // 
            // getHintButton
            // 
            this.getHintButton.Location = new System.Drawing.Point(608, 165);
            this.getHintButton.Name = "getHintButton";
            this.getHintButton.Size = new System.Drawing.Size(180, 49);
            this.getHintButton.TabIndex = 5;
            this.getHintButton.Text = "Get hint";
            this.getHintButton.UseVisualStyleBackColor = true;
            this.getHintButton.Click += new System.EventHandler(this.getHintButton_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(608, 354);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(180, 49);
            this.HelpButton.TabIndex = 6;
            this.HelpButton.Text = "Help";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // generateSolutionButton
            // 
            this.generateSolutionButton.Location = new System.Drawing.Point(608, 220);
            this.generateSolutionButton.Name = "generateSolutionButton";
            this.generateSolutionButton.Size = new System.Drawing.Size(180, 49);
            this.generateSolutionButton.TabIndex = 7;
            this.generateSolutionButton.Text = "Generate solution";
            this.generateSolutionButton.UseVisualStyleBackColor = true;
            this.generateSolutionButton.Click += new System.EventHandler(this.generateSolutionButton_Click);
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 470);
            this.Controls.Add(this.generateSolutionButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.getHintButton);
            this.Controls.Add(this.validateMoveButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.sudokuDataGridView);
            this.Name = "SudokuForm";
            this.Text = "Sudoku";
            ((System.ComponentModel.ISupportInitialize)(this.sudokuDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sudokuDataGridView;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button validateMoveButton;
        private System.Windows.Forms.Button getHintButton;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button generateSolutionButton;
    }
}

