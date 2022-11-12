using System;
using System.Diagnostics;

namespace KoksyApp.API.Models;

public abstract class Maybe<T>
{
    public static Maybe<T> Of(T value)
    {
        return value == null?   new None<T>() :new Some<T>(value);
    }
    public abstract Maybe<T1> Map<T1>(Func<T, T1> f);
    public abstract T GetValueOrFallback(T fallback);

}
public class None<T> : Maybe<T>
{
    public override Maybe<T1> Map<T1>(Func<T, T1> f)
    {
        return new None<T1>();
    }

    public override T GetValueOrFallback(T fallback)
    {
        return fallback;
    }
}

public class Some<T> : Maybe<T>
{
    public readonly T Value;
    public Some(T value) => this.Value = value;

    public override Maybe<T1> Map<T1>(Func<T, T1> f)
    {
        return new Some<T1>(f(Value));
    }

    public override T GetValueOrFallback(T fallback)
    {
        return Value;
    }
}