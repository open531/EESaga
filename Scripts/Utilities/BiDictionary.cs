namespace EESaga.Scripts.Utilities;

using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class BiDictionary
{
    private readonly Dictionary _forward = new();
    private readonly Dictionary _reverse = new();

    public BiDictionary()
    {
    }

    public BiDictionary(Dictionary dictionary)
    {
        foreach (var entry in dictionary)
        {
            Add(entry.Key, entry.Value);
        }
    }

    public Variant this[Variant key]
    {
        get
        {
            if (_forward.TryGetValue(key, out var ret)) return ret;
            if (_reverse.TryGetValue(key, out ret)) return ret;
            throw new KeyNotFoundException();
        }
        set => Add(key, value);
    }

    public void Add(Variant key, Variant value)
    {
        _forward.Add(key, value);
        _reverse.Add(value, key);
    }

    public void Clear()
    {
        _forward.Clear();
        _reverse.Clear();
    }

    public bool ContainsKey(Variant key)
    {
        return _forward.ContainsKey(key) || _reverse.ContainsKey(key);
    }

    public int Count => _forward.Count;

    public bool IsEmpty => _forward.Count == 0 || _reverse.Count == 0;

    public bool IsReadOnly => _forward.IsReadOnly || _reverse.IsReadOnly;

    public void Merge(Dictionary dictionary, bool overwrite = false)
    {
        foreach (var entry in dictionary)
        {
            if (overwrite || !_forward.ContainsKey(entry.Key))
            {
                _forward[entry.Key] = entry.Value;
            }
        }
    }

    public void Merge(BiDictionary other, bool overwrite = false)
    {
        foreach (var entry in other._forward)
        {
            if (overwrite || !_forward.ContainsKey(entry.Key))
            {
                _forward[entry.Key] = entry.Value;
            }
        }

        foreach (var entry in other._reverse)
        {
            if (overwrite || !_reverse.ContainsKey(entry.Key))
            {
                _reverse[entry.Key] = entry.Value;
            }
        }
    }

    public bool Remove(Variant key)
    {
        if (_forward.ContainsKey(key))
        {
            _reverse.Remove(_forward[key]);
            _forward.Remove(key);
            return true;
        }

        if (_reverse.ContainsKey(key))
        {
            _forward.Remove(_reverse[key]);
            _reverse.Remove(key);
            return true;
        }

        return false;
    }

    public bool TryGetValue(Variant key, out Variant value)
    {
        if (_forward.TryGetValue(key, out value)) return true;
        if (_reverse.TryGetValue(key, out value)) return true;
        return false;
    }
}
