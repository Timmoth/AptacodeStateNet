﻿using System;
using System.Collections.Generic;
using System.Linq;
using Aptacode.Expressions;
using Aptacode.StateNet.Engine.Transitions;
using Aptacode.StateNet.Network.Validator;
using Aptacode.StateNet.PatternMatching;

namespace Aptacode.StateNet.Network;

public class NetworkBuilder
{
    private readonly List<(string, string, Connection)> _connections;
    private readonly HashSet<string> _inputs;
    private readonly HashSet<Pattern> _patterns;
    private readonly HashSet<string> _states;
    private string _startState;

    protected NetworkBuilder()
    {
        _startState = string.Empty;
        _states = new HashSet<string>();
        _inputs = new HashSet<string>();
        _patterns = new HashSet<Pattern>();
        _connections = new List<(string, string, Connection)>();
    }

    public static NetworkBuilder New => new();

    public NetworkBuilder SetStartState(string startState)
    {
        _startState = startState;
        _states.Add(_startState);
        return this;
    }

    public NetworkBuilder AddState(string state)
    {
        _states.Add(state);
        return this;
    }

    public NetworkBuilder RemoveState(string state)
    {
        ClearConnectionsFromState(state);
        ClearConnectionsToState(state);
        _states.Remove(state);
        return this;
    }

    private NetworkBuilder AddInput(string input)
    {
        _inputs.Add(input);
        return this;
    }

    public NetworkBuilder RemoveInputOnState(string input, string state)
    {
        ClearConnectionsFromState(state, input);
        _inputs.Remove(input);
        return this;
    }

    public NetworkBuilder AddConnection(string source, string input, string destination,
        IExpression<int, TransitionHistory> expression)
    {
        AddState(source);
        AddInput(input);
        AddState(destination);

        _connections.Add((source, input, new Connection(destination, expression)));
        return this;
    }

    public NetworkBuilder ClearConnectionsFromState(string state, string input)
    {
        var connectionToRemove = _connections.Where(c => c.Item1 == state && c.Item2 == input);
        foreach (var connection in connectionToRemove.ToList())
        {
            _connections.Remove(connection);
        }

        return this;
    }

    public NetworkBuilder ClearConnectionsFromState(string state)
    {
        var connectionToRemove = _connections.Where(c => c.Item1 == state);
        foreach (var connection in connectionToRemove.ToList())
        {
            _connections.Remove(connection);
        }

        return this;
    }

    public NetworkBuilder ClearConnectionsToState(string state, string input)
    {
        var connectionToRemove = _connections.Where(c => c.Item2 == input && c.Item3.Target == state);
        foreach (var connection in connectionToRemove.ToList())
        {
            _connections.Remove(connection);
        }

        return this;
    }

    public NetworkBuilder ClearConnectionsToState(string state)
    {
        var connectionToRemove = _connections.Where(c => c.Item3.Target == state);
        foreach (var connection in connectionToRemove.ToList())
        {
            _connections.Remove(connection);
        }

        return this;
    }

    public NetworkBuilder AddPattern(params Pattern[] patterns)
    {
        foreach (var pattern in patterns)
        {
            _patterns.Add(pattern);
        }

        return this;
    }

    public NetworkBuilder RemovePattern(params Pattern[] patterns)
    {
        foreach (var pattern in patterns)
        {
            _patterns.Remove(pattern);
        }

        return this;
    }

    private StateNetworkResult CreateStateNetwork()
    {
        try
        {
            var stateDictionary =
                new Dictionary<string, Dictionary<string, IEnumerable<Connection>>>();

            foreach (var state in _states)
            {
                var inputDictionary = new Dictionary<string, IEnumerable<Connection>>();

                foreach (var input in _inputs)
                {
                    inputDictionary.Add(input,
                        new List<Connection>()); //This requires every input to have at least an empty connection associated to it
                }

                var connectionsFromState = _connections.Where(c => c.Item1 == state).GroupBy(c => c.Item2);
                foreach (var connectionGroup in connectionsFromState)
                {
                    var connections = connectionGroup.Select(c => c.Item3);
                    inputDictionary[connectionGroup.Key] = connections;

                    foreach (var connection in connections)
                    {
                        var matchesVisitor = new MatchesVisitor();

                        matchesVisitor.Schedule(connection.Expression);
                        foreach (var matchesVisitorPattern in matchesVisitor.Patterns)
                        {
                            _patterns.Add(matchesVisitorPattern);
                        }
                    }
                }

                var emptyInputs = inputDictionary.Where(i => i.Value.Count() == 0).Select(i => i.Key);
                foreach (var emptyInput in emptyInputs)
                {
                    inputDictionary.Remove(emptyInput);
                }

                stateDictionary.Add(state, inputDictionary);
            }

            var network = new StateNetwork(_startState, stateDictionary, _patterns.ToList());
            return StateNetworkResult.Ok(network, Resources.SUCCESS);
        }
        catch (Exception ex)
        {
            return StateNetworkResult.Fail(ex.Message);
        }
    }

    public StateNetworkResult Build()
    {
        var networkResult = CreateStateNetwork();
        var network = networkResult.Network;

        Reset();

        if (!networkResult.Success || network == null)
        {
            return networkResult;
        }

        var stateNetworkValidationResult = network.IsValid();
        return !stateNetworkValidationResult.Success
            ? StateNetworkResult.Fail(stateNetworkValidationResult.Message)
            : StateNetworkResult.Ok(network, Resources.SUCCESS);
    }

    public void Reset()
    {
        _startState = string.Empty;
        _states.Clear();
        _inputs.Clear();
        _connections.Clear();
        _patterns.Clear();
    }
}