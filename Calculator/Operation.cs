using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Operation
    {
        public Operation(OperatorType oper, decimal amount)
        {
            Operator = oper;
            Amount = amount;
        }

        public enum OperatorType
        {
            None,
            Plus,
            Subtract,
            Multiply,
            Divide,
            Modular
        }

        public OperatorType Operator { get; set; } = OperatorType.None;
        public decimal Amount { get; set; } = 0M;

    }
}
