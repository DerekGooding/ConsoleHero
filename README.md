# ConsoleHero
<img src="https://github.com/DerekGooding/ConsoleHero/blob/main/ConsoleHero/icon.png" width=15%>

[![NuGet](https://img.shields.io/nuget/v/ConsoleHero.svg)](https://www.nuget.org/packages/ConsoleHero/) 
[![.NET 7](https://img.shields.io/badge/.NET-7-blue)](https://www.nuget.org/packages/ConsoleHero/) 
[![.NET 8](https://img.shields.io/badge/.NET-8-blue)](https://www.nuget.org/packages/ConsoleHero/) 
[![.NET 9](https://img.shields.io/badge/.NET-9-blue)](https://www.nuget.org/packages/ConsoleHero/) 

[![GitHub license](https://img.shields.io/github/license/DerekGooding/ConsoleHero?color=blue)](https://github.com/DerekGooding/ConsoleHero/blob/main/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/DerekGooding/ConsoleHero?logo=github)](https://github.com/DerekGooding/ConsoleHero/issues)
[![GitHub build](https://img.shields.io/github/actions/workflow/status/DerekGooding/ConsoleHero/build-test.yml?branch=main&logo=github)](https://github.com/DerekGooding/ConsoleHero/actions)
[![Coverage Status](https://coveralls.io/repos/github/DerekGooding/ConsoleHero/badge.svg?refresh)](https://coveralls.io/github/DerekGooding/ConsoleHero)
<!---[![GitHub stars](https://img.shields.io/github/stars/DerekGooding/ConsoleHero?logo=github&style=flat)](https://github.com/modernuo/ModernUO/stargazers)-->


# 🚀 Major Updates 

This project started as a lightweight Menu organizer. It's turned into a static data, architecture engine. It still makes menus easily but it also allows you to quickly spin up and mock architecture for any project. 

## 💡 DI and Source Gen

You get basic DI without any additonal lines of Code. Label a class with the [Singleton] attribute and it becomes a single, globally seen object. Any other [Singleton] class can accept a Singleton in it's constructor and everything fits together. 
[Injection Example Project](https://github.com/DerekGooding/ConsoleHero/tree/main/ConsoleHero.InjectionExample)

Additionally, using the new IContent<T> interface along it's companion INamed interface, gives you a simple way to have global access to all your static data. 
[Example Creatures.cs](https://github.com/DerekGooding/ConsoleHero/blob/main/ConsoleHero.InjectionExample/Creatures.cs)

Allowing you to do something like this from anywhere in your code: 
```
Creature goblin = GlobalSettings.Get<Creatures>().Goblin;
```
Ideally you'd use the DI feature to pass the Creatures Singleton to other Singletons at runtime, but GlobalSettings.Get works as a less performant alternative that won't crash with circular dependancies. 

## ⚙️ Start with ConsoleHero
To install the ConsoleHero package, use the following NuGet command:

```bash
dotnet add package ConsoleHero
```
Or via the NuGet Package Manager:

```bash
Install-Package ConsoleHero
```
## 🎥 Video Examples Coming SOON!
Star or Watch this repo to stay up to date. I'll be creating example videos and additional content soon. It's honestly quite amazing how simple you can quickly mock up architecture. Or even convert existing projects to use ConsoleHero. 

## ☊ Menu Node System

# Turn
![Before](https://github.com/DerekGooding/ConsoleHero/blob/main/ReadmeImages/Before.png)

# Into

![After](https://github.com/DerekGooding/ConsoleHero/blob/main/ReadmeImages/After.png)

# ⭐ Types of Nodes
## Menu
  The backbone of the library. Menus take a fluent approach to options and do all the heavy lifting with console printing and handling for you. Invalid inputs, numbering the options, coloring the text and formalizing everything into a neat, fluent builder. 
## Paragraph
  A simple way to store text information. No more Console.Writeline peppering your codebase. Create a few paragraphs and call them intuitively from another Node. 
## Request
  The obvious input requirement. Menus only get you so far. Eventually you need to request a string from the user like their name or a date. Or how many iterations to loop. This is where requests come in. Quickly create an intuitive user end-point with a few lines of fluent code. 
## Tune
  Who doesn't like a beep or audio queue in their menus? Uses enums and a builder to make note generation intuitive. You can still include custom frequencies but more often then not, you're using a quick Quarter B note or a Half D. 
