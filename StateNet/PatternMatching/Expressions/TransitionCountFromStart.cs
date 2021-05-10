﻿using Aptacode.Expressions;
using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.Integer;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.Expressions.List.ListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class TransitionCountFromStart : UnaryExpression<int, TransitionHistory>
    {
        public TransitionCountFromStart(string state, string input, int takeFirst) : base(
            new Matches(new Pattern(state, input)).TakeFirst(new ConstantInteger<TransitionHistory>(takeFirst))
                .Count())
        {
        }

        public override int Interpret(TransitionHistory context)
        {
            return Expression.Interpret(context);
        }

        #region IEquatable

        public override bool Equals(object obj)
        {
            return obj is TransitionCountFromStart expression && Equals(expression);
        }

        public override bool Equals(IExpression<int, TransitionHistory> other)
        {
            return other is TransitionCountFromStart expression && expression == this;
        }

        public static bool operator ==(TransitionCountFromStart lhs, TransitionCountFromStart rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Expression.Equals(rhs.Expression);
        }

        public static bool operator !=(TransitionCountFromStart lhs, TransitionCountFromStart rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}