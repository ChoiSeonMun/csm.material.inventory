using System;
using Unity.VisualScripting;
public readonly struct Status : IEquatable<Status>
{
    public int Atk { get; }

    public int Def { get; }

    public readonly static Status Zero = new Status(0, 0);

    public Status(int atk, int def)
    {
        if (atk < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(atk), "Attack value cannot be negative.");
        }

        if (def < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(def), "Defense value cannot be negative.");
        }

        Atk = atk;
        Def = def;
    }

    public Status Add(Status other)
    {
        return new Status(Atk + other.Atk, Def + other.Def);
    }

    public override string ToString()
    {
        return $"Status(Atk: {Atk}, Def: {Def})";
    }

    #region Equality Members

    public bool Equals(Status other)
    {
        return Atk == other.Atk && Def == other.Def;
    }

    public override bool Equals(object obj)
    {
        return obj is Status other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Atk, Def);
    }

    public static bool operator ==(Status left, Status right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Status left, Status right)
    {
        return !left.Equals(right);
    }
    #endregion
}