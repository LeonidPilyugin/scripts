// Этот скрипт нужен для обновления списка ссылок в localStorage.
// Он запускается при каждом запускеы браузера.

// Установка списка ссылок.
function setData(strings) {
    // Очистка localStorage.
    localStorage.clear();
    // Обновление версии.
    localStorage["version"] = parseFloat(substrings[0]);
    // Установка размера списка.
    localStorage["size"] = strings.length - 1;
    // Добавление ссылок.
    for (let i = 1; i < strings.length; i++) {
        localStorage[i - 1] = strings[i];
    }
}

// Запрос на список ссылок.
let xhr = new XMLHttpRequest();
xhr.open('GET', 'https://drive.google.com/uc?export=download&confirm=no_antivirus&id=1JLOWXC603p7fiMHvtvQJvddNkpDmQQX7', true);
xhr.responseType = 'string';
xhr.onload = function (e) {
    // Разбивка на подстроки.
    let substrings = this.response.split("\n");
    // Проверка версии. Если текущая версия списка больше последней скачанной версии, список обновляется.
    if (localStorage["version"] == null || parseFloat(localStorage["version"]) < parseFloat(substrings[0]))
        setData(substrings);
};
xhr.send();