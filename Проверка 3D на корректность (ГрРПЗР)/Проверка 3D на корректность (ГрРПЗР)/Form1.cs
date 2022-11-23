using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Проверка_3D_на_корректность__ГрРПЗР_
{
    public partial class Form1 : Form
    {
        string PCFsPath;
        FileInfo[] files;
        public Form1()
        {
            InitializeComponent();
        }

        private void chosePCF_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PCFsPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void startChecking_Click(object sender, EventArgs e)
        {
            if (File.Exists(PCFsPath + "\\Результаты_проверки_изометрий.txt") == true) File.Delete(PCFsPath + "\\Результаты_проверки_изометрий.txt");

            DirectoryInfo dir = new DirectoryInfo(PCFsPath);
            files = dir.GetFiles("*.pcf");

            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("***В отчете приводится только то, что не удовлетворяет выбранным правилам проверки***");
                sw.WriteLine("");
                sw.WriteLine("");
            }

            if (slope.Checked == true)
            {
                slope_do();
            }
            if (lenghOfStraight.Checked == true)
            {
                lenghOfStraight_do();
            }
            if (distanceWeldSup.Checked == true)
            {
                distanceWeldSup_do();
            }
            if (anglesOfBends.Checked == true)
            {
                anglesOfBends_do();
            }
            if (changingSlope.Checked == true)
            {
                changingSlope_do();
            }

            Process.Start(PCFsPath + "\\Результаты_проверки_изометрий.txt");

        }
        private void slope_do()
        {
            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("____________________Проверка уклона______________________");
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                foreach (FileInfo file in files)
                {
                    int i = 0;
                    string[] tempPCF = File.ReadAllLines(file.FullName, Encoding.GetEncoding(1251));
                    while (i < tempPCF.Length - 1)
                    {
                        if (tempPCF[i] == "PIPE")
                        {
                            int ii = i;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                            ii++;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            double deltaPlane = Math.Sqrt((X2 - X1)* (X2 - X1) + (Y2 - Y1)* (Y2 - Y1));
                            double deltaX = Math.Abs(X2 - X1);
                            double deltaY = Math.Abs(Y2 - Y1);
                            double deltaZ = Math.Abs(Z2 - Z1);
                            double thisSlope = deltaZ / deltaPlane * 1000;
                            if (thisSlope < Double.Parse(slopeNum.SelectedItem.ToString(), CultureInfo.InvariantCulture) & thisSlope < (Double.Parse(slopeNum.SelectedItem.ToString(), CultureInfo.InvariantCulture) - 0.002))
                            {
                                sw.WriteLine("Труба: " + file.Name + "    Уклон: " + (thisSlope).ToString(CultureInfo.InvariantCulture) + " мм/м");
                                sw.WriteLine("Длина участка: " + Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)).ToString(CultureInfo.InvariantCulture) + " мм");
                                sw.WriteLine("Координаты одного из концов участка: " + "X: " + (X1).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("");
                            }
                            if (deltaZ / deltaPlane > 10 & vertical.Checked == true & (deltaX > Double.Parse(verticalNum.SelectedItem.ToString(), CultureInfo.InvariantCulture) | deltaY > Double.Parse(verticalNum.SelectedItem.ToString(), CultureInfo.InvariantCulture)))
                            {
                                sw.WriteLine("Труба: " + file.Name + "    Уклон: " + (thisSlope).ToString(CultureInfo.InvariantCulture) + " мм/м" + " - Труба не вертикальна");
                                sw.WriteLine("Длина участка: " + Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)).ToString(CultureInfo.InvariantCulture) + " мм");
                                sw.WriteLine("Координаты одного из концов участка: " + "X: " + (X1).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("");
                            }
                        }
                        i++;
                    }
                }
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
            }
        }
        
        private void lenghOfStraight_do()
        {
            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("____________________Проверка длин прямых участков______________________");
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                foreach (FileInfo file in files)
                {
                    int i = 0;
                    string[] tempPCF = File.ReadAllLines(file.FullName, Encoding.GetEncoding(1251));
                    while (i < tempPCF.Length - 1)
                    {
                        if (tempPCF[i] == "PIPE")
                        {
                            int ii = i;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            
                            double X1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            double DN = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[4], CultureInfo.InvariantCulture);

                            ii++;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            double length = Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1));

                            while (tempPCF[i].Contains("ITEM-DESCRIPTION") != true)
                            {
                                i++;
                            }
                            double diam = Double.Parse(tempPCF[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                [tempPCF[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length - 1].
                                Split(new[] { "x" }, StringSplitOptions.RemoveEmptyEntries)[0], CultureInfo.InvariantCulture);

                            if (DN >= 100 & Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)) <100)
                            {
                                sw.WriteLine("Труба: " + file.Name + "    DN: " + (DN).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("Длина участка: " + Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)).ToString(CultureInfo.InvariantCulture) + " мм");
                                sw.WriteLine("Координаты одного из концов участка: " + "X: " + (X1).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("");
                            }
                            if (DN < 100 & Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)) < diam)
                            {
                                sw.WriteLine("Труба: " + file.Name + "    Наружный диаметр трубы: " + (diam).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("Длина участка: " + Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1)).ToString(CultureInfo.InvariantCulture) + " мм");
                                sw.WriteLine("Координаты одного из концов участка: " + "X: " + (X1).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("");
                            }
                            
                        }
                        i++;
                    }
                }
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
            }
        }
        private void distanceWeldSup_do()
        {
            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("CTRL bend_ang = 4.5");

            }
        }
        private void anglesOfBends_do()
        {
            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("____________________Проверка отводов______________________");
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                foreach (FileInfo file in files)
                {
                    int i = 0;
                    string[] tempPCF = File.ReadAllLines(file.FullName, Encoding.GetEncoding(1251));
                    while (i < tempPCF.Length - 1)
                    {
                        if (tempPCF[i] == "ELBOW")
                        {
                            int ii = i;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                            ii++;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                            ii++;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "CENTRE-POINT")
                            {
                                ii++;
                            }
                            double X3 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y3 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z3 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            
                            double deltaX1 = X3 - X1;
                            double deltaY1 = Y3 - Y1;
                            double deltaZ1 = Z3 - Z1;
                            double deltaX2 = X2 - X3;
                            double deltaY2 = Y2 - Y3;
                            double deltaZ2 = Z2 - Z3;

                            double multi = deltaX1 * deltaX2 + deltaY1 * deltaY2 + deltaZ1 * deltaZ2;
                            double first = Math.Sqrt(deltaX1 * deltaX1 + deltaY1 * deltaY1 + deltaZ1 * deltaZ1);
                            double second = Math.Sqrt(deltaX2 * deltaX2 + deltaY2 * deltaY2 + deltaZ2 * deltaZ2);
                            double cos = multi / (first * second);
                            double angle = Math.Acos(cos)* 180 / Math.PI;
                            if (anglesOfBendsRound.Checked == true)
                            {
                                angle = Math.Round(angle);
                            }
                            if (angle/5 != 0 & angle / 5 != 1 & angle / 5 != 2 & angle / 5 != 3 & angle / 5 != 4 & angle / 5 != 5 &
                                angle / 5 != 6 & angle / 5 != 7 & angle / 5 != 8 & angle / 5 != 9 & angle / 5 != 10 & angle / 5 != 11 &
                                angle / 5 != 12 & angle / 5 != 13 & angle / 5 != 14 & angle / 5 != 15 & angle / 5 != 16 & angle / 5 != 17 & angle / 5 != 18)
                            {
                                sw.WriteLine("Отвод: " + file.Name + "    Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1).ToString(CultureInfo.InvariantCulture));
                                sw.WriteLine("");
                            }
                            
                        }
                        i++;
                    }
                }
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
            }
        }
        private void changingSlope_do()
        {
            using (StreamWriter sw = new StreamWriter(PCFsPath + "\\Результаты_проверки_изометрий.txt", true))
            {
                sw.WriteLine("____________________Проверка смены направления уклона______________________");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("");
                foreach (FileInfo file in files)
                {
                    int i = 0;
                    string[] tempPCF = File.ReadAllLines(file.FullName, Encoding.GetEncoding(1251));
                    while (i < tempPCF.Length - 1)
                    {
                        if (tempPCF[i] == "PIPE")
                        {
                            int ii = i;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }

                            double X1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z1 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            double DN = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[4], CultureInfo.InvariantCulture);

                            ii++;
                            while (tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                            {
                                ii++;
                            }
                            double X2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                            double Y2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                            double Z2 = Double.Parse(tempPCF[ii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                            double length = Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1) + (Z2 - Z1) * (Z2 - Z1));
                            int tt = 0;
                            while (tt < tempPCF.Length - 1)
                            {
                                if (tempPCF[tt] == "ELBOW")
                                {
                                    int iii = tt;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                                    {
                                        iii++;
                                    }
                                    double X1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                                    double Y1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                                    double Z1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                                    iii++;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                                    {
                                        iii++;
                                    }
                                    double X2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                                    double Y2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                                    double Z2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                                    iii++;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "CENTRE-POINT")
                                    {
                                        iii++;
                                    }
                                    double XcB = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                                    double YcB = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                                    double ZcB = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);

                                    //richTextBox1.Text = richTextBox1.Text + "Труба 1 конец " + X1 + "  " + Y1 + "  " + Z1 + "\n";
                                    //richTextBox1.Text = richTextBox1.Text + "Труба 2 конец " + X2 + "  " + Y2 + "  " + Z2 + "\n";
                                    //richTextBox1.Text = richTextBox1.Text + "Отвод 1 конец " + X1B + "  " + Y1B + "  " + Z1B + "\n";
                                    //richTextBox1.Text = richTextBox1.Text + "Отвод 2 конец " + X2B + "  " + Y2B + "  " + Z2B + "\n";
                                    double deltaX1 = XcB - X1B;
                                    double deltaY1 = YcB - Y1B;
                                    double deltaZ1 = ZcB - Z1B;
                                    double deltaX2 = X2B - XcB;
                                    double deltaY2 = Y2B - YcB;
                                    double deltaZ2 = Z2B - ZcB;

                                    double multi = deltaX1 * deltaX2 + deltaY1 * deltaY2 + deltaZ1 * deltaZ2;
                                    double first = Math.Sqrt(deltaX1 * deltaX1 + deltaY1 * deltaY1 + deltaZ1 * deltaZ1);
                                    double second = Math.Sqrt(deltaX2 * deltaX2 + deltaY2 * deltaY2 + deltaZ2 * deltaZ2);
                                    double cos = multi / (first * second);
                                    double angle = Math.Acos(cos) * 180 / Math.PI;

                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "COMPONENT-ATTRIBUTE41")
                                    {
                                        iii++;
                                    }
                                    string nameBR = tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    bool p = false;
                                    if (Math.Abs(X1-X1B)<1 & Math.Abs(Y1 - Y1B) < 1 & Math.Abs(Z1 - Z1B) < 1)
                                    {
                                        p = true;
                                        if (Z1B < ZcB & Z2B < ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1B > ZcB & Z2B > ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X1 - X2B) < 1 & Math.Abs(Y1 - Y2B) < 1 & Math.Abs(Z1 - Z2B) < 1)
                                    {
                                        p = true;
                                        if (Z1B < ZcB & Z2B < ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1B > ZcB & Z2B > ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X2 - X1B) < 1 & Math.Abs(Y2 - Y1B) < 1 & Math.Abs(Z2 - Z1B) < 1)
                                    {
                                        p = true;
                                        if (Z1B < ZcB & Z2B < ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1B > ZcB & Z2B > ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X2 - X2B) < 1 & Math.Abs(Y2 - Y2B) < 1 & Math.Abs(Z2 - Z2B) < 1)
                                    {
                                        p = true;
                                        if (Z1B < ZcB & Z2B < ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1B > ZcB & Z2B > ZcB)
                                        {
                                            sw.WriteLine("Смена направления уклона на отводе: ");
                                            sw.WriteLine("Отвод: " + nameBR);
                                            sw.WriteLine("Угол: " + (angle).ToString(CultureInfo.InvariantCulture) + " град");
                                            sw.WriteLine("Координаты одного из концов отвода: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                }
                                if (tempPCF[tt] == "TEE")
                                {
                                    int iii = tt;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                                    {
                                        iii++;
                                    }
                                    double X1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                                    double Y1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                                    double Z1B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                                    iii++;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "END-POINT")
                                    {
                                        iii++;
                                    }
                                    double X2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], CultureInfo.InvariantCulture);
                                    double Y2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[2], CultureInfo.InvariantCulture);
                                    double Z2B = Double.Parse(tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[3], CultureInfo.InvariantCulture);
                                    iii++;
                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "CENTRE-POINT")
                                    {
                                        iii++;
                                    }
                                    

                                    while (tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] != "COMPONENT-ATTRIBUTE41")
                                    {
                                        iii++;
                                    }
                                    string nameBR = tempPCF[iii].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];

                                    bool p = false;
                                    if (Math.Abs(X1 - X1B) < 1 & Math.Abs(Y1 - Y1B) < 1 & Math.Abs(Z1 - Z1B) < 1)
                                    {
                                        p = true;
                                        if (Z2 > Z1B & Z2B > Z1B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z2 < Z1B & Z2B < Z1B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X1 - X2B) < 1 & Math.Abs(Y1 - Y2B) < 1 & Math.Abs(Z1 - Z2B) < 1)
                                    {
                                        p = true;
                                        if (Z2 > Z2B & Z1B > Z2B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z2 < Z2B & Z1B < Z2B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X2 - X1B) < 1 & Math.Abs(Y2 - Y1B) < 1 & Math.Abs(Z2 - Z1B) < 1)
                                    {
                                        p = true;
                                        if (Z1 > Z1B & Z2B > Z1B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1 < Z1B & Z2B < Z1B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                    if (p == false & Math.Abs(X2 - X2B) < 1 & Math.Abs(Y2 - Y2B) < 1 & Math.Abs(Z2 - Z2B) < 1)
                                    {
                                        p = true;
                                        if (Z1 > Z2B & Z1B > Z2B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                        if (Z1 < Z2B & Z1B < Z2B)
                                        {
                                            sw.WriteLine("Смена направления уклона на тройнике: ");
                                            sw.WriteLine("Тройник: " + nameBR);
                                            sw.WriteLine("Координаты одного из концов тройника: " + "X: " + (X1B).ToString(CultureInfo.InvariantCulture) + " Y: " + (Y1B).ToString(CultureInfo.InvariantCulture) + " Z: " + (Z1B).ToString(CultureInfo.InvariantCulture));
                                            sw.WriteLine("");
                                        }
                                    }
                                }
                                tt++;
                            }
                        }
                        i++;
                    }

                }
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("");
            }
        }
    }
}
