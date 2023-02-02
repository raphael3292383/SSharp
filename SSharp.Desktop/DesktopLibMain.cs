using SSharp.VM;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices;
using static Windows.Win32.PInvoke;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Graphics.Gdi;
using System.ComponentModel;
using Windows.Win32;
using System.Diagnostics.Contracts;
using System.Drawing;
using Windows.Win32.UI.Controls;

namespace SSharp.Desktop
{
    public class WindowEvents
    {
        HWND TargetHWND;

        public void SetTargetHwnd(nint hwnd)
        {
            TargetHWND = new HWND(hwnd);
        }

        public nint GetTargetHwnd()
        {
            return TargetHWND.Value;
        }

        public uint WindowEventsID;
        public string WmCommandEventFunctionName;
        public string WmCreateEventFunctionName;
        public string WmDestroyEventFunctionName;
        public string WmPaintEventFunctionName;
    }

    public unsafe class DesktopLibMain : VMLibrary
    {
        HWND hwnd;
        Interpreter i;

        List<WindowEvents> wev = new();

        enum DwmWindowAttribute : uint
        {
            NCRenderingEnabled = 1,
            NCRenderingPolicy,
            TransitionsForceDisabled,
            AllowNCPaint,
            CaptionButtonBounds,
            NonClientRtlLayout,
            ForceIconicRepresentation,
            Flip3DPolicy,
            ExtendedFrameBounds,
            HasIconicBitmap,
            DisallowPeek,
            ExcludedFromPeek,
            Cloak,
            Cloaked,
            FreezeRepresentation,
            PassiveUpdateMode,
            UseHostBackdropBrush,
            UseImmersiveDarkMode = 20,
            WindowCornerPreference = 33,
            BorderColor,
            CaptionColor,
            TextColor,
            VisibleFrameBorderThickness,
            SystemBackdropType,
            Last
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, ref int attrValue, int attrSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        private char* GetStringPointer(string str)
        {
            fixed (char* ptr = str)
            {
                return ptr;
            }
        }

        public void AbortThread(Thread thread) => thread.Interrupt();

