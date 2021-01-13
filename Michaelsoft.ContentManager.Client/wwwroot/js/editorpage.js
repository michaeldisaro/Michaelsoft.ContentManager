class MediaUploader {
    constructor(loader) {
        this.loader = loader;
        this.url = '/Media/Upload';
    }

    upload() {
        return this.loader.file.then(file => new Promise((resolve, reject) => {
            this._initRequest();
            this._initListeners(resolve, reject, file);
            this._sendRequest(file);
        }));
    }

    abort() {
        if (this.xhr) {
            this.xhr.abort();
        }
    }

    _initRequest() {
        const xhr = this.xhr = new XMLHttpRequest();
        xhr.open('POST', this.url, true);
        xhr.responseType = 'json';
    }

    _initListeners(resolve, reject, file) {
        const xhr = this.xhr;
        const loader = this.loader;
        const genericErrorText = `Couldn't upload file: ${file.name}.`;

        xhr.addEventListener('error', () => reject(genericErrorText));
        xhr.addEventListener('abort', () => reject());
        xhr.addEventListener('load', () => {
            const response = xhr.response;
            if (!response || response.Error) {
                return reject(response && response.Error ? response.Error : genericErrorText);
            }
            resolve({
                default: response.Url
            });
        });
        
        if (xhr.upload) {
            xhr.upload.addEventListener('progress', evt => {
                if (evt.lengthComputable) {
                    loader.uploadTotal = evt.total;
                    loader.uploaded = evt.loaded;
                }
            });
        }
    }

    _sendRequest(file) {
        const data = new FormData();
        data.append('upload', file);
        // Important note: This is the right place to implement security mechanisms
        // like authentication and CSRF protection. For instance, you can use
        // XMLHttpRequest.setRequestHeader() to set the request headers containing
        // the CSRF token generated earlier by your application.
        this.xhr.send(data);
    }
}

function uploadImage(editor) {
    editor.plugins.get('FileRepository').createUploadAdapter = function (loader) {
        return new MediaUploader(loader);
    };
}

BalloonEditor
    .create(document.querySelector('#content-editor'), {extraPlugins: [uploadImage]})
    .catch(error => {
        console.error(error);
    });

function fillEditor() {
    document.getElementById('content-editor').innerHTML = document.getElementById('html-content').value;
}

function fillContent() {
    var editor = document.getElementById('content-editor');
    document.getElementById('html-content').value = editor.innerHTML;
    document.getElementById('text-content').value = editor.innerText;
}

document.getElementById("content-form").addEventListener("submit", fillContent);
fillEditor();