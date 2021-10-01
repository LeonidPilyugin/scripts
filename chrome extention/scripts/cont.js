// Этот скрипт перехватывает нажатие клавиши и передаёт её в скрипт prog.js.
// Также он открывает URL, которую ему передаёт prog.js.

// Обработчик передаёт код клавиши в prog.js.
function onKeyDown(e) {
    chrome.runtime.sendMessage({ key: e.code });
}

// Обработчик открывает ссылку, которую передаёт prog.js.
function onMessage(request, sender, sendResponse) {
    if(request.message == "close"){
        window.close();
    }
    else{
        window.open(request.url, "_self");
    }
}

// Присоединение обработчиков событий.
chrome.runtime.onMessage.addListener(onMessage);
addEventListener("keydown", onKeyDown);