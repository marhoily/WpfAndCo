using System;
using System.Diagnostics;
using static System.StringComparison;

namespace Generator
{
    [DebuggerDisplay("{FullName}")]
    internal sealed class CmpNode
    {
        private static readonly StringComparer 
            Case = StringComparer.OrdinalIgnoreCase;

        public string DependentUpon { get; }
        public string FullName { get; }

        public CmpNode(string fullName, string dependentUpon = null)
        {
            FullName = fullName;
            DependentUpon = dependentUpon ?? "";
        }

        private bool Equals(CmpNode other)
        {
            return string.Equals(DependentUpon, other.DependentUpon, OrdinalIgnoreCase) 
                && string.Equals(FullName, other.FullName, OrdinalIgnoreCase);
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
                return (
                    Case.GetHashCode(DependentUpon)*397) ^ 
                    Case.GetHashCode(FullName);
            }
        }

        public static bool operator ==(CmpNode left, CmpNode right) => Equals(left, right);
        public static bool operator !=(CmpNode left, CmpNode right) => !Equals(left, right);
    }
}