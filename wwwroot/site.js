
window.SetStylesheet = (styleSheet) => {
    document.getElementById(`RadzenStylesheet`).setAttribute(`href`, styleSheet);
}
window.SetSyncfusionStylesheet = (styleSheet) => {
    document.getElementById(`SyncfusionStylesheet`).setAttribute(`href`, styleSheet);
}

window.GetTime = () => {
    return Date.now();
}