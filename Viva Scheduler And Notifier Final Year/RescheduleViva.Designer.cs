namespace Viva_Scheduler_And_Notifier_Final_Year
{
    partial class RescheduleViva
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PickerDate = new System.Windows.Forms.DateTimePicker();
            this.PickerTime = new System.Windows.Forms.DateTimePicker();
            this.btnReschedule = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reschedule Viva";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(78, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Time";
            // 
            // PickerDate
            // 
            this.PickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDate.Location = new System.Drawing.Point(150, 88);
            this.PickerDate.Name = "PickerDate";
            this.PickerDate.Size = new System.Drawing.Size(281, 26);
            this.PickerDate.TabIndex = 3;
            this.PickerDate.Value = new System.DateTime(2020, 12, 18, 0, 0, 0, 0);
            // 
            // PickerTime
            // 
            this.PickerTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.PickerTime.Location = new System.Drawing.Point(150, 124);
            this.PickerTime.Name = "PickerTime";
            this.PickerTime.ShowUpDown = true;
            this.PickerTime.Size = new System.Drawing.Size(281, 26);
            this.PickerTime.TabIndex = 4;
            this.PickerTime.Value = new System.DateTime(2020, 12, 18, 15, 21, 0, 0);
            // 
            // btnReschedule
            // 
            this.btnReschedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReschedule.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReschedule.IconChar = FontAwesome.Sharp.IconChar.CalendarAlt;
            this.btnReschedule.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnReschedule.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReschedule.IconSize = 25;
            this.btnReschedule.Location = new System.Drawing.Point(299, 169);
            this.btnReschedule.Margin = new System.Windows.Forms.Padding(25);
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Size = new System.Drawing.Size(132, 35);
            this.btnReschedule.TabIndex = 8;
            this.btnReschedule.Text = "Re-schedule";
            this.btnReschedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReschedule.UseVisualStyleBackColor = true;
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);
            // 
            // RescheduleViva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(519, 226);
            this.Controls.Add(this.btnReschedule);
            this.Controls.Add(this.PickerTime);
            this.Controls.Add(this.PickerDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.MaximumSize = new System.Drawing.Size(535, 265);
            this.MinimumSize = new System.Drawing.Size(535, 265);
            this.Name = "RescheduleViva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RescheduleViva";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker PickerDate;
        private System.Windows.Forms.DateTimePicker PickerTime;
        private FontAwesome.Sharp.IconButton btnReschedule;
    }
}