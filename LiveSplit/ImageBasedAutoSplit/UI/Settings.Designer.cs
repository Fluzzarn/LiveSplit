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
            ((System.ComponentModel.ISupportInitialize)(this.runGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // runGrid
            // 
            this.runGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runGrid.Location = new System.Drawing.Point(12, 139);
            this.runGrid.Name = "runGrid";
            this.runGrid.Size = new System.Drawing.Size(627, 315);
            this.runGrid.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 466);
            this.Controls.Add(this.runGrid);
            this.Name = "Form1";
            this.Text = "Image Comparison";
            ((System.ComponentModel.ISupportInitialize)(this.runGrid)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.DataGridView runGrid;
    }
}

