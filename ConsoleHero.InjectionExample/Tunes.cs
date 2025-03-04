﻿using static ConsoleHero.TuneBuilder;

namespace ConsoleHero.InjectionExample;
[Singleton]
public class Tunes
{
    public Tune Mary =>
    Quarter(Tone.B).
    Quarter(Tone.A).
    Quarter(Tone.GbelowC).
    Quarter(Tone.A).
    Quarter(Tone.B).
    Quarter(Tone.B).
    Half(Tone.B).
    Quarter(Tone.A).
    Quarter(Tone.A).
    Half(Tone.A).
    Quarter(Tone.B).
    Quarter(Tone.D).
    Half(Tone.D).
    ContinueWhilePlaying();
}
