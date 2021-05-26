using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

// Программа глобально отслеживает нажатие клавиш.
// Для начала работы нужно ввести пароль ABOBA латинскими буквами.
// При нажатии на N открывает следующую ссылку.
// При нажатии на R открывает рикрол.
// При нажатии на E всплывает окно о закрытии,
// для дальнейшей работы приложения нужно заново набрать пароль.
// Все, что не закомментировано, скопировано отсюда: https://upread.ru/art.php?id=57

namespace KeyCatcher
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc;
        private static IntPtr _hookID;
        private static List<Keys> aboba; // Список символов для пароля.
        private static bool mayWork; // Флаг, указывающий, набран ли пароль.

        static Program()
        {
            aboba = new List<Keys>();
            _hookID = IntPtr.Zero;
            _proc = HookCallback;
            mayWork = false;
        }

        public static void Main()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        // Обрабатывает нажатие клавиши.
        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys vkCode = (Keys)Marshal.ReadInt32(lParam);

                if(mayWork)
                {
                    // Если пароль набран, то обрабатывается клавиша.
                    ProcessKey(vkCode);
                }
                else
                {
                    // Если нет, то обрабатывается набор пароля.
                    ProcessABOBA(vkCode);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // Выбирает нужное действие в зависимости от клавиши.
        private static void ProcessKey(Keys key)
        {
            switch (key)
            {
                case Keys.E: CaseE(); break;
                case Keys.N: CaseN(); break;
                case Keys.R: CaseR(); break;
            }
        }

        // Проверка пароля.
        private static void ProcessABOBA(Keys key)
        {
            aboba.Add(key);

            switch(aboba.Count)
            {
                case 1:
                    if(aboba[0] != Keys.A)
                    {
                        aboba.Clear();
                    }
                    break;

                case 2:
                    if (aboba[1] != Keys.B)
                    {
                        aboba.Clear();
                    }
                    break;

                case 3:
                    if (aboba[2] != Keys.O)
                    {
                        aboba.Clear();
                    }
                    break;

                case 4:
                    if (aboba[3] != Keys.B)
                    {
                        aboba.Clear();
                    }
                    break;

                case 5:
                    if (aboba[4] == Keys.A)
                    {
                        mayWork = true;
                    }
                    else
                    {
                        aboba.Clear();
                    }
                    break;

                default:
                    aboba.Clear();
                    break;
            }

            /*if (aboba.Count == 5)
            {
                if(aboba[0] == Keys.A && aboba[1] == Keys.B && aboba[2] == Keys.O &&
                    aboba[3] == Keys.B && aboba[4] == Keys.A)
                {
                    mayWork = true;
                }

            }*/
            return;
        }

        // При нажатии на E перестают обрабатываться клавиши до набора пароля.
        private static void CaseE()
        {
            Process.Start(@"exit_message.vbs");
            mayWork = false;
        }

        // Открыть новое видео.
        private static void CaseN()
        {
            Process.Start(@"random_video.vbs");
        }

        // Открыть рикролл
        private static void CaseR()
        {
            Process.Start(@"..\rickroll.vbs");
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}