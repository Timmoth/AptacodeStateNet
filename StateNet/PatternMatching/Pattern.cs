using System;
using System.Collections.Generic;
using System.Linq;

namespace Aptacode.StateNet.PatternMatching;

public class Pattern : IEquatable<Pattern>
{
    public static readonly Pattern Empty = new();

    public Pattern(params string[] elements)
    {
        if (elements == null)
        {
            elements = Array.Empty<string>();
        }

        Elements = elements;
        HashedElements = elements.Select(x => x?.GetDeterministicHashCode());
        Length = elements.Length;
    }

    public IEnumerable<string?> Elements { get; set; }
    public IEnumerable<int?> HashedElements { get; set; }
    public int Length { get; set; }

    #region IEquatable

    public override int GetHashCode()
    {
        return HashedElements
            .Select(item => item.GetHashCode())
            .Aggregate((total, nextCode) => total ^ nextCode);
    }

    public override bool Equals(object obj)
    {
        return obj is Pattern pattern && Equals(pattern);
    }

    public bool Equals(Pattern other)
    {
        return this == other;
    }

    public static bool operator ==(Pattern lhs, Pattern rhs)
    {
        if (lhs?.Length != rhs?.Length)
        {
            return false;
        }

        return lhs?.Length == null || lhs.HashedElements.SequenceEqual(rhs?.HashedElements);
    }

    public static bool operator !=(Pattern lhs, Pattern rhs)
    {
        return !(lhs == rhs);
    }

    #endregion
}