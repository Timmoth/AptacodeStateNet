using System;
using System.Collections.Generic;
using Aptacode.StateNet.Network;
using Aptacode.StateNet.PatternMatching;

namespace Aptacode.StateNet.Engine.Transitions;

public class TransitionHistory
{
    private readonly StateNetwork _network;

    private readonly Dictionary<Pattern, PatternMatcher>
        _patternMatches = new();

    private readonly List<string> _stringTransitionHistory = new();
    private readonly List<int> _transitionHistory = new();

    public TransitionHistory(StateNetwork network)
    {
        _network = network ?? throw new ArgumentNullException(nameof(network));

        if (string.IsNullOrEmpty(network?.StartState))
        {
            throw new ArgumentNullException(nameof(network));
        }

        _transitionHistory.Add(_network.StartState.GetDeterministicHashCode());
        _stringTransitionHistory.Add(_network.StartState);
        CreateMatchTrackers();
    }

    public int TransitionCount { get; private set; }

    private void CreateMatchTrackers()
    {
        foreach (var pattern in _network.Patterns)
        {
            var matchTracker = new PatternMatcher(pattern);
            matchTracker.Add(0, _network.StartState.GetDeterministicHashCode());
            _patternMatches.Add(pattern, matchTracker);
        }
    }

    public IReadOnlyList<int> GetTransitionHistory()
    {
        return _transitionHistory.AsReadOnly();
    }

    public IEnumerable<int> GetMatches(Pattern pattern)
    {
        if (_patternMatches.TryGetValue(pattern, out var matchTracker))
        {
            return matchTracker.MatchList;
        }

        return Array.Empty<int>();
    }

    public void Add(string input, string destination)
    {
        var inputHashCode = input.GetDeterministicHashCode();
        var destinationHashCode = destination.GetDeterministicHashCode();
        _stringTransitionHistory.Add(input);
        _stringTransitionHistory.Add(destination);
        _transitionHistory.Add(inputHashCode);
        _transitionHistory.Add(destinationHashCode);

        TransitionCount++;

        foreach (var patternMatcher in _patternMatches)
        {
            patternMatcher.Value.Add(TransitionCount, inputHashCode);
            patternMatcher.Value.Add(TransitionCount, destinationHashCode);
        }
    }

    public override string ToString()
    {
        return string.Join(",", _stringTransitionHistory);
    }
}