namespace EESaga.Scripts.Utilities;

using Godot;
using System;

public class RangedValue
{
}

public class RangedInt : RangedValue
{
    protected int _min;
    protected int _max;
    protected int _value;
    public int Max
    {
        get => _max;
        set
        {
            _max = value > _min ? value : _min;
            if (_value > _max)
            {
                _value = _max;
            }
        }
    }
    public int Min
    {
        get => _min;
        set
        {
            _min = value < _max ? value : _max;
            if (_value < _min)
            {
                _value = _min;
            }
        }
    }
    public int Value
    {
        get => _value;
        set => _value = value < _min ? _min : value > _max ? _max : value;
    }

    public RangedInt(int value, int min, int max)
    {
        _min = min;
        _max = max;
        _value = value < min ? min : value > max ? max : value;
    }

    public RangedInt(int min, int max)
    {
        _min = min;
        _max = max;
        _value = min;
    }

    public RangedInt(int value)
    {
        _min = int.MinValue;
        _max = int.MaxValue;
        _value = value;
    }

    public static implicit operator int(RangedInt rangedInt)
    {
        return rangedInt.Value;
    }
}

public class RangedLong : RangedValue
{
    protected long _min;
    protected long _max;
    protected long _value;
    public long Max
    {
        get => _max;
        set
        {
            _max = value > _min ? value : _min;
            if (_value > _max)
            {
                _value = _max;
            }
        }
    }
    public long Min
    {
        get => _min;
        set
        {
            _min = value < _max ? value : _max;
            if (_value < _min)
            {
                _value = _min;
            }
        }
    }
    public long Value
    {
        get => _value;
        set => _value = value < _min ? _min : value > _max ? _max : value;
    }

    public RangedLong(long value, long min, long max)
    {
        _min = min;
        _max = max;
        _value = value < min ? min : value > max ? max : value;
    }

    public RangedLong(long min, long max)
    {
        _min = min;
        _max = max;
        _value = min;
    }

    public RangedLong(long value)
    {
        _min = long.MinValue;
        _max = long.MaxValue;
        _value = value;
    }

    public static implicit operator long(RangedLong rangedLong)
    {
        return rangedLong.Value;
    }
}

public class RangedFloat : RangedValue
{
    protected float _min;
    protected float _max;
    protected float _value;
    public float Max
    {
        get => _max;
        set
        {
            _max = value > _min ? value : _min;
            if (_value > _max)
            {
                _value = _max;
            }
        }
    }
    public float Min
    {
        get => _min;
        set
        {
            _min = value < _max ? value : _max;
            if (_value < _min)
            {
                _value = _min;
            }
        }
    }
    public float Value
    {
        get => _value;
        set => _value = value < _min ? _min : value > _max ? _max : value;
    }

    public RangedFloat(float value, float min, float max)
    {
        _min = min;
        _max = max;
        _value = value < min ? min : value > max ? max : value;
    }

    public RangedFloat(float min, float max)
    {
        _min = min;
        _max = max;
        _value = min;
    }

    public RangedFloat(float value)
    {
        _min = float.MinValue;
        _max = float.MaxValue;
        _value = value;
    }

    public static implicit operator float(RangedFloat rangedFloat)
    {
        return rangedFloat.Value;
    }
}

public class RangedDouble : RangedValue
{
    protected double _min;
    protected double _max;
    protected double _value;
    public double Max
    {
        get => _max;
        set
        {
            _max = value > _min ? value : _min;
            if (_value > _max)
            {
                _value = _max;
            }
        }
    }
    public double Min
    {
        get => _min;
        set
        {
            _min = value < _max ? value : _max;
            if (_value < _min)
            {
                _value = _min;
            }
        }
    }
    public double Value
    {
        get => _value;
        set => _value = value < _min ? _min : value > _max ? _max : value;
    }

    public RangedDouble(double value, double min, double max)
    {
        _min = min;
        _max = max;
        _value = value < min ? min : value > max ? max : value;
    }

    public RangedDouble(double min, double max)
    {
        _min = min;
        _max = max;
        _value = min;
    }

    public RangedDouble(double value)
    {
        _min = double.MinValue;
        _max = double.MaxValue;
        _value = value;
    }

    public static implicit operator double(RangedDouble rangedDouble)
    {
        return rangedDouble.Value;
    }
}