using System;
using System.Diagnostics;

namespace Generator
{
    [DebuggerDisplay("{FullName}")]
    public sealed class CmpNode
    {
        public string DependentUpon { get; }
        public string FullName { get; }

        public CmpNode(string fullName, string dependentUpon = null)
        {
            FullName = fullName;
            DependentUpon = dependentUpon ?? "";
        }

        private bool Equals(CmpNode other)
        {
            return string.Equals(DependentUpon, other.DependentUpon, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(FullName, other.FullName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is CmpNode && Equals((CmpNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StringComparer.OrdinalIgnoreCase.GetHashCode(DependentUpon)*397) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(FullName);
            }
        }

        public static bool operator ==(CmpNode left, CmpNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CmpNode left, CmpNode right)
        {
            return !Equals(left, right);
        }
    }
}