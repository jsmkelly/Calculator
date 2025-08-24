using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Calculator.Operation;

namespace Calculator
{
    public partial class frmCalculator : Form
    {
        #region "Fields"
        List<Operation> _operations = new List<Operation>();
        decimal _currentValue = 0M;
        OperatorType _currentOperator = OperatorType.Plus;
        #endregion

        #region "Properties"
        decimal CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                _currentValue = value;
                txtValue.Text = _currentValue.ToString();
            }
        }
        #endregion

        #region "Constructor"
        public frmCalculator()
        {
            InitializeComponent();
        }
        #endregion

        #region "Event Handlers"
        private void btnClear_Click(object sender, EventArgs e)
        {
            _operations.Clear();
            ResetCurrentState();
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            ResetCurrentState();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Equal(object sender, EventArgs e)
        {
            decimal result = 0M;

            _operations.Add(new Operation(_currentOperator, CurrentValue));

            foreach (Operation oper in _operations)
            {
                result = Calculate(oper.Operator, oper.Amount);
            }

            CurrentValue = result;

            //clear everything prior to the equal to avoid duplicate calculations
            _operations.Clear();
            _currentOperator = OperatorType.Plus;

            //handles the calculation of each operation, but it is only needed for the equal button
            decimal Calculate(OperatorType oper, decimal amt)
            {
                switch (oper)
                {
                    case OperatorType.Plus:
                        result += amt;
                        break;
                    case OperatorType.None:
                        result += amt;
                        break;
                    case OperatorType.Subtract:
                        result -= amt;
                        break;
                    case OperatorType.Multiply:
                        if (result == 0M)
                        {
                            result = 1M;
                        }
                        result *= amt;
                        break;
                    case OperatorType.Divide:
                        if (result == 0M)
                        {
                            result = amt;
                        }
                        else if (amt != 0M)
                        {
                            result /= amt;
                        }
                        break;
                    case OperatorType.Modular:
                        if (result == 0M)
                        {
                            result = amt;
                        }
                        else if (amt != 0M)
                        {
                            result %= amt;
                        }
                        break;
                }

                return result;
            }
        }

        private void frmCalculator_Load(object sender, EventArgs e)
        {
            ResetCurrentState();
        }

        public void NumberPress(object sender, EventArgs e)
        {
            //Should be a button but just making sure
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (!btn.Text.Trim().Contains(".") & !txtValue.Text.Trim().Contains("."))
                {
                    CurrentValue = (CurrentValue * 10M) + decimal.Parse(btn.Text.Trim());
                }
                else if (btn.Text.Trim().Contains("."))
                {
                    CurrentValue = CurrentValue + 0.0M;
                }
                else
                {
                    CurrentValue = decimal.Parse(CurrentValue.ToString().Split('.')[0] + "." +
                                   CurrentValue.ToString().Split('.')[1].TrimEnd('0') + btn.Text.Trim());
                }
            }
        }

        public void OperatorPress(object sender, EventArgs e)
        {
            //Should be a button but just making sure
            if (sender is Button)
            {
                _operations.Add(new Operation(_currentOperator, CurrentValue));
                CurrentValue = 0M;

                Button btn = sender as Button;
                switch (btn.Text.Trim())
                {
                    case "+":
                        _currentOperator = OperatorType.Plus;
                        break;
                    case "-":
                        _currentOperator = OperatorType.Subtract;
                        break;
                    case "*":
                        _currentOperator = OperatorType.Multiply;
                        break;
                    case "/":
                        _currentOperator = OperatorType.Divide;
                        break;
                    case "%":
                        _currentOperator = OperatorType.Modular;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region "Methods"
        private void ResetCurrentState()
        {
            CurrentValue = 0M;
            _currentOperator = OperatorType.Plus;
        }
        #endregion

        private void frmCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    NumberPress(sender, e);
                    break;
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                    OperatorPress(sender, e);
                    break;
                case '=':
                case (char)Keys.Enter:
                    Equal(sender, e);
                    break;
                case (char)Keys.Escape:
                    btnClear_Click(sender, e);
                    break;
            }
        }
    }
}
