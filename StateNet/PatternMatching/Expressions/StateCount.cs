﻿using Aptacode.Expressions.GenericExpressions;
using Aptacode.Expressions.List.IntegerListOperators.Extensions;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions;

public record StateCount : UnaryExpression<int, TransitionHistory>
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
}