﻿using ConsoleHero.Helpers;

namespace ConsoleHero;

public sealed class MenuOption
{
    public MenuOption(string key, string description, Action? effect = null, Color? color = null, Func<bool>? check = null)
    {
        Key = key;
        Description = description;
        Effect = effect ?? (() => { });
        Check = check ?? (static () => true);
        Color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal MenuOption()
    {
        Key = string.Empty;
        Description = string.Empty;
        Effect = () => { };
        Check = static () => true;
        Color = GlobalSettings.DefaultTextColor;
    }

    internal string Key { get; set; }
    internal string Description { get; set; }
    internal Action Effect { get; set; }
    internal Func<bool> Check { get; set; }
    internal bool IsCaseSensitive { get; set; }
    internal bool UsesAutoKey { get; set; } = true;
    internal bool IsHidden { get; set; }
    internal Color Color { get; set; }

    internal void Invoke() => Effect.Invoke();

    internal void Print(string seperator = " => ")
    {
        ColorHelper.SetTextColor(Color);
        WriteLine(Key + seperator + Description);
        ColorHelper.SetToDefault();
    }
}