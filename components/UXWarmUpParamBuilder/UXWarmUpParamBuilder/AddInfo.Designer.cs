namespace UXWarmUpParamBuilder
{
    partial class AddInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAddRoute = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAddParam = new System.Windows.Forms.TextBox();
            this.checkBoxAddStatus = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxParamType = new System.Windows.Forms.ComboBox();
            this.comboBoxModuleName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.Add.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.Color.White;
            this.Add.Location = new System.Drawing.Point(202, 626);
            this.Add.Margin = new System.Windows.Forms.Padding(0);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(503, 50);
            this.Add.TabIndex = 12;
            this.Add.Text = "ADD";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "Route Url";
            // 
            // textBoxAddRoute
            // 
            this.textBoxAddRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddRoute.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAddRoute.Location = new System.Drawing.Point(204, 74);
            this.textBoxAddRoute.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxAddRoute.Name = "textBoxAddRoute";
            this.textBoxAddRoute.Size = new System.Drawing.Size(500, 34);
            this.textBoxAddRoute.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 28);
            this.label3.TabIndex = 15;
            this.label3.Text = "Json Parameters";
            // 
            // textBoxAddParam
            // 
            this.textBoxAddParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddParam.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAddParam.Location = new System.Drawing.Point(204, 177);
            this.textBoxAddParam.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxAddParam.Multiline = true;
            this.textBoxAddParam.Name = "textBoxAddParam";
            this.textBoxAddParam.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxAddParam.Size = new System.Drawing.Size(500, 380);
            this.textBoxAddParam.TabIndex = 16;
            // 
            // checkBoxAddStatus
            // 
            this.checkBoxAddStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAddStatus.AutoSize = true;
            this.checkBoxAddStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAddStatus.Location = new System.Drawing.Point(204, 572);
            this.checkBoxAddStatus.Name = "checkBoxAddStatus";
            this.checkBoxAddStatus.Size = new System.Drawing.Size(93, 32);
            this.checkBoxAddStatus.TabIndex = 18;
            this.checkBoxAddStatus.Text = "Status";
            this.checkBoxAddStatus.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 28);
            this.label4.TabIndex = 19;
            this.label4.Text = "Parameter Type";
            // 
            // comboBoxParamType
            // 
            this.comboBoxParamType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxParamType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxParamType.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxParamType.FormattingEnabled = true;
            this.comboBoxParamType.Location = new System.Drawing.Point(202, 124);
            this.comboBoxParamType.Name = "comboBoxParamType";
            this.comboBoxParamType.Size = new System.Drawing.Size(503, 36);
            this.comboBoxParamType.TabIndex = 20;
            // 
            // comboBoxModuleName
            // 
            this.comboBoxModuleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxModuleName.AutoCompleteCustomSource.AddRange(new string[] {
            "Administration"});
            this.comboBoxModuleName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxModuleName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModuleName.FormattingEnabled = true;
            this.comboBoxModuleName.Location = new System.Drawing.Point(204, 23);
            this.comboBoxModuleName.Name = "comboBoxModuleName";
            this.comboBoxModuleName.Size = new System.Drawing.Size(503, 36);
            this.comboBoxModuleName.TabIndex = 21;
            // 
            // AddInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(815, 692);
            this.Controls.Add(this.comboBoxModuleName);
            this.Controls.Add(this.comboBoxParamType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxAddStatus);
            this.Controls.Add(this.textBoxAddParam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAddRoute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.label1);
            this.Name = "AddInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Action Parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAddRoute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAddParam;
        private System.Windows.Forms.CheckBox checkBoxAddStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxParamType;
        private System.Windows.Forms.ComboBox comboBoxModuleName;
    }
}