using SSharp.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static Windows.Win32.PInvoke;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Graphics.Gdi;
using System.ComponentModel;
using Windows.Win32;

namespace SSharp.Desktop
{
    public unsafe class LibMain : VMLibrary
    {
        [DllImport("coredll.dll")]
        public static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        static char* GetStringPointer(string str)
        {
            fixed (char* ptr = str)
            {
                return ptr;
            }
        }
        static HWND hwnd;

        public void LoadLibrary(Interpreter i)
        {
            var n = i.DefineNamespace("desktop");

            n.DefineVariable("registerClass", new VMNativeFunction(new List<string>() { "string" }, (List<VMObject> arguments) =>
            {
                char* CLASS_NAME_P = GetStringPointer(((VMString)arguments[0]).Value);

                WNDCLASSEXW wcex = new();
                wcex.cbSize = (uint)sizeof(WNDCLASSEXW);
                wcex.style = 0;
                wcex.lpfnWndProc = WndProc;
                wcex.cbClsExtra = 0;
                wcex.cbWndExtra = 0;
                wcex.hInstance = new HINSTANCE(GetCurrentProcess());
                wcex.hIcon = HICON.Null;
                wcex.hCursor = HCURSOR.Null;
                wcex.hbrBackground = new HBRUSH(new IntPtr((int)SYS_COLOR_INDEX.COLOR_WINDOW + 1));
                wcex.lpszMenuName = new PCWSTR((char*)0);
                wcex.lpszClassName = new PCWSTR(CLASS_NAME_P);
                wcex.hIconSm = HICON.Null;

                return new VMBoolean(RegisterClassEx(wcex) == 0);
            }), null);
            n.DefineVariable("msgProc", new VMNativeFunction(new List<string>() { "number" }, (List<VMObject> arguments) =>
            {
                HWND hwnd = new(new((int)((VMNumber)arguments[0]).Value)); 
                MSG msg = new MSG();

                while (GetMessage(out msg, HWND.Null, 0, 0))
                {
                    TranslateMessage(msg);
                    DispatchMessage(msg);
                }

                return new VMNull();
            }), null);
            n.DefineVariable("createWindow", new VMNativeFunction(new List<string>() { "string", "string", "number", "number", "number", "number" }, (List<VMObject> arguments) =>
            {
                char* CLASS_NAME = GetStringPointer(((VMString)arguments[0]).Value);
                char* WINDOW_NAME = GetStringPointer(((VMString)arguments[1]).Value);
                int X = (int)((VMNumber)arguments[2]).Value;
                int Y = (int)((VMNumber)arguments[3]).Value;
                int Width = (int)((VMNumber)arguments[4]).Value;
                int Height = (int)((VMNumber)arguments[5]).Value;

                int hwnd = (int)CreateWindowEx((WINDOW_EX_STYLE)0, new(CLASS_NAME), new(WINDOW_NAME), WINDOW_STYLE.WS_OVERLAPPEDWINDOW, X, Y, Width, Height, HWND.Null, HMENU.Null, new HINSTANCE(GetCurrentProcess())).Value;

                return new VMNumber(hwnd);
            }), null);

            n.DefineVariable("setWindowVisibility", new VMNativeFunction(new List<string>() { "number", "boolean" }, (List<VMObject> arguments) =>
            {
                HWND hwnd = new(new((int)((VMNumber)arguments[0]).Value));
                bool visible = ((VMBoolean)arguments[1]).Value;

                if (visible)
                    ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_SHOWNORMAL);
                else
                    ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_HIDE);

                return new VMNumber(hwnd);
            }), null);

            // This is only to test
            n.DefineVariable("defwinproc", new VMNativeFunction(new List<string>() { }, (List<VMObject> arguments) =>
            {
                string CLASS_NAME = "Sample Window Class";
                string WINDOW_NAME = "Learn to program Windows";
                char* CLASS_NAME_P = GetStringPointer(CLASS_NAME);
                char* WINDOW_NAME_P = GetStringPointer(WINDOW_NAME);

                WNDCLASSEXW wcex = new();
                wcex.cbSize = (uint)sizeof(WNDCLASSEXW);
                wcex.style = 0;
                wcex.lpfnWndProc = WndProc;
                wcex.cbClsExtra = 0;
                wcex.cbWndExtra = 0;
                wcex.hInstance = new HINSTANCE(GetCurrentProcess());
                wcex.hIcon = HICON.Null;
                wcex.hCursor = HCURSOR.Null;
                wcex.hbrBackground = new HBRUSH(new IntPtr((int)SYS_COLOR_INDEX.COLOR_WINDOW + 1));
                wcex.lpszMenuName = new PCWSTR((char*)0);
                wcex.lpszClassName = new PCWSTR(CLASS_NAME_P);
                wcex.hIconSm = HICON.Null;


                if (RegisterClassEx(wcex) == 0)
                {
                    throw new Win32Exception("Window Registration failed");
                }

                hwnd = CreateWindowEx((WINDOW_EX_STYLE)0, new PCWSTR(CLASS_NAME_P), new PCWSTR(WINDOW_NAME_P), WINDOW_STYLE.WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND.Null, HMENU.Null, new HINSTANCE(GetCurrentProcess()), null);

                if (hwnd == HWND.Null)
                {
                    throw new Win32Exception("The window can't be created");
                }

                ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_SHOWNORMAL);

                MSG msg = new MSG();

                while (GetMessage(out msg, HWND.Null, 0, 0))
                {
                    TranslateMessage(msg);
                    DispatchMessage(msg);
                }

                return new VMNull();
            }), null);
        }

        static LRESULT WndProc(HWND hwnd, uint msg, WPARAM wParam, LPARAM lParam)
        {
            switch (msg)
            {
                case WM_CREATE:

                    break;
                case WM_DESTROY:
                    PostQuitMessage(0);
                    break;
                case WM_PAINT:
                    PAINTSTRUCT ps;
                    HDC hdc = BeginPaint(hwnd, out ps);
                    FillRect(hdc, &ps.rcPaint, new HBRUSH(new IntPtr((int)SYS_COLOR_INDEX.COLOR_WINDOW + 1)));
                    EndPaint(hwnd, ps);
                    break;
            }
            return DefWindowProc(hwnd, msg, wParam, lParam);
        }
    }
}
