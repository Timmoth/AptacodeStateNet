using Aptacode.Expressions;
using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class TransitionCount : UnaryExpression<int, TransitionHistory>
    {
        public TransitionCount(string state, string input) : base(new Matches(new Pattern(state, input)).Count())
        {
        }

        public override int Interpret(TransitionHistory context)
        {
            return Expression.Interpret(context);
        }

        #region IEquatable

        public override bool Equals(object obj)
        {
            return obj is TransitionCount expression && Equals(expression);
        }

        public override bool Equals(IExpression<int, TransitionHistory> other)
        {
            return other is TransitionCount expression && expression == this;
        }

        public static bool operator ==(TransitionCount lhs, TransitionCount rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Expression.Equals(rhs.Expression);
        }

        public static bool operator !=(TransitionCount lhs, TransitionCount rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}