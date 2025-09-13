using System;
using System.Text;
using UnityEngine;

public sealed class Item : Entity<int>
{
    public string Name { get; }
    public Status Stat { get; }

    public Item(int id, string name, int atk, int def)
    {
        Id = id;
        Name = name;
        Stat = new Status(atk, def);
    }
}
