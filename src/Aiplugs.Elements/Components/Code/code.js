AiplugsElements.register("aiplugs-code", class extends Stimulus.Controller {
    static get targets() {
        return ["view","input"];
    }
    initialize() {
        this.inputTarget.addEventListener('change', () => {
            this._setValue(this.value);
        })
    }
    edit() {
        const target = this.container;
        const template = this.element.querySelector("template:not([data-controller='aiplugs-dialog-template'])");
        const id = this.editorId || (this.editorId = "editor-" + (this.inputTarget.id || (~~(Math.random() * Math.pow(2, 16))).toString(16)));
        const blade = target.querySelector(`#${id}`);
        
        if (!blade && template) {
            const content = template.content.cloneNode(true);
            content.firstElementChild.setAttribute('id', id);
            target.appendChild(content);
        }
    }
    update() {
        this.element.classList.toggle("aiplugs-code--visible-all", this.visibleView);
    }
    toggleView() {
        this.visibleView = !this.visibleView;
    }
    _setValue(value) {
        this.viewTarget.innerText = value;
        hljs.highlightBlock(this.viewTarget);
    }
    get value() {
        return this.inputTarget.value;
    }
    set value(value) {
        this.inputTarget.value = value;
        this._setValue(value);
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
    setNamePrefix(prefix) {
        this.inputTarget.name = prefix + '.' + (this.data.get('nameTemplate') || '');
    }
});