# S# Desktop Library
This library provide functions to make desktop apps with S#

This library is only compatible with the Microsoft Windows since it uses the Win32 API to work.

# Namespace `desktop`

## Function `defwinproc()`
This function creates a basic window

## Function `registerClass(class)`
This function register a class than can be used to create a window

### Arguments
class (string) : The class name

### Returns
If the result of RegisterClassEx native function is 0

## Function `createWindow(className, windowName, x, y, width, height)`
This function asks Windows to create a window with the provided arguments

### Arguments
className (string) : The class name (it should be the same class name than you provided on registerClass function)
windowName (string) : The window name / title
x (number) : The X position of the window
y (number) : The Y position of the window
width (number) : The width of the window
height (number) : The height of the window

### Returns
The handle of the window (also called HWND)

## Function `setWindowVisibility(hwnd, visible)`
Sets the visibility of a window (visible or hidden)

### Arguments
hwnd (number) : The window handle
visible (boolean) : The window visibility (true for visible, false for hidden)

## Function `msgProc(hwnd)`
Create a basic message procedure

### Arguments
hwnd (number) : The window handle
