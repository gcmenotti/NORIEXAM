using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int f = 0;
        int l = 0;
        int n, m;

        TextBox[,] textbox = new TextBox[100, 100];
        Label[,] letter = new Label[120, 120];
        string operators = "+*/ -";
        int currentPos = 0;

        private void btnResolve_Click(object sender, EventArgs e)
        {
            if ((txtA.Text == "") || (txtB.Text == ""))
                MessageBox.Show("Error", "opps", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                n = Convert.ToInt32(txtA.Text);
                m = Convert.ToInt32(txtB.Text);
            }

            double[,] Matrica;
            Matrica = new double[n, m];
            bool[,] solved = new bool[n, m];
            var allSolved = n * m;

            while (allSolved > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (!solved[i, j])
                        {
                            Matrica[i, j] = ProcessCell(textbox[i, j].Text);
                            solved[i, j] = true;
                            allSolved--;
                            richTextBox1.AppendText($"{i},{j}: {Matrica[i, j]}\r\n");
                        }
                    }
                }
            }
        }

        private double ProcessCell(string text)
        {
            var result = 0d;
            if (text.StartsWith("="))
            {
                string noEqual = text.Substring(1);
                
                if (!double.TryParse(noEqual, out result))
                {
                    var operands = new string[3];
                    operands[0] = GetOperand(noEqual, 0);
                    if (noEqual.Length > operands[0].Length)
                    {
                        operands[1] = GetOperator(noEqual, operands[0].Length);
                        currentPos = operands[0].Length;
                    }
                    if (!string.IsNullOrEmpty(operands[1]))
                    {
                        operands[2] = GetOperand(noEqual, currentPos + 1);
                    }
                    result = ProcessFormula(operands);
                }
            }
            else
            {
                double.TryParse(text, out result);
            }
            return result;
        }

        private string GetOperator(string text, int pos)
        {
            int p = pos;
            string result = string.Empty;
            while (p < text.Length)
            {
                if (operators.Contains(text[p]) && text[p] != ' ')
                {
                    if (text[p] == '-')
                    {
                        if (p < text.Length)
                            if ((!operators.Contains(text[p + 1]) || text[p + 1] == ' '))
                                result = text[p].ToString();
                    }
                    else
                    { result = text[p].ToString(); }
                }
                p++;
            }
            currentPos = p;
            return result;
        }

        private string GetOperand(string text, int pos)
        {
            int p = pos;
            string result = string.Empty;
            var keepGoing = true;
            while (p < text.Length && keepGoing)
            {
                if (p == 0 && text[p] == '-' && text.Length > 1)
                {
                    if (!operators.Contains(text[1]))
                    {
                        result = text.Substring(p, 2);
                        p = 1;
                    }
                    else
                    { result = "ERROR: " + text.Substring(p, 2); }
                }
                else
                {
                    
                    if (operators.Contains(text[p]) && string.IsNullOrEmpty(result))
                    {
                        if (p < text.Length)
                            if (text[p] == '-' && !operators.Contains(text[p + 1]) && string.IsNullOrEmpty(result))
                                result += text.Substring(p, 1);
                            else
                                result = "ERROR: " + text.Substring(p, 2);
                    }
                    else if (!operators.Contains(text[p]))
                    {
                        result += text.Substring(p, 1);
                    }
                }
                
                if (!string.IsNullOrEmpty(result) && operators.Contains(text[p]))
                    keepGoing = false;
                p++;
            }
            currentPos = p;
            return result;
        }

        private double ProcessFormula(string[] operands)
        {
            double[] op = new double[2];
            op[0] = LoadOperands(operands[0]);
            op[1] = LoadOperands(operands[2]);
            switch (operands[1])
            {
                case "+": return op[0] + op[1];
                case "-": return op[0] - op[1];
                case "*": return op[0] * op[1];
                case "/": return op[0] / op[1];
            }
            return 0;
        }

        private double LoadOperands(string operand)
        {
            double result = 0;
            if (!double.TryParse(operand, out result))
            {
                TextBox t = new TextBox();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {

                        if (textbox[i, j].Name.ToLower() == operand.Trim().ToLower())
                            t = textbox[i, j];
                    }
                }
                result = ProcessCell(t.Text);
            }
            return result;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Point locaLabel = this.label.Location;

            int a = locaLabel.X + 80;
            int b = locaLabel.Y;

            if ((txtA.Text == "") || (txtB.Text == ""))
                MessageBox.Show("Error", "opps", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                n = Convert.ToInt32(txtA.Text);
                m = Convert.ToInt32(txtB.Text);
            }
            int i, j;
            int[,] Matrix;
            Matrix = new int[n, m];

            int PositionalValue = 0;

            int pos_x_lb_step = 40;

            int pos_y_lb_step = 30;
            List<string> list = new List<String>() { "A", "B", "C", "D", "E", "F", "G", "H" };
            var z = 0;
            for (i = 0; i < n; i++)
            {
                a = locaLabel.X + 10;
                b = b + 30;

                PositionalValue++;
                for (j = 0; j < m; j++)
                {

                    textbox[i, j] = new TextBox();
                    letter[i, j] = new Label();


                    letter[i, j].Text = list.ElementAt(j) + PositionalValue;
                    letter[i, j].AutoSize = true;
                    letter[i, j].Font = new Font("Calibri", 8);
                    letter[i, j].ForeColor = Color.Red;
                    letter[i, j].Padding = new Padding(3);
                    letter[i, j].Location = new Point(a + (j * pos_x_lb_step) + 30, b + (pos_y_lb_step * i) - 20);

                    this.textbox[i, j].Name = PositionalValue.ToString();


                    a = a + 25;


                    textbox[i, j].Width = 45;
                    textbox[i, j].Height = 25;
                    //textbox[i, j].Location = new Point(a+PositionalValue, b + 40);
                    textbox[i, j].Text = z.ToString();
                    textbox[i, j].Location = new Point(a + (j * pos_x_lb_step), b + (pos_y_lb_step * i));
                    this.textbox[i, j].Name = list.ElementAt(j) + PositionalValue;
                    this.Controls.Add(textbox[i, j]);

                    this.Controls.Add(letter[i, j]);
                    z++;



                }



            }
        }
    }
}
