# S# Standard Library
This library includes basic type conversion, math and I/O functions.

This library is required to the S# Interpreter to work.

# Namespace `stdlib`

This namespace contains some info about the used runtime / operating system

## Variable `os`

This variable contains the operating system version string.

For exemple : `Microsoft Windows NT 10.0.22621.0`

## Variable `runtime`

This variable contains the runtime string.

For exemple : `.NET 7.0.3`

# Namespace `stdio`

This namespace contains basic I/O functions

## Function `print(line)`
Prints text to screen

### Arguments
line (string) : The text to print.

## Function `println(line)`
Prints text to screen, but adds ```\n``` at the end

### Arguments
line (string) : The text to print.

## Function `readInput()`
Reads the user input, then returns it

### Returns
The user input.

# Namespace `stdmath`

This namespace contains some math functions

## Function `print(line)`
Prints text to screen

### Arguments
line (string) : The text to print.

## Function `floor(number)`
Returns the floor of a number.

### Returns
The floor of the provided number.

## Function `ceil(number)`
Returns the ceiling of a number.

### Returns
The ceiling of the provided number.

## Function `round(number)`
Round the number.

### Returns
The rounded number.
