using Aptacode.Expressions.Json;
using Aptacode.StateNet.Engine.Transitions;
using Aptacode.StateNet.PatternMatching.Expressions;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Aptacode.StateNet.Json
{
    public static class StateNetJsonExtensions
    { 

        public static ExpressionsSubTypes AddStateNet(this ExpressionsSubTypes subTypes)
        {
            return subTypes
                .AddInt<TransitionHistory>()
                .AddBool<TransitionHistory>()
                .AddBool<int, TransitionHistory>()
                .AddList<int, TransitionHistory>()
                .Add<Matches>()
                .Add<StateCount>()
                .Add<StateCountFromEnd>()
                .Add<StateCountStart>()
                .Add<TransitionCount>()
                .Add<TransitionCountFromEnd>()
                .Add<TransitionCountFromStart>();
        }
    }
}