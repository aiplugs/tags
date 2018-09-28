AiplugsElements.register('aiplugs-dialog-template', class extends Stimulus.Controller {
    initialize() {
        const query = this.opener;
        if (query) {
            this.registerAll(query);
        }
    }
    registerAll(query) {
        for (let el of document.querySelectorAll(query)) {
            this.register(el);
        }
    }
    register(el,callback) {
        el.addEventListener('click', () => {
            this.open().then(el => { if (callback) callback(el); });
        });
    }
    open() {
        const tempalte = this.element;
        const node = tempalte.content.cloneNode(true);
        return new Promise(resolve => {
            node.firstElementChild.addEventListener('initialized', e => resolve(e.target));
            document.body.appendChild(node);
        });
    }
    get opener() {
        return this.data.get('open');
    }
})
AiplugsElements.register('aiplugs-dialog', class extends Stimulus.Controller {
    initialize() {
        this.initialized();
    }
    close() {
        this.element.remove();
    }
})