# S# syntax

## Namespaces
Namespaces are like chests, but for storing variables & functions.

You can declare a namespace by adding a variable into a non-existant namespace.

```py
# The namespace "sus" doens't exists yet. When the namespace isn't found, it will create it and add the variable into it.
sus.v = 100
```

## Functions

You can declare functions with the def keyword, and call them with their name followed by hooks.

```py
def exemple() [
   print("Exemple")
]

exemple()
```

You can pass arguments to the function like this :
```py
def exemple(myarg, myarg2) [
   print("Exemple with " + myarg + " and " + myarg2 + " as args")
]

exemple("Hello World", "Among Us is my favourite game")
```

## Variables
You can declare variables like this : 
```py
x = "Hello, World!"
```

And can be accessed just by his name : 
`print(x)`

## For-loops
```py
for (i in 1..5) [
    print(i)
]
```
This program prints the numbers 1 to 5. It uses a range operator (..) to define the range to loop with.

## Ranges
For-loops are defined with a range, which is a special object type in S#. They can be either used in a for-loop directly or stored in a variable.

Ranges can be created with the range operator (`..`):

```py
range = 1..10
```
Ranges can also use existing variables for their lower or upper bounds:

```py
x = 10
range = 1..x
```

## While loops
```py
while (true) [
    print("Hello World!")
]
```
A while loop runs continously, provided its condition (in parentheses) is truthy.

This program prints "Hello World" endlessly, because the condition in the while loop is true.

## Booleans
S# supports booleans and boolean logic:

```py
trueBool = true
falseBool = false
print(trueBool)
```

## If and else statements
If statements can be created with the if keyword.
```py
bool = true
if (bool) [
    print("Bool is truthy.")
]
```
You can also use the else keyword to execute a block if the condition is not truthy.
```py
bool = false
if (bool) [
    print("Bool is truthy.")
] else [
    print("Bool is not truthy.")
]
```

## Comments
Comments are prefixed with `#` : 
```py
# This is a comment
print("Hello, World") # This is another comment
```

## Null
You can use the `null` keyword to create a null value. Empty expressions will also evaluate to null:

```py
print(())
# Prints 'null'.
```

You can check if a variable is null by equating it with null:

```py
nullvar = null
print(nullvar == null)
# Prints 'True'.
```

## Truthiness
In S#, all variables are truthy, except false and null.

## Expressions
S# supports all of the standard operators you would expect, and you can combine them in expressions.

Note: Expressions are evaluated from right to left.

Arithmetic operators
`+` - Addition
`-` - Subtraction (unary)
`*` - Multiplication
`/` - Division
`%` - Modulo
Logical operators
`==` - Equals
`!=` - Does not equal
`>=` - Greater than or equal
`<=` - Less than or equal
`>` - Greater than
`<` - Less than
`and` - Returns the first non-truthy operand
`or` - Returns the left-hand operand if it is truthy, otherwise the right-hand operand
`not` - Returns false if its right-hand operand is truthy, otherwise true (unary only)

## Concatenation
You can concatenate strings with other strings using the + operator:

```py
print("Enter your name:")
input = read()
print("Hello " + input + "!")
```
This program asks the user to enter their name. It then uses string concatenation to print out a customised message with the user's name.

You cannot concatenate strings and numbers directly:
```py
print("Hello world!" + 123)
# Error: Cannot add Hello world! and 123.
```
Instead, you should convert the number to a string with the str function.

```py
print("Hello world!" + str(123))
# Hello world!123
```

# Library loader 

S# include a library loader to load libraries made for the S# programming language.

The library loader can load external managed DLL files and internal libraries.

For exemple : the standard library is a internal library. The test library is an external library.

## They are compatible with Linux / macOS???
The DLLs used to be imported in S# is not Windows native DLLs, but .NET DLLs so you can use it fine
on Linux / macOS.

## How to make a library for the S# programming language?

1 : Create a class library with .NET 7 as runtime.

2 : Add the S# DLL file in references

3 : Open the C# file into your class library and change the class name to `Library` and the namespace to `SSLib` and inherit your class from `SSharp.VM.VMLibrary` class.

4 : Override the `VMLibrary.LoadLibrary(SSharp.VM.Interpreter)` method.

5 : Put any code to add anything (functions, variables, types...) to the interpreter.

6 : Build your class library and copy the output DLL path.

7 : Import the DLL file by his path with the `import(string)` function of the standard library

Voila!

# Using S#

You can run a S# script by downloading & running the S# interpreter and the [.NET 7 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0), which will run the specified file.

If no file is specified, it will enter the [REPL](https://en.wikipedia.org/wiki/Read%E2%80%93eval%E2%80%93print_loop) (read-eval-print-loop), in which you can run expressions and scripts sequentially by typing them and pressing enter. Variables and program state are never cleared in the REPL. You can use the exit(0) function to exit the REPL.

Alternatively, you can also use [SiCode IDE](https://github.com/raphmar2021/sicodeide), and go to Interpreter -> Interpret to run the script.

You should save S# script files with the .ss file extension.



