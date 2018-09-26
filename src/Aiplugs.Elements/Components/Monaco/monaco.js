AiplugsElements.register("aiplugs-monaco", class extends Stimulus.Controller {
    static get targets() {
        return ["progress"];
    }
    initialize() {
        this.init = new Promise((resolve, reject) => {
            require(['vs/editor/editor.main'], () => {
                const created = monaco.editor.onDidCreateEditor(() => {
                    this.progressTarget.remove();
                    created.dispose();
                });
                const editor = monaco.editor.create(this.element, { language: 'html' });
                window.addEventListener('resize', () => {
                    editor.layout();
                });
                resolve(editor);
            });
        });
        
    }
    close() {
        this.init.then(editor => {
            editor.dispose();
        });
    }
    get value() {
        return this.init.then(editor => editor.getValue());
    }
    set value(value) {
        this.init.then(editor => {
            editor.setValue(value);
        });
    }
});