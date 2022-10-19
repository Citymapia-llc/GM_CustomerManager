function setUrlHash(hashParameterName, hashParameterValue) {
    let theURL = new URL('https://dummy.com');             // create dummy url
    theURL.search = window.location.hash.substring(1);        // copy current hash-parameters without the '#' AS search-parameters
    theURL.searchParams.set(hashParameterName, hashParameterValue);   // set or update value with the searchParams-API
    window.location.hash = theURL.searchParams;                      // Write back as hashparameters
}
function PageTitle(title) {
    document.title = title;
}
function SetFav(iconlink) {
    var link = document.querySelector("link[rel~='icon']");
    if (!link) {
        link = document.createElement('link');
        link.rel = 'icon';
        document.getElementsByTagName('head')[0].appendChild(link);
    }
    link.href = iconlink;
}
function BeforeSend() {
    document.getElementById("payment-Loader").classList.add("processing-loader");
}
function AfterSend() {
    document.getElementById("payment-Loader").classList.remove("processing-loader");
}
function GetTimezoneValue() {
    // Returns the time difference in minutes between UTC time and local time.
    return new Date().getTimezoneOffset();
}
window.PlayAudio = (elementName) => {
    //document.getElementById(elementName).play();
    var s = document.getElementById(elementName);
    var promise = s.play();
    if (promise !== undefined) {
        promise.then(_ => {
            // Autoplay started!
        }).catch(error => {
            s.muted = false;
            s.autoplay = true;
            s.play();
            // Autoplay was prevented.
            // Show a "Play" button so that user can start playback.
        });
    }
}