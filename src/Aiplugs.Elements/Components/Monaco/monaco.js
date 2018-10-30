AiplugsElements.register('aiplugs-monaco', class extends Stimulus.Controller {
    static get targets() {
        return ['progress', 'textarea'];
    }
    initialize() {
        require(['vs/editor/editor.main'], () => {
            this.init();
        });
    }
    init() {
        const created = monaco.editor.onDidCreateEditor(() => {
            this.progressTarget.remove();
            created.dispose();
        });
        Promise.all([
            this.getText(this.valueFrom),
            this.getText(this.settingsFrom),
        ]).then(values => {
            const value = values[0] || this.textareaTarget.value;
            const settings = JSON.parse(values[1] || '{}');
            const options = Object.assign({ language: 'html' }, settings, { value: value } );
            const editor = monaco.editor.create(this.element, options);
            this.textareaTarget.value = value;
            editor.getModel().onDidChangeContent(() => {
                this.textareaTarget.value = editor.getValue();
            })
            editor.addCommand(monaco.KeyMod.CtrlCmd | monaco.KeyCode.KEY_S, function() {
                // prevent browser action;
            });
            window.addEventListener('resize', () => {
                editor.layout();
            });
            new ResizeObserver(() => {
                editor.layout();
            }).observe(this.element);
            this.disposable(() => {
                editor.dispose();
            })
        })
    }
    getText(url) {
        if (!url)
            return new Promise(resolve => { resolve(); });
        
        if (url.startsWith('#')) {
            const el = document.querySelector(url);
            return new Promise(resolve => { resolve(el.value || el.innerText); });
        }

        return fetch(url, {mode:'cors',credentials:'include'}).then(res => res.text());
    }
    get valueFrom() {
        return this.data.get('value-from') || '';
    }
    get settingsFrom() {
        return this.data.get('settings-from') || '';
    }
});