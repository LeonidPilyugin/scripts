// Этот скрипт нужен для обновления списка ссылок в localStorage.
// Он запускается при каждом запускеы браузера.

// Версия списка ссылок.
var version = 0.1;

// Установка списка ссылок.
function setData() {
    // Очистка localStorage.
    localStorage.clear();
    // Добавление ссылок.
    let iter = 0;
    localStorage[iter++] = "https://www.youtube.com/watch?v=SjHSAbnU3_c";
    localStorage[iter++] = "https://www.youtube.com/watch?v=jtyFdK2Y33s&list=WL&index=15";
    localStorage[iter++] = "https://www.youtube.com/watch?v=rqtEGrSGFvw";
    localStorage[iter++] = "https://youtu.be/n1W0hqOs3bU";
    localStorage[iter++] = "https://youtu.be/im64PnwAc6o";
    // Число ссылок.
    localStorage["size"] = iter;
    // Обновление версии.
    localStorage["version"] = version;
    // Создание массива ссылок.
    var data = [];
    localStorage["data"] = JSON.stringify(data);
}

// Если версия меньше текущей или отсутствует, обновить ссылки.
if (localStorage["version"] == null || parseFloat(localStorage["version"]) < version) setData();