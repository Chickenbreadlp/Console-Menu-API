# Console Menu API
A Console Menu API for C#.NET
This might not be the most efficent code, but it gets the job done ^^

## Installation
To install the API, you add the compiled .dll as a Reference in VS and then you can use this API in the MenuAPI namespace.

## Usage
The API Uses the MenuAPI Namespace and has the following Objects:
- ConsoleMenu
  - The default Console Menu. All Entries are listed under each other
- ConsoleMenuRowed
  - Modifies the Console Menu to show up in multiple rows. The row-count is calculated dynamically depending on how many are needed due to the maximum hight and amount of entries

### ConsoleMenu
Constructor:
```
ConsoleMenu(string[] entries, char selector = '>', string seperator = "")
ConsoleMenu(string[] entries, string title, char selector = '>', string seperator = "")
```
- entries
  - Is an array of all menu options, that should show up.
- title
  - specifies the application/menu title
- selector
  - specifies the character to indicate the currently selected menu option
- sperator
  - specifies the seperator menu item (aka entries with the exact matching name are not selectable)

Other public functions:
```
int Show()
```
- Show()
  - Shows the menu and halts the current thread, until the user presses Enter or Escape
  - returns whatever entry the user selected or -1 if the user pressed Escape.
    - returned Index is the Index of the entries array **without** seperators

### ConsoleMenuRowed
Constructor:
```
ConsoleMenuRowed(string[] entries, int lines, int seperation = 2, char selector = '>', string seperator = "")
ConsoleMenuRowed(string[] entries, string title, int lines, int seperation = 2, char selector = '>', string seperator = "")
```
- entries
  - Is an array of all menu options, that should show up.
- title
  - specifies the application/menu title
- lines
  - specifies how many lines should be available for the entries, before wrapping back to the top in the next row
- seperation
  - specifies how many empty rows should be between individual rows of entries
- selector
  - specifies the character to indicate the currently selected menu option
- sperator
  - specifies the seperator menu item (aka entries with the exact matching name are not selectable)

Other public functions:
```
int Show()
```
- Show()
  - Shows the menu in multiple rows and halts the current thread, until the user presses Enter or Escape
  - returns whatever entry the user selected or -1 if the user pressed Escape.
    - returned Index is the Index of the entries array **without** seperators
