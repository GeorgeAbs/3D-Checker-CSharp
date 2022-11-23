
namespace Проверка_3D_на_корректность__ГрРПЗР_
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
            this.chosePCF = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.startChecking = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.slope = new System.Windows.Forms.CheckBox();
            this.slopeNum = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.verticalNum = new System.Windows.Forms.ComboBox();
            this.vertical = new System.Windows.Forms.CheckBox();
            this.lenghOfStraight = new System.Windows.Forms.CheckBox();
            this.distanceWeldSup = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.distanceWeldSupNum = new System.Windows.Forms.ComboBox();
            this.anglesOfBends = new System.Windows.Forms.CheckBox();
            this.anglesOfBendsRound = new System.Windows.Forms.CheckBox();
            this.changingSlope = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chosePCF
            // 
            this.chosePCF.Location = new System.Drawing.Point(239, 12);
            this.chosePCF.Name = "chosePCF";
            this.chosePCF.Size = new System.Drawing.Size(161, 40);
            this.chosePCF.TabIndex = 1;
            this.chosePCF.Text = "Выбрать папку с изометриями (*.pcf)";
            this.chosePCF.UseVisualStyleBackColor = true;
            this.chosePCF.Click += new System.EventHandler(this.chosePCF_Click);
            // 
            // startChecking
            // 
            this.startChecking.Location = new System.Drawing.Point(239, 452);
            this.startChecking.Name = "startChecking";
            this.startChecking.Size = new System.Drawing.Size(151, 78);
            this.startChecking.TabIndex = 2;
            this.startChecking.Text = "Проверить\r\n(отчет будет сохранен в блокноте Результаты проверки в выбранной папке" +
    ")";
            this.startChecking.UseVisualStyleBackColor = true;
            this.startChecking.Click += new System.EventHandler(this.startChecking_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Проверить:";
            // 
            // slope
            // 
            this.slope.AutoSize = true;
            this.slope.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.slope.Location = new System.Drawing.Point(12, 122);
            this.slope.Name = "slope";
            this.slope.Size = new System.Drawing.Size(266, 19);
            this.slope.TabIndex = 4;
            this.slope.Text = "Уклон       Должен быть больше или равен:";
            this.slope.UseVisualStyleBackColor = true;
            // 
            // slopeNum
            // 
            this.slopeNum.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.slopeNum.FormattingEnabled = true;
            this.slopeNum.Items.AddRange(new object[] {
            "0",
            "4",
            "написать свой"});
            this.slopeNum.Location = new System.Drawing.Point(284, 118);
            this.slopeNum.Name = "slopeNum";
            this.slopeNum.Size = new System.Drawing.Size(71, 23);
            this.slopeNum.TabIndex = 5;
            this.slopeNum.Text = "4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(361, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "мм/м";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(413, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "мм";
            // 
            // verticalNum
            // 
            this.verticalNum.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.verticalNum.FormattingEnabled = true;
            this.verticalNum.Items.AddRange(new object[] {
            "0",
            "0.1",
            "1",
            "написать свой"});
            this.verticalNum.Location = new System.Drawing.Point(336, 157);
            this.verticalNum.Name = "verticalNum";
            this.verticalNum.Size = new System.Drawing.Size(71, 23);
            this.verticalNum.TabIndex = 8;
            this.verticalNum.Text = "1";
            // 
            // vertical
            // 
            this.vertical.AutoSize = true;
            this.vertical.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vertical.Location = new System.Drawing.Point(12, 161);
            this.vertical.Name = "vertical";
            this.vertical.Size = new System.Drawing.Size(318, 19);
            this.vertical.TabIndex = 7;
            this.vertical.Text = "Вертикальность       Отклонение по X или Y не более:";
            this.vertical.UseVisualStyleBackColor = true;
            // 
            // lenghOfStraight
            // 
            this.lenghOfStraight.AutoSize = true;
            this.lenghOfStraight.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lenghOfStraight.Location = new System.Drawing.Point(12, 205);
            this.lenghOfStraight.Name = "lenghOfStraight";
            this.lenghOfStraight.Size = new System.Drawing.Size(408, 19);
            this.lenghOfStraight.TabIndex = 10;
            this.lenghOfStraight.Text = "Длины прямых участков  (Для Dn>=100 = 100мм, для Dn<100 = Dn)";
            this.lenghOfStraight.UseVisualStyleBackColor = true;
            // 
            // distanceWeldSup
            // 
            this.distanceWeldSup.AutoSize = true;
            this.distanceWeldSup.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.distanceWeldSup.Location = new System.Drawing.Point(11, 253);
            this.distanceWeldSup.Name = "distanceWeldSup";
            this.distanceWeldSup.Size = new System.Drawing.Size(514, 19);
            this.distanceWeldSup.TabIndex = 11;
            this.distanceWeldSup.Text = "Расстояние от логической опоры до сварного шва         Должно быть более или равн" +
    "о:";
            this.distanceWeldSup.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(608, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "мм";
            // 
            // distanceWeldSupNum
            // 
            this.distanceWeldSupNum.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.distanceWeldSupNum.FormattingEnabled = true;
            this.distanceWeldSupNum.Items.AddRange(new object[] {
            "30",
            "50",
            "80",
            "100",
            "написать свое"});
            this.distanceWeldSupNum.Location = new System.Drawing.Point(531, 249);
            this.distanceWeldSupNum.Name = "distanceWeldSupNum";
            this.distanceWeldSupNum.Size = new System.Drawing.Size(71, 23);
            this.distanceWeldSupNum.TabIndex = 12;
            this.distanceWeldSupNum.Text = "50";
            // 
            // anglesOfBends
            // 
            this.anglesOfBends.AutoSize = true;
            this.anglesOfBends.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.anglesOfBends.Location = new System.Drawing.Point(11, 300);
            this.anglesOfBends.Name = "anglesOfBends";
            this.anglesOfBends.Size = new System.Drawing.Size(435, 19);
            this.anglesOfBends.TabIndex = 14;
            this.anglesOfBends.Text = "Углы отводов        Показать отводы, у которых угол не кратен 5 градусам";
            this.anglesOfBends.UseVisualStyleBackColor = true;
            // 
            // anglesOfBendsRound
            // 
            this.anglesOfBendsRound.AutoSize = true;
            this.anglesOfBendsRound.Checked = true;
            this.anglesOfBendsRound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.anglesOfBendsRound.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.anglesOfBendsRound.Location = new System.Drawing.Point(456, 292);
            this.anglesOfBendsRound.Name = "anglesOfBendsRound";
            this.anglesOfBendsRound.Size = new System.Drawing.Size(160, 34);
            this.anglesOfBendsRound.TabIndex = 15;
            this.anglesOfBendsRound.Text = "Округлять\r\n до ближайшего целого";
            this.anglesOfBendsRound.UseVisualStyleBackColor = true;
            // 
            // changingSlope
            // 
            this.changingSlope.AutoSize = true;
            this.changingSlope.Checked = true;
            this.changingSlope.CheckState = System.Windows.Forms.CheckState.Checked;
            this.changingSlope.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changingSlope.Location = new System.Drawing.Point(11, 348);
            this.changingSlope.Name = "changingSlope";
            this.changingSlope.Size = new System.Drawing.Size(391, 19);
            this.changingSlope.TabIndex = 16;
            this.changingSlope.Text = "Изменение направления уклона (только на отводах и тройниках)";
            this.changingSlope.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(746, 86);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(402, 384);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 542);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.changingSlope);
            this.Controls.Add(this.anglesOfBendsRound);
            this.Controls.Add(this.anglesOfBends);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.distanceWeldSupNum);
            this.Controls.Add(this.distanceWeldSup);
            this.Controls.Add(this.lenghOfStraight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.verticalNum);
            this.Controls.Add(this.vertical);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.slopeNum);
            this.Controls.Add(this.slope);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startChecking);
            this.Controls.Add(this.chosePCF);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chosePCF;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button startChecking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox slope;
        private System.Windows.Forms.ComboBox slopeNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox verticalNum;
        private System.Windows.Forms.CheckBox vertical;
        private System.Windows.Forms.CheckBox lenghOfStraight;
        private System.Windows.Forms.CheckBox distanceWeldSup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox distanceWeldSupNum;
        private System.Windows.Forms.CheckBox anglesOfBends;
        private System.Windows.Forms.CheckBox anglesOfBendsRound;
        private System.Windows.Forms.CheckBox changingSlope;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

