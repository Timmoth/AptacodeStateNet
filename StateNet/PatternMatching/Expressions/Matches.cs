using System.Linq;
using Aptacode.Expressions;
using Aptacode.Expressions.List;
using Aptacode.StateNet.Engine.Transitions;

namespace Aptacode.StateNet.PatternMatching.Expressions
{
    public class Matches : TerminalListExpression<int, TransitionHistory>
    {
        public readonly Pattern Pattern;

        public Matches(Pattern pattern)
        {
            Pattern = pattern;
        }

        public override int[] Interpret(TransitionHistory context)
        {
            return context.GetMatches(Pattern).ToArray();
        }

        #region IEquatable

        public override bool Equals(object obj)
        {
            return obj is Matches expression && Equals(expression);
        }

        public override bool Equals(IExpression<int[], TransitionHistory> other)
        {
            return other is Matches expression && expression == this;
        }

        public static bool operator ==(Matches lhs, Matches rhs)
        {
            if (lhs is null || rhs is null)
            {
                return lhs is null && rhs is null;
            }

            return lhs.Pattern.Equals(rhs.Pattern);
        }

        public static bool operator !=(Matches lhs, Matches rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}