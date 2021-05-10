using Aptacode.Expressions.Json;
using Aptacode.StateNet.Engine.Transitions;
using Aptacode.StateNet.PatternMatching.Expressions;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Aptacode.StateNet.Json
{
    public static class StateNetJsonExtensions
    {
        public static JsonSubtypesConverterBuilder AddPatternExpressions(this JsonSubtypesConverterBuilder builder)
        {
            builder.RegisterSubtype<Matches>(nameof(Matches))
                .RegisterSubtype<StateCount>(nameof(StateCount))
                .RegisterSubtype<StateCountFromEnd>(nameof(StateCountFromEnd))
                .RegisterSubtype<StateCountStart>(nameof(StateCountStart))
                .RegisterSubtype<TransitionCount>(nameof(TransitionCount))
                .RegisterSubtype<TransitionCountFromEnd>(nameof(TransitionCountFromEnd))
                .RegisterSubtype<TransitionCountFromStart>(nameof(TransitionCountFromStart));

            return builder;
        }


        public static JsonSerializerSettings AddStateNet(this JsonSerializerSettings settings)
        {
            var intExpressions = ExpressionsJsonExtensions.IntExpressions<TransitionHistory>().AddPatternExpressions();
            var boolExpressions = ExpressionsJsonExtensions.BoolExpressions<TransitionHistory>().AddBoolExpressions<int, TransitionHistory>();

            settings.Add(intExpressions).Add(boolExpressions);

            return settings;
        }
    }
}
