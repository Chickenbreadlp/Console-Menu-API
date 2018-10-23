using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuAPI
{
    public class ConsoleMenu
    {
        protected string[] _entries;
        protected string _title;
        protected string _seperator;
        protected char _selector;

        /// <summary>
        /// Constructs a Menu Object
        /// </summary>
        /// <param name="entries">A String array containing all menu items</param>
        /// <param name="selector">Character used for indicating current selection</param>
        /// <param name="seperator">Sets a custom string for list seperation. menu Items exactly matching the seperator, will not be selectable</param>
        public ConsoleMenu(string[] entries, char selector = '>', string seperator = "")
        {
            _entries = (string[])entries.Clone();
            _seperator = seperator;
            _selector = selector;
            _title = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
        /// <summary>
        /// Constructs a Menu Object
        /// </summary>
        /// <param name="entries">A String array containing all menu items</param>
        /// <param name="title">Sets a custom menu title</param>
        /// <param name="selector">Character used for indicating current selection</param>
        /// <param name="seperator">Sets a custom string for list seperation. menu Items exactly matching the seperator, will not be selectable</param>
        public ConsoleMenu(string[] entries, string title, char selector = '>', string seperator = "")
        {
            _entries = (string[])entries.Clone();
            _seperator = seperator;
            _selector = selector;
            _title = title;
        }

        /// <summary>
        /// Shows the menu and allows user to select something from the menu
        /// </summary>
        /// <returns>The index of the selected item with just the selectable items</returns>
        public virtual int Show()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine(_title);
            string underline = "";
            for (int i = 0; i < _title.Length; i++)
            {
                underline += "-";
            }
            Console.WriteLine(underline + "\n");

            Console.WriteLine(_selector + " " + _entries[0]);
            for (int i = 1; i < _entries.Length; i++)
            {
                Console.WriteLine("  " + _entries[i]);
            }

            ConsoleKey input = 0;
            int cSelect = 0;

            do
            {
                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, 3 + cSelect);
                    Console.Write(" ");

                    cSelect++;
                    if (cSelect >= _entries.Length)
                    {
                        cSelect = 0;
                    }
                    if (_entries[cSelect] == _seperator)
                    {
                        cSelect++;
                    }

                    Console.SetCursorPosition(0, 3 + cSelect);
                    Console.Write(_selector);
                }
                else if (input == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, 3 + cSelect);
                    Console.Write(" ");

                    cSelect--;
                    if (cSelect < 0)
                    {
                        cSelect = _entries.Length - 1;
                    }
                    if (_entries[cSelect] == _seperator)
                    {
                        cSelect--;
                    }

                    Console.SetCursorPosition(0, 3 + cSelect);
                    Console.Write(_selector);
                }
            } while (input != ConsoleKey.Enter && input != ConsoleKey.Escape);

            int tmpSelect = -1;

            if (input == ConsoleKey.Enter)
            {
                tmpSelect = cSelect;
                for (int i = 0; i < cSelect; i++)
                {
                    if (_entries[i] == _seperator)
                    {
                        tmpSelect -= 1;
                    }
                }
            }

            Console.CursorVisible = true;
            return tmpSelect;
        }
    }

    public class ConsoleMenuRowed : ConsoleMenu
    {
        protected int _lines;
        protected int _seperation;

        /// <summary>
        /// Constructs a Rowed Menu Object
        /// </summary>
        /// <param name="entries">A String array containing all menu items</param>
        /// <param name="lines">Amount of lines, before the menu jumps to the next row</param>
        /// <param name="seperation">Amount of space, between the indiviual rows</param>
        /// <param name="selector">Character used for indicating current selection</param>
        /// <param name="seperator">Sets a custom string for list seperation. menu Items exactly matching the seperator, will not be selectable</param>
        public ConsoleMenuRowed(string[] entries, int lines, int seperation = 2, char selector = '>', string seperator = "") : base(entries, selector, seperator)
        {
            _lines = lines;
            _seperation = seperation;
        }
        /// <summary>
        /// Constructs a Rowed Menu Object
        /// </summary>
        /// <param name="entries">A String array containing all menu items</param>
        /// <param name="title">Sets a custom menu title</param>
        /// <param name="lines">Amount of lines, before the menu jumps to the next row</param>
        /// <param name="seperation">Amount of space, between the indiviual rows</param>
        /// <param name="selector">Character used for indicating current selection</param>
        /// <param name="seperator">Sets a custom string for list seperation. menu Items exactly matching the seperator, will not be selectable</param>
        public ConsoleMenuRowed(string[] entries, string title, int lines, int seperation = 2, char selector = '>', string seperator = "") : base(entries, title, selector, seperator)
        {
            _lines = lines;
            _seperation = seperation;
        }

        /// <summary>
        /// Shows the menu and allows user to select something from the menu
        /// </summary>
        /// <returns>The index of the selected item with just the selectable items</returns>
        public override int Show()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine(_title);
            string underline = "";
            for (int i = 0; i < _title.Length; i++)
            {
                underline += "-";
            }
            Console.WriteLine(underline + "\n");

            int rows = _entries.Length / _lines;
            int[] rowWidths = new int[rows];
            int cX = 0;

            for (int x = 0; x < rows; x++)
            {
                int cWidth = 0;
                for (int y = 0; y < _lines; y++)
                {
                    Console.SetCursorPosition(cX, y + 3);
                    if (x + y == 0)
                    {
                        Console.Write(_selector + " " + _entries[0]);
                    }
                    else
                    {
                        Console.Write("  " + _entries[y + (x * rows)]);
                    }

                    if (_entries[y + (x*rows)].Length > cWidth)
                    {
                        cWidth = _entries[y + (x * rows)].Length;
                    }
                }
                cX += cWidth + _seperation + 2;
                rowWidths[x] = cWidth + 2;
            }

            ConsoleKey input = 0;
            int cSelect = 0;

            do
            {
                int xPos;
                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.DownArrow)
                {
                    drawCursor(cSelect, ref rowWidths, ' ');

                    cSelect++;
                    if (cSelect >= _entries.Length)
                    {
                        cSelect = 0;
                    }
                    if (_entries[cSelect] == _seperator)
                    {
                        cSelect++;
                    }

                    drawCursor(cSelect, ref rowWidths, _selector);
                }
                else if (input == ConsoleKey.UpArrow)
                {
                    drawCursor(cSelect, ref rowWidths, ' ');

                    cSelect--;
                    if (cSelect < 0)
                    {
                        cSelect = _entries.Length - 1;
                    }
                    if (_entries[cSelect] == _seperator)
                    {
                        cSelect--;
                    }

                    drawCursor(cSelect, ref rowWidths, _selector);
                }
            } while (input != ConsoleKey.Enter && input != ConsoleKey.Escape);

            int tmpSelect = -1;

            if (input == ConsoleKey.Enter)
            {
                tmpSelect = cSelect;
                for (int i = 0; i < cSelect; i++)
                {
                    if (_entries[i] == _seperator)
                    {
                        tmpSelect -= 1;
                    }
                }
            }

            Console.CursorVisible = true;
            return tmpSelect;
        }

        /// <summary>
        /// Helper-Function to draw the cursor at the appropriate position
        /// </summary>
        /// <param name="cPos">Position inside the entries array</param>
        /// <param name="rowWidths">Widths of all used rows</param>
        /// <param name="symbol">The symbol that's supposed to be drawn</param>
        void drawCursor(int cPos, ref int[] rowWidths, char symbol)
        {
            int xPos = 0;
            for (int i = 0; i < cPos / _lines; i++)
            {
                xPos = rowWidths[i] + _seperation;
            }
            Console.SetCursorPosition(xPos, 3 + cPos % _lines);
            Console.Write(symbol);
        }
    }
}
