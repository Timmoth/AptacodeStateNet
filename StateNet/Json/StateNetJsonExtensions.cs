using Aptacode.Expressions.Json;
using Aptacode.StateNet.Engine.Transitions;
using Aptacode.StateNet.PatternMatching.Expressions;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Aptacode.StateNet.Json
{
    public static class StateNetJsonExtensions
    {
        public static JsonSubtypesConverterBuilder AddStateNetIntExpressions(this JsonSubtypesConverterBuilder builder)
        {
            builder
                .RegisterSubtype<StateCount>(nameof(StateCount))
                .RegisterSubtype<StateCountFromEnd>(nameof(StateCountFromEnd))
                .RegisterSubtype<StateCountStart>(nameof(StateCountStart))
                .RegisterSubtype<TransitionCount>(nameof(TransitionCount))
                .RegisterSubtype<TransitionCountFromEnd>(nameof(TransitionCountFromEnd))
                .RegisterSubtype<TransitionCountFromStart>(nameof(TransitionCountFromStart));

            return builder;
        }

        public static JsonSubtypesConverterBuilder AddStateNetIntListExpressions(this JsonSubtypesConverterBuilder builder)
        {
            builder.RegisterSubtype<Matches>(nameof(Matches));

            return builder;
        }


        public static JsonSerializerSettings AddStateNet(this JsonSerializerSettings settings)
        {
            var intExpressions = ExpressionsJsonExtensions.IntExpressions<TransitionHistory>().AddStateNetIntExpressions();
            var listExpressions = ExpressionsJsonExtensions.ListExpressions<int, TransitionHistory>().AddStateNetIntListExpressions();
            var boolExpressions = ExpressionsJsonExtensions.BoolExpressions<TransitionHistory>().ExtendBoolExpressions<int, TransitionHistory>();

            settings.Add(intExpressions).Add(boolExpressions).Add(listExpressions);

            return settings;
        }
    }
}