        public override void LoadLibrary(Interpreter i)
        {
            base.LoadLibrary(i);

            Console.WriteLine("Win32 Desktop Library version 1.0");
            Console.WriteLine("Made by RaphMar2019, Original APIs by Microsoft");

            INITCOMMONCONTROLSEX iccex = new();
            iccex.dwICC = INITCOMMONCONTROLSEX_ICC.ICC_STANDARD_CLASSES;
            iccex.dwSize = (uint)sizeof(INITCOMMONCONTROLSEX);
            InitCommonControlsEx(iccex);

            if (!Environment.OSVersion.VersionString.StartsWith("Microsoft"))
            {
                Console.WriteLine("You need to run Microsoft Windows for this library to work.");
                return;
            }

            this.i = i;

            i.DefineVariable("DefineWindowEvents", new VMNativeFunction(new List<string> { }, (List<VMObject> arguments) =>
            {
                //HWND hWnd = new HWND((int)((VMNumber)arguments[0]).Value);
                WindowEvents we = new();
                //we.SetTargetHwnd(hWnd);
                we.WindowEventsID = (uint)new Random().Next();
                //we.WmDestroyEventFunctionName = destroyFunctionName;
                //we.WmCreateEventFunctionName = createFunctionName;
                //we.WmPaintEventFunctionName = paintFunctionName;
                //we.WmCommandEventFunctionName = commandFunctionName;

                wev.Add(we);

                return new VMNumber((double)we.WindowEventsID);
            }), null);
            i.DefineVariable("SubscribeWindowEvent", new VMNativeFunction(new List<string> { ("number"), ("number"), ("string") }, (List<VMObject> arguments) =>
            {
                //HWND hWnd = new HWND((int)((VMNumber)arguments[0]).Value);
                int weId = (int)((VMNumber)arguments[0]).Value;
                int eventMsg = (int)((VMNumber)arguments[1]).Value;
                string funcName = ((VMString)arguments[2]).Value;

                WindowEvents we = null;
                bool found = false;
                foreach (WindowEvents w in wev)
                {
                    if (w.GetTargetHwnd() == hwnd)
                    {
                        we = w;
                        found = true;
                    }
                }

                switch (eventMsg)
                {
                    case (int)WM_CREATE:
                        we.WmCreateEventFunctionName = funcName;
                        break;
                    case (int)WM_DESTROY:
                        we.WmDestroyEventFunctionName = funcName;
                        break;
                    case (int)WM_PAINT:
                        we.WmPaintEventFunctionName = funcName;
                        break;
                    case (int)WM_COMMAND:
                        we.WmCommandEventFunctionName = funcName;
                        break;
                }
                return new VMNumber((double)we.WindowEventsID);
            }), null);
            i.DefineVariable("RegisterClass", new VMNativeFunction(new List<string> { ("string") }, (List<VMObject> arguments) =>
            {
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
                wcex.lpszClassName = new PCWSTR(GetStringPointer(((VMString)arguments[0]).Value));
                wcex.hIconSm = HICON.Null;

                int result = (int)RegisterClassEx(wcex);

                return new VMNumber(result);
            }), null);
            i.DefineVariable("CreateWindow", new VMNativeFunction(new List<string> { ("number"), ("string"), ("string"), ("number"), ("number"), ("number"), ("number") }, (List<VMObject> arguments) =>
            {
                int weId = (int)((VMNumber)arguments[0]).Value;
                PCWSTR className = new PCWSTR(GetStringPointer(((VMString)arguments[1]).Value));
                PCWSTR windowName = new PCWSTR(GetStringPointer(((VMString)arguments[2]).Value));
                int x = (int)((VMNumber)arguments[3]).Value;
                int y = (int)((VMNumber)arguments[4]).Value;
                int width = (int)((VMNumber)arguments[5]).Value;
                int height = (int)((VMNumber)arguments[6]).Value;

                HWND hwnd = CreateWindowEx((WINDOW_EX_STYLE)0, className, windowName, WINDOW_STYLE.WS_OVERLAPPEDWINDOW, x, y, width, height, HWND.Null, HMENU.Null, new HINSTANCE(GetCurrentProcess()), null);

                WindowEvents wee = GetWindowEvents((uint)weId);

                if (wee == null)
                {
                    return new VMNumber(0);
                }
                else
                {
                    if (String.IsNullOrEmpty(wee.WmCreateEventFunctionName))
                    {
                        wee.SetTargetHwnd(hwnd);
                        return new VMNumber((double)hwnd);
                    }
                    else
                    {
                        wee.SetTargetHwnd(hwnd);
                        i.CallFunctionByName(wee.WmCreateEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd) }, null);
                        return new VMNumber((double)hwnd);
                    }
                }

                return new VMNumber((double)hwnd);
            }), null);
            i.DefineVariable("CreateButton", new VMNativeFunction(new List<string> { ("number"), ("string"), ("number"), ("number"), ("number"), ("number"), ("number") }, (List<VMObject> arguments) =>
            {
                HWND parentHwnd = new HWND((int)((VMNumber)arguments[0]).Value);
                PCWSTR windowName = new PCWSTR(GetStringPointer(((VMString)arguments[1]).Value));
                PCWSTR buttonClass = new PCWSTR(GetStringPointer("BUTTON"));
                int x = (int)((VMNumber)arguments[2]).Value;
                int y = (int)((VMNumber)arguments[3]).Value;
                int width = (int)((VMNumber)arguments[4]).Value;
                int height = (int)((VMNumber)arguments[5]).Value;
                int buttonId = (int)((VMNumber)arguments[6]).Value;

                HWND hwnd = CreateWindowEx((WINDOW_EX_STYLE)0, buttonClass, windowName, WINDOW_STYLE.WS_TABSTOP | WINDOW_STYLE.WS_VISIBLE | WINDOW_STYLE.WS_CHILD | (WINDOW_STYLE)0x00000001, x, y, width, height, parentHwnd, new HMENU(buttonId), new HINSTANCE(GetCurrentProcess()), null);

                return new VMNumber((double)hwnd);
            }), null);
            i.DefineVariable("SetBackdropType", new VMNativeFunction(new List<string> { ("number"), ("number") }, (List<VMObject> arguments) =>
            {
                //HWND hWnd = new HWND((int)((VMNumber)arguments[0]).Value);
                HWND hwnd = new HWND((int)((VMNumber)arguments[0]).Value);
                int backdropType = (int)((VMNumber)arguments[1]).Value;

                //if (backdropType != 0 && backdropType != 1)
                //{
                    DwmExtendFrameIntoClientArea(hwnd, new MARGINS() { cxLeftWidth = -1, cxRightWidth = -1, cyBottomHeight = -1, cyTopHeight = -1 });
                //}
                //else
                //{
                    //DwmExtendFrameIntoClientArea(hwnd, new MARGINS() { cxLeftWidth = 0, cxRightWidth = 0, cyBottomHeight = 0, cyTopHeight = 0 });
                //}
                DwmSetWindowAttribute(hwnd, DwmWindowAttribute.SystemBackdropType, ref backdropType, sizeof(int));

                return new VMNull();
            }), null);
            i.DefineVariable("SetDarkMode", new VMNativeFunction(new List<string> { ("number"), ("boolean") }, (List<VMObject> arguments) =>
            {
                //HWND hWnd = new HWND((int)((VMNumber)arguments[0]).Value);
                HWND hwnd = new HWND((int)((VMNumber)arguments[0]).Value);
                bool darkMode = ((VMBoolean)arguments[1]).Value;

                //if (backdropType != 0 && backdropType != 1)
                //{
                DwmExtendFrameIntoClientArea(hwnd, new MARGINS() { cxLeftWidth = -1, cxRightWidth = -1, cyBottomHeight = -1, cyTopHeight = -1 });
                //}
                //else
                //{
                //DwmExtendFrameIntoClientArea(hwnd, new MARGINS() { cxLeftWidth = 0, cxRightWidth = 0, cyBottomHeight = 0, cyTopHeight = 0 });
                //}
                int darkModeInt = darkMode ? 1 : 0;

                DwmSetWindowAttribute(hwnd, DwmWindowAttribute.UseImmersiveDarkMode, ref darkModeInt, sizeof(int));

                return new VMNull();
            }), null);
            i.DefineVariable("ShowWindow", new VMNativeFunction(new List<string> { ("number"), ("number") }, (List<VMObject> arguments) =>
            {
                HWND hWnd = new HWND((int)((VMNumber)arguments[0]).Value);
                int cmd = (int)((VMNumber)arguments[1]).Value;

                return new VMBoolean(ShowWindow(hWnd, (SHOW_WINDOW_CMD)cmd));
            }), null);
            i.DefineVariable("MessageLoop", new VMNativeFunction(new List<string> { ("number") }, (List<VMObject> arguments) =>
            {
                try
                {
                    
                    MSG msg = new MSG();

                    //void WhileCall()
                    //{
                    Console.WriteLine("Press ESC to quit the message loop (and close the window)");
                    Console.CancelKeyPress += (s, e) =>
                    {
                        ShowWindow(new HWND(new nint((int)((VMNumber)arguments[0]).Value)), SHOW_WINDOW_CMD.SW_HIDE);
                        DestroyWindow(new HWND(new nint((int)((VMNumber)arguments[0]).Value)));
                    };

                    while (GetMessage(out msg, new HWND(new nint((int)((VMNumber)arguments[0]).Value)), 0, 0) != new BOOL(false) && !Console.KeyAvailable)
                    {
                        TranslateMessage(msg);
                        DispatchMessage(msg);
                        if (msg.message == WM_DESTROY)
                        {
                            ShowWindow(new HWND(new nint((int)((VMNumber)arguments[0]).Value)), SHOW_WINDOW_CMD.SW_HIDE);
                            return new VMNull();
                        }
                    }
                    //}


                    return new VMNull();
                }
                catch
                {
                    return new VMBoolean(false);
                }
            }), null);
            
