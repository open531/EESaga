namespace EESaga.Scripts.Utilities;

using Godot;

public class BiDictionary<T1, T2>
{
    private readonly System.Collections.Generic.Dictionary<T1, T2> _forward = new();
    private readonly System.Collections.Generic.Dictionary<T2, T1> _reverse = new();

    public BiDictionary()
    {
    }

    public BiDictionary(System.Collections.Generic.Dictionary<T1, T2> dictionary)
    {
        foreach (var entry in dictionary)
        {
            Add(entry.Key, entry.Value);
        }
    }

    public T2 this[T1 key]
    {
        get
        {
            if (_forward.TryGetValue(key, out var ret)) return ret;
            throw new System.Collections.Generic.KeyNotFoundException();
        }
        set => Add(key, value);
    }

    public T1 this[T2 key]
    {
        get
        {
            if (_reverse.TryGetValue(key, out var ret)) return ret;
            throw new System.Collections.Generic.KeyNotFoundException();
        }
        set => Add(value, key);
    }

    public void Add(T1 key, T2 value)
    {
        _forward.Add(key, value);
        _reverse.Add(value, key);
    }

    public void Clear()
    {
        _forward.Clear();
        _reverse.Clear();
    }

    public bool ContainsKey(T1 key)
    {
        return _forward.ContainsKey(key);
    }

    public bool ContainsKey(T2 key)
    {
        return _reverse.ContainsKey(key);
    }

    public int Count => _forward.Count;

    public bool IsEmpty => _forward.Count == 0 || _reverse.Count == 0;

    public void Merge(System.Collections.Generic.Dictionary<T1, T2> dictionary, bool overwrite = false)
    {
        foreach (var entry in dictionary)
        {
            if (overwrite || !_forward.ContainsKey(entry.Key))
            {
                _forward[entry.Key] = entry.Value;
            }
        }
    }

    public void Merge(BiDictionary<T1, T2> other, bool overwrite = false)
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

    public bool Remove(T1 key)
    {
        if (_forward.ContainsKey(key))
        {
            _reverse.Remove(_forward[key]);
            _forward.Remove(key);
            return true;
        }

        return false;
    }

    public bool Remove(T2 key)
    {
        if (_reverse.ContainsKey(key))
        {
            _forward.Remove(_reverse[key]);
            _reverse.Remove(key);
            return true;
        }

        return false;
    }

    public bool TryGetValue(T1 key, out T2 value)
    {
        if (_forward.TryGetValue(key, out value)) return true;
        return false;
    }

    public bool TryGetValue(T2 key, out T1 value)
    {
        if (_reverse.TryGetValue(key, out value)) return true;
        return false;
    }
}

public class BiGodotDictionary
{
    private readonly Godot.Collections.Dictionary _forward = new();
    private readonly Godot.Collections.Dictionary _reverse = new();

    public BiGodotDictionary()
    {
    }

    public BiGodotDictionary(Godot.Collections.Dictionary dictionary)
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
            throw new System.Collections.Generic.KeyNotFoundException();
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

    public void Merge(Godot.Collections.Dictionary dictionary, bool overwrite = false)
    {
        foreach (var entry in dictionary)
        {
            if (overwrite || !_forward.ContainsKey(entry.Key))
            {
                _forward[entry.Key] = entry.Value;
            }
        }
    }

    public void Merge(BiGodotDictionary other, bool overwrite = false)
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
