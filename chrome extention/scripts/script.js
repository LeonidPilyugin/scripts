// Этот скрипт загружается из страницы page.html и нужен для переключения её на другую.
// Chrome почему-то не выполняет этот код, если я его вставлю в тег <script> страницы.

var goodURL = "https://www.google.com/"; // Нормальный поисковик
var badURL = "https://litsearch.surge.sh/"; // Horny поисковик

chrome.tabs.create({ url: localStorage["horny"] == "true" ? badURL : goodURL }); // Открытие в новой вкладке
window.close(); // Закрытие текущей вкладки