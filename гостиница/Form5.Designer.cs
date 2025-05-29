namespace гостиница
{
    partial class Form5
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
            this.label4 = new System.Windows.Forms.Label();
            this.add_organization = new System.Windows.Forms.Button();
            this.number_phone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.full_name_organization = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sale = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(184, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(416, 32);
            this.label4.TabIndex = 13;
            this.label4.Text = "Введите данные организации:";
            // 
            // add_organization
            // 
            this.add_organization.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_organization.Location = new System.Drawing.Point(535, 317);
            this.add_organization.Name = "add_organization";
            this.add_organization.Size = new System.Drawing.Size(185, 51);
            this.add_organization.TabIndex = 12;
            this.add_organization.Text = "Добавить";
            this.add_organization.UseVisualStyleBackColor = true;
            this.add_organization.Click += new System.EventHandler(this.add_organization_Click);
            // 
            // number_phone
            // 
            this.number_phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.number_phone.Location = new System.Drawing.Point(153, 192);
            this.number_phone.Name = "number_phone";
            this.number_phone.Size = new System.Drawing.Size(170, 28);
            this.number_phone.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(22, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 29);
            this.label2.TabIndex = 10;
            this.label2.Text = "Телефон:";
            // 
            // full_name_organization
            // 
            this.full_name_organization.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.full_name_organization.Location = new System.Drawing.Point(370, 131);
            this.full_name_organization.Name = "full_name_organization";
            this.full_name_organization.Size = new System.Drawing.Size(393, 28);
            this.full_name_organization.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(22, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Наименование организации:";
            // 
            // sale
            // 
            this.sale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sale.Location = new System.Drawing.Point(153, 253);
            this.sale.Name = "sale";
            this.sale.Size = new System.Drawing.Size(73, 28);
            this.sale.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(22, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "Скидка:";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.add_organization);
            this.Controls.Add(this.number_phone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.full_name_organization);
            this.Controls.Add(this.label1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button add_organization;
        private System.Windows.Forms.TextBox number_phone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox full_name_organization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sale;
        private System.Windows.Forms.Label label3;
    }
}