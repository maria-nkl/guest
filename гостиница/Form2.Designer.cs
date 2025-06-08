namespace гостиница
{
    partial class Form2
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
            this.number = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FIO = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.kolvo_mest = new System.Windows.Forms.Label();
            this.room_category = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.data_start = new System.Windows.Forms.Label();
            this.data_end = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.Label();
            this.Text_task = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.task = new System.Windows.Forms.Button();
            this.completed = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.floor = new System.Windows.Forms.Label();
            this.checkedListBoxServices = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // number
            // 
            this.number.AutoSize = true;
            this.number.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.number.Location = new System.Drawing.Point(355, 9);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(52, 29);
            this.number.TabIndex = 0;
            this.number.Text = "111";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(251, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(23, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Клиент:";
            // 
            // FIO
            // 
            this.FIO.AutoSize = true;
            this.FIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FIO.Location = new System.Drawing.Point(107, 159);
            this.FIO.Name = "FIO";
            this.FIO.Size = new System.Drawing.Size(209, 22);
            this.FIO.TabIndex = 3;
            this.FIO.Text = "Иванов Иван Иванович";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(471, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Категория номера:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(24, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "Количество мест:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // kolvo_mest
            // 
            this.kolvo_mest.AutoSize = true;
            this.kolvo_mest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.kolvo_mest.Location = new System.Drawing.Point(195, 76);
            this.kolvo_mest.Name = "kolvo_mest";
            this.kolvo_mest.Size = new System.Drawing.Size(20, 22);
            this.kolvo_mest.TabIndex = 4;
            this.kolvo_mest.Text = "2";
            // 
            // room_category
            // 
            this.room_category.AutoSize = true;
            this.room_category.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.room_category.Location = new System.Drawing.Point(656, 56);
            this.room_category.Name = "room_category";
            this.room_category.Size = new System.Drawing.Size(90, 22);
            this.room_category.TabIndex = 5;
            this.room_category.Text = "Стандарт";
            this.room_category.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(24, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 22);
            this.label7.TabIndex = 2;
            this.label7.Text = "Даты проживания: c";
            // 
            // data_start
            // 
            this.data_start.AutoSize = true;
            this.data_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.data_start.Location = new System.Drawing.Point(228, 224);
            this.data_start.Name = "data_start";
            this.data_start.Size = new System.Drawing.Size(100, 22);
            this.data_start.TabIndex = 4;
            this.data_start.Text = "03.05.2025";
            // 
            // data_end
            // 
            this.data_end.AutoSize = true;
            this.data_end.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.data_end.Location = new System.Drawing.Point(375, 224);
            this.data_end.Name = "data_end";
            this.data_end.Size = new System.Drawing.Size(100, 22);
            this.data_end.TabIndex = 4;
            this.data_end.Text = "03.05.2025";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(334, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 22);
            this.label10.TabIndex = 4;
            this.label10.Text = "до";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(485, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 22);
            this.label11.TabIndex = 2;
            this.label11.Text = "Услуги:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(23, 191);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(192, 22);
            this.label12.TabIndex = 2;
            this.label12.Text = "Контактный телефон:";
            // 
            // phone
            // 
            this.phone.AutoSize = true;
            this.phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.phone.Location = new System.Drawing.Point(229, 191);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(120, 22);
            this.phone.TabIndex = 2;
            this.phone.Text = "89134344090";
            // 
            // Text_task
            // 
            this.Text_task.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Text_task.Location = new System.Drawing.Point(27, 319);
            this.Text_task.Name = "Text_task";
            this.Text_task.Size = new System.Drawing.Size(720, 71);
            this.Text_task.TabIndex = 7;
            this.Text_task.Text = "";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(23, 294);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(205, 22);
            this.label14.TabIndex = 2;
            this.label14.Text = "Заявка для персонала:";
            // 
            // task
            // 
            this.task.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.task.Location = new System.Drawing.Point(586, 401);
            this.task.Name = "task";
            this.task.Size = new System.Drawing.Size(160, 40);
            this.task.TabIndex = 8;
            this.task.Text = "Отправить";
            this.task.UseVisualStyleBackColor = true;
            this.task.Click += new System.EventHandler(this.task_Click);
            // 
            // completed
            // 
            this.completed.Enabled = false;
            this.completed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.completed.Location = new System.Drawing.Point(28, 401);
            this.completed.Name = "completed";
            this.completed.Size = new System.Drawing.Size(160, 40);
            this.completed.TabIndex = 8;
            this.completed.Text = "Устранено";
            this.completed.UseVisualStyleBackColor = true;
            this.completed.Click += new System.EventHandler(this.completed_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(24, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 22);
            this.label15.TabIndex = 2;
            this.label15.Text = "Этаж:";
            this.label15.Click += new System.EventHandler(this.label4_Click);
            // 
            // floor
            // 
            this.floor.AutoSize = true;
            this.floor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.floor.Location = new System.Drawing.Point(90, 54);
            this.floor.Name = "floor";
            this.floor.Size = new System.Drawing.Size(20, 22);
            this.floor.TabIndex = 4;
            this.floor.Text = "2";
            // 
            // checkedListBoxServices
            // 
            this.checkedListBoxServices.Enabled = false;
            this.checkedListBoxServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxServices.FormattingEnabled = true;
            this.checkedListBoxServices.Items.AddRange(new object[] {
            "Завтрак",
            "Ужин",
            "Уборка",
            "Развлечения"});
            this.checkedListBoxServices.Location = new System.Drawing.Point(572, 94);
            this.checkedListBoxServices.Name = "checkedListBoxServices";
            this.checkedListBoxServices.Size = new System.Drawing.Size(152, 119);
            this.checkedListBoxServices.TabIndex = 44;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 461);
            this.Controls.Add(this.checkedListBoxServices);
            this.Controls.Add(this.completed);
            this.Controls.Add(this.task);
            this.Controls.Add(this.Text_task);
            this.Controls.Add(this.room_category);
            this.Controls.Add(this.data_end);
            this.Controls.Add(this.data_start);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.floor);
            this.Controls.Add(this.kolvo_mest);
            this.Controls.Add(this.FIO);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.number);
            this.Name = "Form2";
            this.Text = "Свойства комнаты";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label number;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FIO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label kolvo_mest;
        private System.Windows.Forms.Label room_category;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label data_start;
        private System.Windows.Forms.Label data_end;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label phone;
        private System.Windows.Forms.RichTextBox Text_task;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button task;
        private System.Windows.Forms.Button completed;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label floor;
        private System.Windows.Forms.CheckedListBox checkedListBoxServices;
    }
}