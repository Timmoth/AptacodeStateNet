using System;
using System.Collections.Generic;
using System.Linq;
using Aptacode.StateNet.PatternMatching;

namespace Aptacode.StateNet.Network;

public sealed class StateNetwork : IEquatable<StateNetwork>
{
    public StateNetwork(string startState,
        Dictionary<string, Dictionary<string, IEnumerable<Connection>>> stateDictionary,
        IEnumerable<Pattern> patterns)
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

    public Dictionary<string, Dictionary<string, IEnumerable<Connection>>> StateDictionary { get; set; }

    public IEnumerable<Pattern> Patterns { get; set; }

    public string StartState { get; set; }

    public IEnumerable<Connection> GetConnections(string state, string input)
    {
        if (!StateDictionary.TryGetValue(state, out var inputs))
        {
            return new Connection[0];
        }

        return inputs.TryGetValue(input, out var connections) ? connections : new Connection[0];
    }

    public IEnumerable<Connection> GetConnections(string state)
    {
        return !StateDictionary.TryGetValue(state, out var inputs)
            ? new Connection[0]
            : inputs.Values.SelectMany(c => c);
    }

    public IEnumerable<string> GetInputs(string state)
    {
        if (!StateDictionary.TryGetValue(state, out var inputs))
        {
            return new string[0];
        }

        return inputs.Keys;
    }

    public IEnumerable<Connection> GetAllConnections()
    {
        return StateDictionary.Values.SelectMany(c => c.Values.SelectMany(c => c));
    }

    public IEnumerable<string> GetAllInputs()
    {
        return StateDictionary.Values.SelectMany(c => c.Keys);
    }

    public IEnumerable<string> GetAllStates()
    {
        return StateDictionary.Keys;
    }


    #region IEquatable

    public override bool Equals(object obj)
    {
        return obj is StateNetwork stateNetwork && Equals(stateNetwork);
    }

    public bool Equals(StateNetwork other)
    {
        return this == other;
    }

    public static bool operator ==(StateNetwork lhs, StateNetwork rhs)
    {
        if (lhs is null || rhs is null)
        {
            return lhs is null && rhs is null;
        }

        if (lhs.StartState != rhs.StartState)
        {
            return false;
        }

        if (!lhs.GetAllStates().SequenceEqual(rhs.GetAllStates()))
        {
            return false;
        }

        if (!lhs.GetAllInputs().SequenceEqual(rhs.GetAllInputs()))
        {
            return false;
        }

        if (!lhs.GetAllConnections().SequenceEqual(rhs.GetAllConnections()))
        {
            return false;
        }

        return true;
    }

    public static bool operator !=(StateNetwork lhs, StateNetwork rhs)
    {
        return !(lhs == rhs);
    }

    #endregion
}