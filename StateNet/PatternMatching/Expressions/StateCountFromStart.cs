using Aptacode.Expressions;
using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.Integer;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.Expressions.List.ListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class StateCountStart : UnaryExpression<int, TransitionHistory>
    {
        public StateCountStart(string state, int takeFirst) : base(
            new Matches(new Pattern(state)).TakeFirst(new ConstantInteger<TransitionHistory>(takeFirst)).Count()
        ) { }

        public override int Interpret(TransitionHistory context) => Expression.Interpret(context);

        #region IEquatable

        public override bool Equals(object obj) => obj is StateCountStart expression && Equals(expression);

        public override bool Equals(IExpression<int, TransitionHistory> other) => other is StateCountStart expression && expression == this;

        public static bool operator ==(StateCountStart lhs, StateCountStart rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Expression.Equals(rhs.Expression);
        }

        public static bool operator !=(StateCountStart lhs, StateCountStart rhs) => !(lhs == rhs);

        #endregion
    }
}