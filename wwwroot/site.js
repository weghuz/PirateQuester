
window.SetStylesheet = (styleSheet) => {
    document.getElementById(`AppStyleSheet`).setAttribute(`href`, styleSheet);
}
window.SetSyncfusionStylesheet = (styleSheet) => {
    document.getElementById(`SyncfusionStylesheet`).setAttribute(`href`, styleSheet);
}

window.GetTime = () => {
    return Date.now();
}

window.download = (filename, text) => {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/json;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', `${filename}.json`);
    
    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}