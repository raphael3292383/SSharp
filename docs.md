# Desktop library

S# include a (in dev state) desktop library with some useful functions for making desktop apps.

## Variables
### Backdrop types
#### BDT_NONE : no backdrop
#### BDT_AUTO : let DWM choose automatically
#### BDT_MICA : Mica visual effect
#### BDT_MICAALT : Mica Alt visual effect (also called Tabbed)
#### BDT_ACRYLIC : Acrylic visual effect

### Window messages
#### WM_CREATE : first message sended by Windows (sended on window creation)
#### WM_DESTROY : a message than is sended by Windows when you click on close button / when you press ALT+F4
#### WM_PAINT : a message than is sended by Windows when the window needs to be repainted
#### WM_COMMAND : a message than is sended by Windows when you click on a control

## Functions

### `SampleWinProc()`
Sample window. Use it to test S# desktop capabilities.

### `RegisterClass(string)`
Register a class to be used in a window.

The argument passed in the function is the class name.

### `DefineWindowEvents()`
Define a new WindowEvents class with link to functions to be called when Windows send a message.

Returns the window events ID (also called weId)

### `SubscribeWindowEvent(number weId, number eventMessage, string functionName)`
Subscribe a function to a WindowEvents class by his weId

Returns the window events ID

### `CreateButton(number parentHwnd, string buttonName, number x, number y, number width, number height, number buttonId)`
Create a button.

To handle the button click event, add a ID to this button, subscribe a function to handle WM_COMMAND and check if the button ID is same as this button's ID

If yes, do whatever you want.

### `CreateWindow(number weId, string className, string windowName, number x, number y, number width, number height)`
Create a window.

Returns : the HWND of the created window, as number

### `ShowWindow(number hwnd, number showWindowCommand)`
Sets the visibility of a window.

The first argument passed is the HWND (or Window Handle) of the window.
The second argument passed is the show window command.

Show window commands : https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow 

### `SetBackdropType(number hwnd, number backdrop)`
Sets the backdrop for a window.

NOTE : The BDT_NONE, BDT_AUTO, BDT_MICA, BDT_MICAALT and BDT_ACRYLIC variables can be used with the second argument to describe the backdrop

### `SetDarkMode(number hwnd, bool setDarkMode)`
Sets the immersive dark mode for a window (introduced in Windows 10 1903)

### `MessageLoop(number)`
Runs a basic Win32 message loop.

The argument passed is the HWND (or Window Handle) of the window.

# Standard library

S# include a standard library with some useful functions for mathematics, type conversion and I/O and more!

## I/O

### `print(string)`
Prints the passed string to the console.

### `clear()`
Clear the console

### `readLine()`
Return the user input in string format

### `readKey()`
Return the key than the user pressed

### `setBackground(number background)`
Sets a custom background color

### `setForeground(number foreground)`
Sets a custom foreground color

## Mathematics

### `floor(number)`
Returns the floor of a number.

### `round(number)`
Rounds a number.

### `ceil(number)`
Returns the ceiling of a number.

## Types

### `str(number)`
Convert a number to a string

### `num(string)`
Convert a string to a number

### `typeof(object)`
Return the type of an object as string

## Misc

### `exit(number)`
Prints `Script exited with code <exitcode>` and exit the S# interpreter with the provided exit code.

### `callScript(string)`
Call/Run an external script

## Library Manager

### `import(string)`
Imports a external managed DLL file made for S#

# Threading library
S# includes a (very buggy) threading library to run scripts in a thread.

## Functions

### `CreateThread(string script)`
Create a thread with the provided script and returns the thread ID.

### `StartThread(number threadId)`
Starts a thread.

### `StopThread(number threadId)`
Stop a thread.


# File Management library
S# includes a file mto run scripts in a thread.

## Functions

### `GetFileContents(string file)`
Returns the contents of a file

### `SetFileContents(string file, string content)`
Replaces the content of a file with the provided content

### `FileExists(string file)`
Returns a boolean to know if a file exists

### `CreateFile(string file)`
Create a file with the provided file path

### `CopyFile(string file, string dest)`
Copy a file with the provided file paths

### `CopyFile(string file, string dest)`
Move a file with the provided file paths

### `EncryptFile(string file)`
Should encrypt a file with AES encryption and returns the password...

...but i didn't write the function yet

# S# syntax

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



