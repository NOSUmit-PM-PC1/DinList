using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinList
{
    public class Node
    {
        public string val;
        public Node adr;
        
        //конструктор
        public Node(string c)
        { val = c; adr = null; }

        public string ToString()
        {
            return val.ToString();
        }
    }

    public class DinStack
    {
        Node top = null;

        // конструктор класса
        public DinStack() { }
        public void Push (string c)
        {
            Node temp = new Node(c);
            temp.adr = top;
            top = temp;
        }

        public string ToString()
        {
            string rez = "";
            Node current = top;
            while (current != null)
            {
                rez += current.ToString();
                current = current.adr;
            }

            return rez;
        }

        public string Pop()
        {
            if (!isEmpty())
            {
                string rez = top.val;
                top = top.adr;
                return rez;
            }
            return string.Empty;
        }

        public string Peek()
        {
            if (!isEmpty())
            {
               return  top.val;
            }
            return string.Empty;
        }

        public bool isEmpty()
        {
            if (top == null) return true;
            return false;
        }
    }


    class Program
    {
        static string sk_open = "({[<";
        static string sk_close = ")}]>";
        static bool para(char close, char open)
        {
            int ind_open = sk_open.IndexOf(open);
            char temp_close = sk_close[ind_open];
            return temp_close == close;

        }
        static void Main(string[] args)
        {          
            string sk = "";
            DinStack st = new DinStack();
            string ans = "Yes";
            //foreach (var c in sk)
            //{
            //    // если открывающаяся скобка
            //    if (sk_open.IndexOf(c) >= 0)
            //        st.Push(c);
            //    else
            //    {
            //        if (!st.isEmpty())
            //        {
            //            char temp = st.Pop(); // открывающаяся из стека
            //            if (!para(c, temp))
            //            {
            //                ans = "No";
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            ans = "No";
            //            break;
            //        }
            //    }
            //}
            if (!st.isEmpty()) ans = "No";
            Console.WriteLine(ans);
        }
    }
}