            // Testing functions
            i.DefineVariable("SampleWinProc", new VMNativeFunction(new List<string> { },(List<VMObject> arguments) =>
            {
                var uiThread = new Thread(new ThreadStart(() => {
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
                        Console.WriteLine("Window Registration failed");
                    }

                    hwnd = CreateWindowEx((WINDOW_EX_STYLE)0, new PCWSTR(CLASS_NAME_P), new PCWSTR(WINDOW_NAME_P), WINDOW_STYLE.WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, HWND.Null, HMENU.Null, new HINSTANCE(GetCurrentProcess()), null);

                    if (hwnd == HWND.Null)
                    {
                        Console.WriteLine("The window can't be created");
                    }

                    ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_SHOWNORMAL);

                }));
                
                uiThread.Start();
                return new VMNull();
            }), scope: null);

            // Constants
            i.DefineVariable("BDT_AUTO", new VMNumber(0), null);
            i.DefineVariable("BDT_NONE", new VMNumber(1), null);
            i.DefineVariable("BDT_MICA", new VMNumber(2), null);
            i.DefineVariable("BDT_ACRYLIC", new VMNumber(3), null);
            i.DefineVariable("BDT_MICAALT", new VMNumber(4), null);


            i.DefineVariable("WM_CREATE", new VMNumber((int)WM_CREATE), null);
            i.DefineVariable("WM_DESTROY", new VMNumber((int)WM_DESTROY), null);
            i.DefineVariable("WM_PAINT", new VMNumber((int)WM_PAINT), null);
            i.DefineVariable("WM_COMMAND", new VMNumber((int)WM_COMMAND), null);
        }
        LRESULT WndProc(HWND hwnd, uint msg, WPARAM wParam, LPARAM lParam)
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
                    FillRect(hdc, &ps.rcPaint, CreateSolidBrush(new COLORREF(0x000000)));
                    EndPaint(hwnd, ps);
                    break;
            }

            if (msg != WM_CREATE) GetAndCallEventFunction(hwnd, msg, wParam, lParam);

            return DefWindowProc(hwnd, msg, wParam, lParam);
        }

        int LoWord(int Number)
        {
            return Number & 0xffff;
        }

        WindowEvents GetWindowEvents(uint weId)
        {
            // Get target window events class
            WindowEvents we = null;
            bool found = false;
            foreach (WindowEvents w in wev)
            {
                if (w.WindowEventsID == weId)
                {
                    we = w;
                    found = true;
                }
            }

            return we;
        }

        WindowEvents GetAndCallEventFunction(nint hwnd, uint msg, WPARAM wParam, LPARAM lParam)
        {
            // Get target window events class
            WindowEvents we = null;
            bool found = false;
            foreach (WindowEvents w in wev)
            {
                if (w.GetTargetHwnd() == hwnd)
                {
                    we = w;
                    found = true;
                }
            }

            //if (found) Console.WriteLine("Window found"); else Console.WriteLine("Window not found");

            // Get raised event and call target function
            switch (msg)
            {
                case WM_COMMAND:
                    if (!String.IsNullOrEmpty(we.WmCommandEventFunctionName))
                        i.CallFunctionByName(we.WmCommandEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd), new VMNumber(LoWord((int)wParam.Value)) }, null);
                    break;
                case WM_CREATE:
                    if (!String.IsNullOrEmpty(we.WmCreateEventFunctionName))
                        i.CallFunctionByName(we.WmCreateEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd)}, null);
                    break;
                case WM_DESTROY:
                    if (!String.IsNullOrEmpty(we.WmDestroyEventFunctionName))
                        i.CallFunctionByName(we.WmDestroyEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd) }, null);
                    break;
                case WM_PAINT:
                    if (!String.IsNullOrEmpty(we.WmPaintEventFunctionName))
                        i.CallFunctionByName(we.WmPaintEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd) }, null);
                    break;
            }

            return we;
        }
        public WindowEvents GetAndCallEventFunction(uint weId, uint msg)
        {
            // Get target window events class
            WindowEvents we = null;
            bool found = false;
            foreach (WindowEvents w in wev)
            {
                if (w.WindowEventsID == weId)
                {
                    we = w;
                    found = true;
                }
            }
            // ye bou
            // Get raised event and call target function
            switch (msg)
            {
                case WM_CREATE:
                    i.CallFunctionByName(we.WmCreateEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd)}, null);
                    break;
                case WM_DESTROY:
                    i.CallFunctionByName(we.WmDestroyEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd) }, null);
                    break;
                case WM_PAINT:
                    i.CallFunctionByName(we.WmPaintEventFunctionName, new List<VMObject> { new VMNumber((double)hwnd) }, null);
                    break;
            }

            return we;
        }
    }
}