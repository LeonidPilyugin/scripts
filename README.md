# scripts
scripts for school desk
Старые скрипты лежат в папке c#.

Расширение для Chrome лежит в папке extention.

Для начала работы нужно запустить браузер и открыть любую страницу.
Далее нужно ввести пароль aboba (капс и раскладка не влияют).
После ввода пароля при нажатии N в текущей вкладке открывается ссылка из списка.
При нажатии A ссылка, открытая в текущей вкладке добавляется к списку.
При нажатии R ссылка, открытая в текущей вкладке удаляется из списку.
При нажатии U список ссылок обновляется.
При нажатии E пароль сбрасывается. Для дальнейшей работы расширения нужно набрать aboba.

Структура:
* manifest.json - файл манифеста
* logo.png - иконка расширения
* setdata.js - скрипт, записывающий ссылки в хранилище браузера
* prog.js - скрипт, обрабатывающий нажатие клавиш
* cont.js - скрипт, реагирующий на нажатие клавиш и открывающий ссылки

При запуске браузера запускаются скрипты setdata.js и prog.js.
Первый записывает в хранилище браузера список ссылок и создаёт очередь показываемых.
Второй - обрабатывает нажатие клавиш.
При открытии любой страницы запускается скрипт cont.js.
Он отслеживает нажатие клавиши и передаёт её код в prog.js.
Также он открывает новые ссылки в текущей вкладке.
