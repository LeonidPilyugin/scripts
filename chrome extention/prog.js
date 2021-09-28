// Этот скрипт обновляет список ссылок и обрабатывает нажатия клавиш.
// Он запускается при каждом запуске браузера.

// Флаг, указывающий, был ли введён пароль.
var mayWork = false;
// Пароль.
var password = ["KeyA", "KeyB", "KeyO", "KeyB", "KeyA"];
// Ссылка, на которой лежит список ссылок.
var dataURL = "https://drive.google.com/uc?export=download&confirm=no_antivirus&id=1JLOWXC603p7fiMHvtvQJvddNkpDmQQX7";
var passwordURL = "https://drive.google.com/uc?export=download&confirm=no_antivirus&id=1pEsPoOxauzkwbhgoCOvw5n1Hz3sHcf-c"
// Массив с вводимыми клавишами для проверки пароля.
var passwordArray = [];
// Список ссылок.
var data = [];
// Текущая ссылка.
var thisURL;

// Эта функция загружает список ссылок.
function setURLList() {
    // Запрос на список ссылок.
    let xhrData = new XMLHttpRequest();
    xhrData.open("GET", dataURL, true);
    xhrData.onload = function (e) {
        // В случае успеха скрипт обновляет ссылки.
        if (this.status == 200) {
            // Разбивка на подстроки.
            let substrings = this.response.split("\n");
            // Проверка версии. Если текущая версия списка больше последней скачанной версии, список обновляется.
            if (localStorage["version"] == null || parseFloat(localStorage["version"]) < parseFloat(substrings[0])) {
                // Очистка localStorage.
                localStorage.clear();
                // Обновление версии.
                localStorage["version"] = parseFloat(substrings[0]);
                // Установка размера списка.
                localStorage["size"] = substrings.length - 1;
                // Добавление ссылок и их запись в массив.
                var dataa = [];
                for (let i = 1; i < substrings.length; i++) {
                    localStorage[i - 1] = substrings[i];
                    dataa[i - 1] = substrings[i];
                }
                localStorage["data"] = JSON.stringify(dataa);
            }
        }

        // Проигрывание звука.
        var audio = new Audio();
        audio.src = "whatsapp.mp3";
        audio.autoplay = true;

    };
    // Загрузка ссылок из localStorage и перемешка массива.
    setDataa();
    shuffle(data);

    // Запрос на пароль.
    let xhrPassword = new XMLHttpRequest();
    xhrPassword.open("GET", passwordURL, true);
    xhrPassword.onload = function (e) {
        // В случае успеха скрипт обновляет пароль.
        if (this.status == 200) {
            password = JSON.parse(this.response);
        }
    }
    xhrData.send();
    xhrPassword.send();
}

// Обработчик события нажатия клавиши.
function onKeyDown(e) {
    // Если введён пароль, обработать клавишу. Если нет, проверить пароль.
    if (mayWork) {
        switch (e) {
            case "KeyN": next(); break;
            case "KeyE": exit(); break;
            case "KeyU": updateData(); alert("Data updated"); break;
            case "KeyA": addURL(); break;
            case "KeyR": removeURL(); break;
        }
    }
    else {
        checkPassword(e);
    }
}

// Проверка пароля.
function checkPassword(code) {
    // Добавление клавиши в конец списка.
    writeToPassword(code);
    // Проверить пароль.
    mayWork = equal(password, passwordArray);
}

// Запись в массив проверки пароля.
function writeToPassword(item) {
    // Добавить клавишу в конец массива.
    passwordArray.push(item);
    // Если в массиве больше элементов, чем в пароле, убрать первый элемент массива и сдвинуть очередь.
    if (passwordArray.length > password.length) passwordArray.shift();
}

// Проверка равенства элементов массива.
function equal(a, b) {
    // Вернуть false, если разные длины.
    if (a.length != b.length) return false;
    // Проверить все элементы.
    for (let i = 0; i < a.length; i++) {
        // Если элемент не совпадает, вернуть false.
        if (a[i] != b[i]) return false;
    }
    return true;
}

