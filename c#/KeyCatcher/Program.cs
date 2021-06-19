using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;


// Программа глобально отслеживает нажатие клавиш.
// Для начала работы нужно ввести пароль ABOBA латинскими буквами.
// При нажатии на N открывает следующую ссылку.
// При нажатии на N открывает предыдущую ссылку.
// При нажатии на R открывает рикрол.
// При нажатии на O запускает custom_error.vbs.
// При нажатии на S запускает custom_shutdown.vbs.
// При нажатии на D запускает custom_dj_watermelon.vbs.
// При нажатии на B запускает custom_beer.vbs.
// При нажатии на I запускает custom_rickroll.vbs.
// При нажатии на U обновляет data.txt с флешки.
// При нажатии на C открывает Сталина на ютубе.
// При нажатии на H перемешивает список ссылок и сбрасывает счетчик.
// При нажатии на Y открывает youtube.com.
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
        private static string[] references;
        private static int position;


        static Program()
        {
            aboba = new List<Keys>();
            references = File.ReadAllLines(@"..\data\data.txt");
            _hookID = IntPtr.Zero;
            _proc = HookCallback;
            mayWork = false;
            position = 0;
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

        // Обрабатывает нажатие клавиши
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

        // Выбирает нужное действие в зависимости от клавиши
        private static void ProcessKey(Keys key)
        {
            switch (key)
            {
                case Keys.E: CaseE(); break;
                case Keys.N: CaseN(); break;
                case Keys.R: CaseR(); break;
                case Keys.O: CaseO(); break;
                case Keys.S: CaseS(); break;
                case Keys.B: CaseB(); break;
                case Keys.D: CaseD(); break;
                case Keys.I: CaseI(); break;
                case Keys.C: CaseC(); break;
                case Keys.Y: CaseY(); break;
                case Keys.U: CaseU(); break;
                case Keys.H: CaseH(); break;
                case Keys.P: CaseP(); break;
            }
        }

        // Проверка пароля
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

        // При нажатии на E перестают обрабатываться клавиши до набора пароля
        private static void CaseE()
        {
            try
            {
                // Выдыть сообщение об ошибке
                Process.Start(@"scripts\exit_message.vbs");
                // Сбросить флаг
                mayWork = false;
            }
            catch
            {
                Process.Start(@"scripts\e_fail.vbs");
            }
        }

        // Открыть следующую ссылку
        private static void CaseN()
        {
            try
            {
                OpenNext();
            }
            catch
            {
                Process.Start(@"scripts\n_fail.vbs");
            }
        }

        // Открыть предыдущую ссылку
        private static void CaseP()
        {
            try
            {
                OpenPrevious();
            }
            catch
            {
                Process.Start(@"scripts\p_fail.vbs");
            }
        }

        // Открыть рикролл (ссылка)
        private static void CaseR()
        {
            try
            {
                Process.Start(@"scripts\rickroll.vbs");
            }
            catch
            {
                Process.Start(@"scripts\r_fail.vbs");
            }
        }

        // Открыть Сталина
        private static void CaseC()
        {
            try
            {
                Process.Start(@"scripts\stalin.vbs");
            }
            catch
            {
                Process.Start(@"scripts\c_fail.vbs");
            }
        }

        // Запуск custom_error.vbs
        private static void CaseO()
        {
            try
            {
                Process.Start(@"..\error\custom_error.vbs");
            }
            catch
            {
                Process.Start(@"scripts\o_fail.vbs");
            }
        }

        // Запуск custom_shutdown.vbs
        private static void CaseS()
        {
            try
            {
                Process.Start(@"..\shutdown\custom_shutdown.vbs");
            }
            catch
            {
                Process.Start(@"scripts\s_fail.vbs");
            }
        }

        // Запуск custom_dj_watermelon.vbs
        private static void CaseD()
        {
            try
            {
                Process.Start(@"..\dj_watermelon\custom_dj_watermelon.vbs");
            }
            catch
            {
                Process.Start(@"scripts\d_fail.vbs");
            }
        }

        // Запуск custom_beer.vbs
        private static void CaseB()
        {
            try
            {
                Process.Start(@"..\beer\custom_beer.vbs");
            }
            catch
            {
                Process.Start(@"scripts\b_fail.vbs");
            }
        }

        // Запуск custom_rickroll.vbs
        private static void CaseI()
        {
            try
            {
                Process.Start(@"..\rickroll\custom_rickroll.vbs");
            }
            catch
            {
                Process.Start(@"scripts\i_fail.vbs");
            }
        }

        // Открыть youtube.com
        private static void CaseY()
        {
            try
            {
                Process.Start(@"scripts\youtube.vbs");
            }
            catch
            {
                Process.Start(@"scripts\y_fail.vbs");
            }
        }

        // Перемешивает список и сбрасывает счетчик
        private static void CaseH()
        {
            try
            {
                Suffle(references);
                position = 0;
            }
            catch
            {
                Process.Start(@"scripts\h_fail.vbs");
            }
        }

        // Обновить data.txt с флешки. Копируемый файл не должен находиться в папках.
        // https://ds-release.ru/uznaem-bukvu-fleshki-drivetype-removable/
        private static void CaseU()
        {
            try
            {
                // Поиск флешки
                DriveInfo[] D = DriveInfo.GetDrives();
                
                foreach (DriveInfo DI in D)
                {
                    if (DI.DriveType == DriveType.Removable)
                    {
                        if (File.Exists(Convert.ToString(DI.Name) + @"\data.txt"))
                        {
                            // Замена файла (Replace отказался работать)
                            string text = File.ReadAllText(Convert.ToString(DI.Name) + @"data.txt");
                            File.WriteAllText(@"..\data\data.txt", text);
                            // Использование нового файла
                            references = File.ReadAllLines(@"..\data\data.txt");
                            return;
                        }
                    }
                }

                // Сообщение об отсутствии файла
                Process.Start(@"scripts\not_exist.vbs");
            }
            catch
            {
                Process.Start(@"scripts\copy_fail.vbs");
            }
        }

        // Перемешать ссылки из исходного файла.
        private static void Suffle(string[] array)
        {
            Random rand = new Random();
            string temp;
            int j;

            for (int i = array.Length - 1; i > 0; i--)
            {
                j = rand.Next(i + 1);
                
                temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
        }

        // Открыть следующую ссылку.
        private static void OpenNext()
        {
            if(position >= references.Length)
            {
                // Сбросить счётчик
                position = 0;
                // Вывести окно о просмотре всех ссылок
                Process.Start(@"scripts\all_viewed.vbs");
            }

            // Открыть следющую ссылку
            Process.Start(references[position]);

            // Увеличить счетчикс
            position++;
        }

        // Открыть предыдущую ссылку.
        private static void OpenPrevious()
        {
            if (position < 0)
            {
                // Сбросить счётчик
                position = references.Length - 1;
                // Вывести окно о просмотре всех ссылок
                Process.Start(@"scripts\all_viewed.vbs");
            }

            // Открыть следющую ссылку
            Process.Start(references[position]);

            // Увеличить счетчикс
            position--;
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