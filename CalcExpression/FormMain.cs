using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DinList;


namespace CalcExpression
{
    public partial class FormMain : Form
    {
        Queue<string> backExpression;
        Dictionary<char, byte> prioritet = new Dictionary<char, byte>
             {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 },
                { '(', 0 },
                { ')', 0 }
            };
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonCalcExpression_Click(object sender, EventArgs e)
        {
            DinStack ds = new DinStack();
            foreach (var s in backExpression)
            {
                char c = s[0];
                if (!prioritet.ContainsKey(c))
                    ds.Push(s.ToString());
                else
                {
                    int a = Convert.ToInt32(ds.Pop().ToString());
                    int b = Convert.ToInt32(ds.Pop().ToString());
                    switch (c)
                    {
                        case '+':  a = a + b; break;
                        case '-': a = b - a; break;
                        case '*': a = a * b; break;
                        case '/': a = b / a; break;
                    }
                    ds.Push(a.ToString());
                }
            }
            labelResult.Text = ds.Pop().ToString();
        }

        string queueToString(Queue<string> q)
        {
            string temp = "";
            foreach (var t in q)
                temp += t + " ";
            return temp;
        }
      

        private void buttonCalcBackExpression_Click(object sender, EventArgs e)
        {
            DinStack calc_back_exp = new DinStack();
            backExpression = new Queue<string>();
            string temp = string.Empty;

            try
            {
                foreach (var c in textBoxExpression.Text)
                {
                    if (c >= '0' && c <= '9')
                    {
                        temp += c;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(temp)) backExpression.Enqueue(temp);
                        temp = string.Empty;
                        if (c == '(')
                            calc_back_exp.Push(c.ToString());
                        else
                            if (c == ')')
                        {
                            while (calc_back_exp.Peek() != '('.ToString())
                            {
                                backExpression.Enqueue(calc_back_exp.Pop());
                            }
                            calc_back_exp.Pop(); // выкинуть ( из стека
                        }
                        else
                        {
                            while (!calc_back_exp.isEmpty() && prioritet[c] <= prioritet[calc_back_exp.Peek()[0]])
                            {
                                backExpression.Enqueue(calc_back_exp.Pop());
                            }
                            calc_back_exp.Push(c.ToString());
                        }
                    }
                }
                if (!string.IsNullOrEmpty(temp)) backExpression.Enqueue(temp);
                temp = string.Empty;

                while (!calc_back_exp.isEmpty())
                {
                    backExpression.Enqueue(calc_back_exp.Pop());
                }

                textBoxBackExpression.Text = queueToString(backExpression);
                buttonCalcExpression.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Что-то не так с выражением!");
            }

        }

        private void textBoxExpression_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
