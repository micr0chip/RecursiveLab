using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurciveAnalyzer
{
    // G[<���������>]:
    //1. <���������> = <���������>{(+|-)<���������>}
    //2. < ��������� > = < ��������� >{ (*|/) < ��������� >}
    //3. < ��������� > = [+| -] < ������������ > | < ������� > |(< ��������� >)
    //4. < ������������ > = < ���������� > | < ���������� > .< ������������ >
    //5. < ���������� > = < ����� >{< ����� >}
    //6. < ������������ > = < ����� >{< ����� >}
    //7. < ������� > = < ���������� > (< ��������� >)
    //8. < ���������� > = "sin" | "cos"
    //< ����� > = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9

    class StateMachine
    {
        public class Transition
        {
            public char state;
            public char transition;
            public Transition(char _state, char _transition)
            {
                state = _state;
                transition = _transition;
            }
        }

        //��������� 
        public char State { get; set; }
        // �������� �� ��������
        public bool IsEnd { get; }
        // ������ ������ � ������� �����������
        private List<Transition> _transitions = new List<Transition>();
        public List<Transition> Transitions { get { return _transitions; } }
        public StateMachine(char state, bool isEnd, List<Transition> transitions)
        {
            State = state;
            IsEnd = isEnd;
            _transitions = transitions;
        }
    }
    internal interface IRecursiveFunction
    {
        // ���������
        void Expression();
        // ���������
        void Term();
        // ���������
        void Factor();
        // ������� �����
        void FractionalNumber();
        // �������
        void Function();
        // ����� �����
        void WholePart();
        // �����
        void Digit();
        // ������� �����
        void Fraction();
        // ��� �������
        void NameFunction();
    }
    internal class RecurciveAnalyzer : IRecursiveFunction
    {
        private string _text;
        private int i = 0;
        private bool _result = false;
        private List<string> transitions = new List<string>();
        public RecurciveAnalyzer(string text)
        {
            _text = text;
            _text.Replace("\n", "");
            _text.Replace(" ", "");
        }

        public (bool, string) StartAnalyze()
        {
            Expression();
            return (_result, string.Join(" ", transitions));
        }
        public void Expression()
        {
            transitions.Add("-> <���������> ");
            Term();
            while (i < _text.Length)
            {
                if (_text[i] == '+' || _text[i] == '-')
                    Term();
                else return;
            }
        }

        public void Term()
        {
            transitions.Add("-> <���������> ");
            Factor();
            while (i < _text.Length)
            {
                if (_text[i] == '/' || _text[i] == '*')
                    Factor();
                else return;
            }
        }
        public void Factor()
        {
            transitions.Add("-> <���������>");
            if (_text[i] == '+' || _text[i] == '-' || _text[i] == '*' || _text[i] == '/')
                i++;

            if (Char.IsDigit(_text[i]))
                FractionalNumber();
            else
            {
                if (Char.IsLetter(_text[i]))
                    Function();
                else if (_text[i] == '(')
                {
                    i++;
                    Expression();
                    i++;
                    if (_text[i] != ')')
                        return;
                }
            }
        }

        public void FractionalNumber()
        {
            transitions.Add("-> <������� �����>");
            int j = i;
            while (j < _text.Length && Char.IsDigit(_text[j]))
                j++;
            if (j < _text.Length && _text[j] == '.')
            {
                WholePart();
                i++;
                Fraction();
            }
            else WholePart();
        }
        public void WholePart()
        {
            transitions.Add("-> <����� �����>");
            Digit();
        }

        public void Digit()
        {
            transitions.Add("-> <�����>");
            while (i < _text.Length && Char.IsDigit(_text[i]))
                i++;
            _result = true;
        }
        public void Fraction()
        {
            transitions.Add("-> <������� �����>");
            Digit();
        }

        public void Function()
        {
            transitions.Add("-> <�������>");
            NameFunction();
            if (_text[i] == '(')
            {
                i++;
                Expression();
                if (_text[i] != ')')
                {
                    _result = false;
                    return;
                }
                else
                {
                    i++;
                    _result = true;
                }
            }
            else
            {
                _result = false;
                return;
            }
        }

        public void NameFunction()
        {
            transitions.Add("-> <��� �������>");
            if (i + 3 < _text.Length && (_text.Substring(i, 3) == "sin" || _text.Substring(i, 3) == "cos"))
            {
                i += 3;
                _result = true;
            }
            else _result = false;
            return;
        }
    }
}