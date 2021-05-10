using Aptacode.Expressions;
using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.Integer;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.Expressions.List.ListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class StateCountFromEnd : UnaryExpression<int, TransitionHistory>
    {
        public StateCountFromEnd(string state, int takeLast) : base(
            new Matches(new Pattern(state)).TakeLast(new ConstantInteger<TransitionHistory>(takeLast)).Count()) { }

        public override int Interpret(TransitionHistory context) => Expression.Interpret(context);

        #region IEquatable

        public override bool Equals(object obj) => obj is StateCountFromEnd expression && Equals(expression);

        public override bool Equals(IExpression<int, TransitionHistory> other) => other is StateCountFromEnd expression && expression == this;

        public static bool operator ==(StateCountFromEnd lhs, StateCountFromEnd rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Expression.Equals(rhs.Expression);
        }

        public static bool operator !=(StateCountFromEnd lhs, StateCountFromEnd rhs) => !(lhs == rhs);

        #endregion
    }
}