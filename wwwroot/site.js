
window.SetStylesheet = (styleSheet) =>
{
    document.getElementById(`RadzenStylesheet`).setAttribute(`href`, styleSheet);
}

window.GetTime = () => {
    return Date.now();
}