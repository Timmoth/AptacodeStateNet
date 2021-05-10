using System;
using System.Collections.Generic;
using System.Linq;
using Aptacode.StateNet.PatternMatching;

namespace Aptacode.StateNet.Network
{
    public sealed class StateNetwork
    {
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyList<Connection>>> StateDictionary { get; set; }

        public IReadOnlyList<Pattern> Patterns { get; set; }

        public string StartState { get; set; }

        public StateNetwork(string startState,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyList<Connection>>> stateDictionary,
            IReadOnlyList<Pattern> patterns)
        {
            if (string.IsNullOrEmpty(startState))
            {
                throw new ArgumentNullException(Resources.INVALID_START_STATE);
            }

            StateDictionary = stateDictionary ?? throw new ArgumentNullException(Resources.NO_STATES);

            if (!StateDictionary.Keys.Any())
            {
                throw new ArgumentException(Resources.NO_STATES);
            }

            Patterns = patterns;
            StartState = startState;
        }

        public IReadOnlyList<Connection> GetConnections(string state, string input)
        {
            if (!StateDictionary.TryGetValue(state, out var inputs))
            {
                return new Connection[0];
            }

            return inputs.TryGetValue(input, out var connections) ? connections : new Connection[0];
        }

        public IReadOnlyList<Connection> GetConnections(string state)
        {
            return !StateDictionary.TryGetValue(state, out var inputs)
                ? new Connection[0]
                : inputs.Values.SelectMany(c => c).ToArray();
        }

        public IReadOnlyList<string> GetInputs(string state)
        {
            if (!StateDictionary.TryGetValue(state, out var inputs))
            {
                return new string[0];
            }

            return inputs.Keys.ToList();
        }

        public IReadOnlyList<Connection> GetAllConnections()
        {
            return StateDictionary.Values.SelectMany(c => c.Values.SelectMany(c => c)).ToList();
        }

        public IReadOnlyList<string> GetAllInputs()
        {
            return StateDictionary.Values.SelectMany(c => c.Keys).ToList();
        }

        public IReadOnlyList<string> GetAllStates() => StateDictionary.Keys.ToList();
    }
}