# ConsoleHero
This project started as a lightweight Menu organizer. It's turned into a static data, architecture engine. It still makes menus easily but it also allows you to quickly spin up and mock architecture for any project. 

## DI and Source Gen

You get basic DI without any additonal lines of Code. Label a class with the [Singleton] attribute and it becomes a single, globally seen object. Any other [Singleton] class can accept a Singleton in it's constructor and everything fits together. 

Additionally, using the new IContent<T> interface along it's companion INamed interface, gives you a simple way to have global access to all your static data. 

Allowing you to do something like this from anywhere in your code: 

Creature goblin = GlobalSettings.Get<Creatures>().Goblin;

Ideally you'd use the DI feature to pass the Creatures Singleton to other Singletons at runtime, but GlobalSettings.Get works as a less performant alternative that won't crash with circular dependencies. 


## Video Examples Coming SOON!
Star or Watch this repo to stay up to date. I'll be creating example videos and additional content soon. It's honestly quite amazing how simple you can quickly mock up architecture. Or even convert existing projects to use ConsoleHero. 

## Menu Node System
# Types of Nodes
## Menu
  The backbone of the library. Menus take a fluent approach to options and do all the heavy lifting with console printing and handling for you. Invalid inputs, numbering the options, coloring the text and formalizing everything into a neat, fluent builder. 
## Paragraph
  A simple way to store text information. No more Console.Writeline peppering your codebase. Create a few paragraphs and call them intuitively from another Node. 
## Request
  The obvious input requirement. Menus only get you so far. Eventually you need to request a string from the user like their name or a date. Or how many iterations to loop. This is where requests come in. Quickly create an intuitive user end-point with a few lines of fluent code. 
## Tune
  Who doesn't like a beep or audio queue in their menus? Uses enums and a builder to make note generation intuitive. You can still include custom frequencies but more often then not, you're using a quick Quarter B note or a Half D. 
