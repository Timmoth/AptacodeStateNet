using System.Linq;
using Aptacode.Expressions.List;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions;

public record Matches(Pattern Pattern) : TerminalListExpression<int, TransitionHistory>
{
    public override int[] Interpret(TransitionHistory context)
    {
        return context.GetMatches(Pattern).ToArray();
    }
}