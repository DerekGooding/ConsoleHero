# ConsoleHero
[![NuGet](https://img.shields.io/nuget/v/ConsoleHero.svg)](https://www.nuget.org/packages/ConsoleHero/)

A light-weight library to help quickly and fluently develop Console application UI. 

*Warning, this is currently in heavy development and changes will be aggressive. Use at your own descresion*

# Turn
![Before](https://github.com/DerekGooding/ConsoleHero/blob/master/ReadmeImages/Before.png)

# Into

![After](https://github.com/DerekGooding/ConsoleHero/blob/master/ReadmeImages/After.png)

# Types of Nodes
## Menu
  The backbone of the library. Menus take a fluent approach to options and do all the heavy console printing and handling for you. Invalid inputs, numbering the options, coloring the text and formalizing everything into a neat, fluent builder. 
## Paragraph
  A simple way to store text information. No more Console.Writeline peppering your codebase. Create a few paragraphs and call them intuitively from a menu option. 
## Tune
  Who doesn't like a beep or audio queue in their menues? Uses enums and a builder to make note generation intuitive. You can still include custom frequencies but more often then not, you're using a quick Quarter B note or a Half D. 
## Request
  The obvious input requirement. Menus only get you so far. Eventually you need to request a string from the user like their name or a date. Or how many iterations to loop. This is where requests come in. Quickly create an intuitive user end-point with a few lines of fluent, builder code. 
