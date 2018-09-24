(function() {
    const path = Array.from(document.getElementsByTagName('script')).find(script => script.src.endsWith("monaco-settings.js")).src;
    window.MonacoEnvironment = { getWorkerUrl: function (workerId, label) { return path.replace('monaco-settings.js', 'monaco-editor-worker-loader-proxy.js') } };
    require.config({ paths: { 'vs': '//unpkg.com/monaco-editor/min/vs' } });
}())