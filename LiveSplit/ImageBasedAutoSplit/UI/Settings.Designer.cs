using System;
using System.Windows.Forms;

namespace ImageBasedAutoSplit
{
    partial class ImageEditorDialog
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
            this.runGrid = new System.Windows.Forms.DataGridView();
            this.sourcesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.runGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // runGrid
            // 
            this.runGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runGrid.Location = new System.Drawing.Point(12, 139);
            this.runGrid.Name = "runGrid";
            this.runGrid.Size = new System.Drawing.Size(367, 315);
            this.runGrid.TabIndex = 0;
            this.runGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.runGrid_CellContentClick);
            // 
            // sourcesComboBox
            // 
            this.sourcesComboBox.FormattingEnabled = true;
            this.sourcesComboBox.Location = new System.Drawing.Point(164, 12);
            this.sourcesComboBox.Name = "sourcesComboBox";
            this.sourcesComboBox.Size = new System.Drawing.Size(215, 21);
            this.sourcesComboBox.TabIndex = 1;
            this.sourcesComboBox.SelectedIndexChanged += new System.EventHandler(this.sourcesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Capture Device to use:";
            // 
            // ImageEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 466);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourcesComboBox);
            this.Controls.Add(this.runGrid);
            this.Name = "ImageEditorDialog";
            this.Text = "Image Comparison";
            ((System.ComponentModel.ISupportInitialize)(this.runGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.DataGridView runGrid;
        private ComboBox sourcesComboBox;
        private Label label1;
    }
}

