
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
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Panel panel3;
            System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem possibilitiesToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem expressionsToolStripMenuItem1;
            System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.expressionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            label1 = new System.Windows.Forms.Label();
            buttonRemoveColumn = new System.Windows.Forms.Button();
            buttonAddColumn = new System.Windows.Forms.Button();
            buttonRemoveRow = new System.Windows.Forms.Button();
            buttonAddRow = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            possibilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            expressionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
            buttonRemoveColumn.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            buttonAddColumn.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            buttonRemoveRow.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            buttonAddRow.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
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
            panel1.Controls.Add(this.textBoxExpression);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(this.textBoxId);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 28);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(800, 30);
            panel1.TabIndex = 1;
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
            this.textBoxExpression.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxExpression_Validating);
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
            // panel2
            // 
            panel2.Controls.Add(buttonRemoveColumn);
            panel2.Controls.Add(buttonAddColumn);
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(760, 58);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(40, 352);
            panel2.TabIndex = 6;
            panel2.Click += new System.EventHandler(this.buttonRemoveRow_Click);
            // 
            // panel3
            // 
            panel3.Controls.Add(buttonRemoveRow);
            panel3.Controls.Add(buttonAddRow);
            panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel3.Location = new System.Drawing.Point(0, 410);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(800, 40);
            panel3.TabIndex = 7;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            newToolStripMenuItem,
            openToolStripMenuItem,
            saveToolStripMenuItem,
            saveAsToolStripMenuItem,
            closeToolStripMenuItem});
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            newToolStripMenuItem.Text = "Створити";
            newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            openToolStripMenuItem.Text = "Відкрити";
            openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            saveToolStripMenuItem.Text = "Зберегти";
            saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            saveAsToolStripMenuItem.Text = "Зберегти як";
            saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            closeToolStripMenuItem.Text = "Закрити";
            closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expressionsToolStripMenuItem,
            this.valuesToolStripMenuItem});
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            viewToolStripMenuItem.Text = "Вигляд";
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
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            possibilitiesToolStripMenuItem,
            expressionsToolStripMenuItem1,
            aboutToolStripMenuItem});
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            helpToolStripMenuItem.Text = "Довідка";
            // 
            // possibilitiesToolStripMenuItem
            // 
            possibilitiesToolStripMenuItem.Name = "possibilitiesToolStripMenuItem";
            possibilitiesToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            possibilitiesToolStripMenuItem.Text = "Можливості";
            possibilitiesToolStripMenuItem.Click += new System.EventHandler(this.possibilitiesToolStripMenuItem_Click);
            // 
            // expressionsToolStripMenuItem1
            // 
            expressionsToolStripMenuItem1.Name = "expressionsToolStripMenuItem1";
            expressionsToolStripMenuItem1.Size = new System.Drawing.Size(203, 26);
            expressionsToolStripMenuItem1.Text = "Формат виразів";
            expressionsToolStripMenuItem1.Click += new System.EventHandler(this.expressionsToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            aboutToolStripMenuItem.Text = "Про програму";
            aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 58);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(760, 352);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.TabStop = false;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView_ColumnWidthChanged);
            this.dataGridView.CurrentCellChanged += new System.EventHandler(this.dataGridView_CurrentCellChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem,
            viewToolStripMenuItem,
            helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(panel2);
            this.Controls.Add(panel3);
            this.Controls.Add(panel1);
            this.Controls.Add(this.menuStrip);
            this.Name = "Window";
            this.Text = "Табличний калькулятор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem expressionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valuesToolStripMenuItem;
    }
}