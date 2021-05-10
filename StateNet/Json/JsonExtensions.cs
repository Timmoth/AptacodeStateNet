using Aptacode.Expressions;
using Aptacode.StateNet.PatternMatching.Expressions;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Aptacode.StateNet.Json
{
    public static class JsonExtensions
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
    }
}
