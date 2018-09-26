AiplugsElements.register("aiplugs-code", class extends Stimulus.Controller {
    static get targets() {
        return ["view","input"];
    }
    edit() {
        const target = this.container;
        const template = this.element.querySelector("template");
        const id = this.editorId || (this.editorId = "editor-" + (this.inputTarget.id || (~~(Math.random() * Math.pow(2, 16))).toString(16)));
        const blade = target.querySelector(`#${id}`);

        if (!blade && template) {
            const content = template.content.cloneNode(true);
            content.firstElementChild.setAttribute('id', id);
            target.appendChild(content);

            this.trySetup(id);
        }
    }
    trySetup(id, timeout) {
        setTimeout(() => {
            this.setup(id);
        }, timeout || 0);
    }
    setup(id) {
        const blade = this.container.querySelector(`#${id}`);
        const close = blade.querySelector('.aiplugs-code__close');
        const cancel = blade.querySelector('.aiplugs-code__cancel');
        let editor = blade.querySelector('.aiplugs-code__editor');
        if (editor) {
            if (editor.classList.contains("aiplugs-tinymce")) {
                editor = this.application.resolve(editor, "aiplugs-tinymce");
            }
            else if (editor.classList.contains("aiplugs-monaco")) {
                editor = this.application.resolve(editor, "aiplugs-monaco");
            }
        }
        if (editor == null) {
            this.trySetup(id, 10);
            return;
        }
        
        editor.value = this.value;
        
        if (close) {
            close.addEventListener('click', () => {
                if (editor) {
                    editor.value.then(value => {
                        this.value = value;
                        editor.close();
                    });
                }
                blade.remove();
            })
        }

        if (cancel) {
            cancel.addEventListener('click', () => {
                if (editor) {
                    editor.close();
                }
                blade.remove();
            })
        }
    }
    update() {
        this.element.classList.toggle("aiplugs-code--visible-all", this.visibleView);
    }
    toggleView() {
        this.visibleView = !this.visibleView;
    }
    get value() {
        return this.inputTarget.value;
    }
    set value(value) {
        this.inputTarget.value = value;
        this.viewTarget.innerText = value;
        hljs.highlightBlock(this.viewTarget);
    }
    get visibleView() {
        return this.data.get("visible-view") === "true";
    }
    set visibleView(value) {
        this.data.set("visible-view", value);
        this.update();
    }
    get container() {
        return document.querySelector(this.data.get("container")) || document.body;
    }
});