// Добавляет URL открытой вкладки к ссылкам.
function addURL() {
    chrome.tabs.query({ "active": true, "lastFocusedWindow": true }, function (tabs) {
        // Получить URL открытой вкладки.
        var url = tabs[0].url;
        // Проверить, есть ли такая ссылка в localStorage.
        var isInStorage = false;
        for (let i = 0; i < localStorage["size"]; i++) {
            if (localStorage[i] == url) isInStorage = true;
        }
        // Если там такой ссылки нет, добавить.
        if (!isInStorage) {
            localStorage[localStorage["size"]] = url;
            localStorage["size"] = ++localStorage["size"];
        }
        // Сообщение о добавлении URL.
        alert("URL " + url + " added");
    });
}

// Удаляет URL открытой вкладки из ссылок.
/*function removeURL() {
    chrome.tabs.query({ "active": true, "lastFocusedWindow": true }, function (tabs) {
        // Получить URL открытой вкладки.
        var url = tabs[0].url;

        // Удалить ссылку из localStorage.
        // Флаг, указывающий, было ли удалено значение.
        var hasRemoved = false;
        for (let i = 0; i < localStorage["size"]; i++) {
            // Если найдена URL, установить флаг.
            if (localStorage[i] == url) {
                hasRemoved = true;
            }
            // Если установлен флаг, сдвинуть остальные элементы и изменить size.
            if (hasRemoved) {
                // Обработка последнего элемента и изменение size.
                if (i == localStorage["size"] - 1) {
                    localStorage.removeItem(i);
                    localStorage["size"] = --localStorage["size"];
                }
                // Сдвиг элементов.
                else {
                    localStorage[i] = localStorage[i + 1];
                }
            }
        }
        // Сообщение об удалении URL.
        alert("URL " + url + " removed");
    });
}*/

// Удаляет URL открытой вкладки из ссылок.
function removeURL() {
    // Удалить ссылку из localStorage.
    // Флаг, указывающий, было ли удалено значение.
    var hasRemoved = false;
    for (let i = 0; i < localStorage["size"]; i++) {
        // Если найдена URL, установить флаг.
        if (localStorage[i] == thisURL) {
            hasRemoved = true;
        }
        // Если установлен флаг, сдвинуть остальные элементы и изменить size.
        if (hasRemoved) {
            // Обработка последнего элемента и изменение size.
            if (i == localStorage["size"] - 1) {
                localStorage.removeItem(i);
                localStorage["size"] = --localStorage["size"];
            }
            // Сдвиг элементов.
            else {
                localStorage[i] = localStorage[i + 1];
            }
        }
    }
    // Сообщение об удалении URL.
    alert("URL " + thisURL + " removed");
}

// Открывает следующую ссылку.
function next() {
    // Открыть следующую ссылку и сдвинуть очередь.
    if (data[0] != undefined) {
        thisURL = data[0];
        openURL(data[0]);
        data.shift();
        changeData();
    }
    // Если очередь пуста (первый элемент не установлен), обновить очередь и вывести сообщение.
    else {
        alert("There aren't any url");
        updateData();
        setDataa();
        shuffle(data);
    }
}

// Отправляет ссылку открытой вкладке, чтобы открыть её.
function openURL(urlstr) {
    chrome.tabs.query({ 'active': true, 'lastFocusedWindow': true }, function (tabs) {
        chrome.tabs.sendMessage(tabs[0].id, { url: urlstr });
    });
}

// Блокировка работы (сброс пароля) и вывод сообщения о выходе.
function exit() {
    this.passwordArray = [];
    mayWork = false;
    alert("You have exited");
}

// Возвращает случайное целое число от 0 до number включительно.
function random(number) {
    return Math.floor(Math.random() * number);
}

// Перемешивает массив.
function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        let j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
}

// Задает массиву data ссылки из localStorage. С названием setData эта функция не работает.
function setDataa() {
    data = JSON.parse(localStorage["data"]);
}

// Обновление массива ссылок.
function updateData() {
    for (let i = 0; i < localStorage["size"]; i++) {
        data[i] = localStorage[i];
    }
    localStorage["data"] = JSON.stringify(data);
}

// Записывает массив data в localStorage.
function changeData() {
    localStorage["data"] = JSON.stringify(data);
}

// Добавление обработчика нажатия клавиши.
function onKeyDown2(request, sender, sendResponse) {
    onKeyDown(request.key);
}
chrome.runtime.onMessage.addListener(onKeyDown2);

// Загрузка списка ссылок.
setURLList();