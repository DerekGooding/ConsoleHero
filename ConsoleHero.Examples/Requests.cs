﻿using static ConsoleHero.RequestBuilder;

namespace ConsoleHero.Examples;
public static class Requests
{
    public static Request AskForName =>
    Ask("What is your name?").
    For(DataType.String).
    Goto(Paragraphs.YourNameIs).
    Use((x) => Data.Name = (string)x);
}
