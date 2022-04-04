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
            this.moduleName_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.paramRequired_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.controllerName_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.routeUrl_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.paramType_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.jsonParam_txt = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ModuleName";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // moduleName_txt
            // 
            this.moduleName_txt.Location = new System.Drawing.Point(214, 44);
            this.moduleName_txt.Name = "moduleName_txt";
            this.moduleName_txt.Size = new System.Drawing.Size(414, 26);
            this.moduleName_txt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "ParamRequired";
            // 
            // paramRequired_txt
            // 
            this.paramRequired_txt.Location = new System.Drawing.Point(214, 93);
            this.paramRequired_txt.Name = "paramRequired_txt";
            this.paramRequired_txt.Size = new System.Drawing.Size(414, 26);
            this.paramRequired_txt.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "ControllerName";
            // 
            // controllerName_txt
            // 
            this.controllerName_txt.Location = new System.Drawing.Point(214, 142);
            this.controllerName_txt.Name = "controllerName_txt";
            this.controllerName_txt.Size = new System.Drawing.Size(414, 26);
            this.controllerName_txt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "RouteUrl";
            // 
            // routeUrl_txt
            // 
            this.routeUrl_txt.Location = new System.Drawing.Point(214, 192);
            this.routeUrl_txt.Name = "routeUrl_txt";
            this.routeUrl_txt.Size = new System.Drawing.Size(414, 26);
            this.routeUrl_txt.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "ParamType";
            // 
            // paramType_txt
            // 
            this.paramType_txt.Location = new System.Drawing.Point(214, 240);
            this.paramType_txt.Name = "paramType_txt";
            this.paramType_txt.Size = new System.Drawing.Size(414, 26);
            this.paramType_txt.TabIndex = 9;
            this.paramType_txt.TextChanged += new System.EventHandler(this.paramType_txt_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "JsonParam";
            // 
            // jsonParam_txt
            // 
            this.jsonParam_txt.Location = new System.Drawing.Point(214, 293);
            this.jsonParam_txt.Multiline = true;
            this.jsonParam_txt.Name = "jsonParam_txt";
            this.jsonParam_txt.Size = new System.Drawing.Size(414, 151);
            this.jsonParam_txt.TabIndex = 11;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(214, 494);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(428, 44);
            this.Add.TabIndex = 12;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // AddInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 594);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.jsonParam_txt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.paramType_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.routeUrl_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.controllerName_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.paramRequired_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moduleName_txt);
            this.Controls.Add(this.label1);
            this.Name = "AddInfo";
            this.Text = "AddInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox moduleName_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox paramRequired_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox controllerName_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox routeUrl_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox paramType_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox jsonParam_txt;
        private System.Windows.Forms.Button Add;
    }
}