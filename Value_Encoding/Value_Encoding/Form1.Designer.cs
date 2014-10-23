namespace Value_Encoding
{
    partial class Form1
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
            this.initialPopulationButton = new System.Windows.Forms.Button();
            this.nextGenerationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // initialPopulationButton
            // 
            this.initialPopulationButton.Location = new System.Drawing.Point(34, 28);
            this.initialPopulationButton.Name = "initialPopulationButton";
            this.initialPopulationButton.Size = new System.Drawing.Size(105, 23);
            this.initialPopulationButton.TabIndex = 0;
            this.initialPopulationButton.Text = "Initial population";
            this.initialPopulationButton.UseVisualStyleBackColor = true;
            this.initialPopulationButton.Click += new System.EventHandler(this.initialPopulationButton_Click);
            // 
            // nextGenerationButton
            // 
            this.nextGenerationButton.Location = new System.Drawing.Point(34, 57);
            this.nextGenerationButton.Name = "nextGenerationButton";
            this.nextGenerationButton.Size = new System.Drawing.Size(105, 23);
            this.nextGenerationButton.TabIndex = 1;
            this.nextGenerationButton.Text = "Next Generation";
            this.nextGenerationButton.UseVisualStyleBackColor = true;
            this.nextGenerationButton.Click += new System.EventHandler(this.nextGenerationButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 262);
            this.Controls.Add(this.nextGenerationButton);
            this.Controls.Add(this.initialPopulationButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button initialPopulationButton;
        private System.Windows.Forms.Button nextGenerationButton;
    }
}

