// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function MyTestCors(host, uri, methodName, includeHeaders = false) {
    const resultSpan = document.getElementById('result');
    const myHeaders = includeHeaders ? { 'x-teste': 'text/plain' } : {};

    fetch(`${host}${uri}`,
        {
            method: methodName,
            headers: myHeaders,
        }).then(response => {
            if (response.ok) {
                response.text().then(text => {
                    resultSpan.innerText = text;
                });
            }
            else {
                resultSpan.innerText = response.status;
            }
        })
        .catch(() => resultSpan.innerText = 'See F12 Console for error');
}
