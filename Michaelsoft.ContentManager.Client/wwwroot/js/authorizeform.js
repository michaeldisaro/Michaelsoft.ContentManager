function loadCertificate() {
    let fr = new FileReader();
    fr.onload = function () {
        document.getElementById('certificate-text').value = fr.result;
    };
    fr.readAsText(document.getElementById('certificate-file').files[0]);
}

function encryptToken() {
    let token = document.getElementById('token').value;
    let privateKey = document.getElementById('certificate-text').value;
    let cipher = new JSEncrypt();
    cipher.setKey(privateKey);
    document.getElementById('encrypted-token').value = cipher.encrypt(token, true);
}

document.getElementById("authorize-form").addEventListener("submit", encryptToken);