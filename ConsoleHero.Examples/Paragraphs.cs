﻿using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Examples;
public static class Paragraphs
{
    public static Paragraph Eat =>
    Line("You just ate a ").Input().Text(".").
    PressToContinue();

    public static Paragraph Crying =>
    Line("You cry and cry!", ConsoleColor.DarkBlue).
    PressToContinue();

    public static Paragraph ReadNumbers =>
    Line("You read the number ").Input().Text(".").
    Line("Twice that number is ").ModifiedInput((x) => $"{(int)x * 2}").Text(".").
    PressToContinue();
}
