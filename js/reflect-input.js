var themeManager = JSON.parse(window.localStorage.getItem("clientPreference"));
var primaryColor = '#3eaf7c';
var baseColor = '#ffffff';

if (themeManager) {
    if (themeManager.PrimaryColor) {
        primaryColor = themeManager.PrimaryColor;
    }
    if (themeManager.IsDarkMode) {
        baseColor = '#1b1f22';
    }
}
var elements = document.getElementsByClassName('fsh-wasm');
for (var i = 0; i < elements.length; i++) {
    elements[i].style.backgroundImage = 'linear-gradient(-120deg,' + primaryColor + ' 50%,' + baseColor + ' 50%)';
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);

    triggerFileDownload(fileName, url);

    URL.revokeObjectURL(url);
}

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}