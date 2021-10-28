
namespace TableCalculator
{
    partial class Window
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Button buttonRemoveColumn;
            System.Windows.Forms.Button buttonAddColumn;
            System.Windows.Forms.Button buttonRemoveRow;
            System.Windows.Forms.Button buttonAddRow;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expressionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            buttonRemoveColumn = new System.Windows.Forms.Button();
            buttonAddColumn = new System.Windows.Forms.Button();
            buttonRemoveRow = new System.Windows.Forms.Button();
            buttonAddRow = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Left;
            label1.Location = new System.Drawing.Point(84, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(19, 20);
            label1.TabIndex = 2;
            label1.Text = "=";
            // 
            // buttonRemoveColumn
            // 
            buttonRemoveColumn.Dock = System.Windows.Forms.DockStyle.Top;
            buttonRemoveColumn.Location = new System.Drawing.Point(0, 40);
            buttonRemoveColumn.Name = "buttonRemoveColumn";
            buttonRemoveColumn.Size = new System.Drawing.Size(40, 40);
            buttonRemoveColumn.TabIndex = 4;
            buttonRemoveColumn.TabStop = false;
            buttonRemoveColumn.Text = "-";
            buttonRemoveColumn.UseVisualStyleBackColor = true;
            buttonRemoveColumn.Click += new System.EventHandler(this.buttonRemoveColumn_Click);
            // 
            // buttonAddColumn
            // 
            buttonAddColumn.Dock = System.Windows.Forms.DockStyle.Top;
            buttonAddColumn.Location = new System.Drawing.Point(0, 0);
            buttonAddColumn.Name = "buttonAddColumn";
            buttonAddColumn.Size = new System.Drawing.Size(40, 40);
            buttonAddColumn.TabIndex = 5;
            buttonAddColumn.TabStop = false;
            buttonAddColumn.Text = "+";
            buttonAddColumn.UseVisualStyleBackColor = true;
            buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // buttonRemoveRow
            // 
            buttonRemoveRow.Dock = System.Windows.Forms.DockStyle.Left;
            buttonRemoveRow.Location = new System.Drawing.Point(40, 0);
            buttonRemoveRow.Name = "buttonRemoveRow";
            buttonRemoveRow.Size = new System.Drawing.Size(40, 40);
            buttonRemoveRow.TabIndex = 6;
            buttonRemoveRow.TabStop = false;
            buttonRemoveRow.Text = "-";
            buttonRemoveRow.UseVisualStyleBackColor = true;
            buttonRemoveRow.Click += new System.EventHandler(this.buttonRemoveRow_Click);
            // 
            // buttonAddRow
            // 
            buttonAddRow.Dock = System.Windows.Forms.DockStyle.Left;
            buttonAddRow.Location = new System.Drawing.Point(0, 0);
            buttonAddRow.Name = "buttonAddRow";
            buttonAddRow.Size = new System.Drawing.Size(40, 40);
            buttonAddRow.TabIndex = 6;
            buttonAddRow.TabStop = false;
            buttonAddRow.Text = "+";
            buttonAddRow.UseVisualStyleBackColor = true;
            buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxExpression);
            this.panel1.Controls.Add(label1);
            this.panel1.Controls.Add(this.textBoxId);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 30);
            this.panel1.TabIndex = 1;
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExpression.Location = new System.Drawing.Point(103, 0);
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(697, 27);
            this.textBoxExpression.TabIndex = 3;
            this.textBoxExpression.TabStop = false;
            this.textBoxExpression.TextChanged += new System.EventHandler(this.textBoxExpression_TextChanged);
            this.textBoxExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExpression_KeyDown);
            this.textBoxExpression.Leave += new System.EventHandler(this.textBoxExpression_Leave);
            // 
            // textBoxId
            // 
            this.textBoxId.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxId.Location = new System.Drawing.Point(0, 0);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(84, 27);
            this.textBoxId.TabIndex = 1;
            this.textBoxId.TabStop = false;
            this.textBoxId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxId_KeyDown);
            this.textBoxId.Leave += new System.EventHandler(this.textBoxId_Leave);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 58);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(760, 352);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView_ColumnWidthChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView_PreviewKeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(buttonRemoveColumn);
            this.panel2.Controls.Add(buttonAddColumn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(760, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(40, 352);
            this.panel2.TabIndex = 6;
            this.panel2.Click += new System.EventHandler(this.buttonRemoveRow_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(buttonRemoveRow);
            this.panel3.Controls.Add(buttonAddRow);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 410);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 40);
            this.panel3.TabIndex = 7;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.newToolStripMenuItem.Text = "Створити";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.openToolStripMenuItem.Text = "Відкрити";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.saveToolStripMenuItem.Text = "Зберегти";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.saveAsToolStripMenuItem.Text = "Зберегти як";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.closeToolStripMenuItem.Text = "Закрити";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expressionsToolStripMenuItem,
            this.valuesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.viewToolStripMenuItem.Text = "Вигляд";
            // 
            // expressionsToolStripMenuItem
            // 
            this.expressionsToolStripMenuItem.Name = "expressionsToolStripMenuItem";
            this.expressionsToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.expressionsToolStripMenuItem.Text = "Вирази";
            this.expressionsToolStripMenuItem.Click += new System.EventHandler(this.expressionsToolStripMenuItem_Click);
            // 
            // valuesToolStripMenuItem
            // 
            this.valuesToolStripMenuItem.Checked = true;
            this.valuesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.valuesToolStripMenuItem.Name = "valuesToolStripMenuItem";
            this.valuesToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.valuesToolStripMenuItem.Text = "Значення";
            this.valuesToolStripMenuItem.Click += new System.EventHandler(this.valuesToolStripMenuItem_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip);
            this.Name = "Window";
            this.Text = "Табличний калькулятор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonRemoveColumn;
        private System.Windows.Forms.Button buttonAddColumn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonRemoveRow;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expressionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valuesToolStripMenuItem;
    }
}