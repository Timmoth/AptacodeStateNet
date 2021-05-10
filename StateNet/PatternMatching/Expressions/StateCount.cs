using Aptacode.Expressions;
using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class StateCount : UnaryExpression<int, TransitionHistory>
    {
        public StateCount(string state) : base(
            new Matches(
                new Pattern(state)
            ).Count()
        )
        {
        }

        public override int Interpret(TransitionHistory context)
        {
            return Expression.Interpret(context);
        }

        #region IEquatable

        public override bool Equals(object obj)
        {
            return obj is StateCount expression && Equals(expression);
        }

        public override bool Equals(IExpression<int, TransitionHistory> other)
        {
            return other is StateCount expression && expression == this;
        }

        public static bool operator ==(StateCount lhs, StateCount rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Expression.Equals(rhs.Expression);
        }

        public static bool operator !=(StateCount lhs, StateCount rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}