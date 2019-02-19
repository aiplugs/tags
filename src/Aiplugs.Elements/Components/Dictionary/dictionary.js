AiplugsElements.register("aiplugs-dictionary", class extends Stimulus.Controller {
    static get targets() {
        return ["items", "template", "message", "item"];
    }
    initialize() {
        this.element.closest("form").addEventListener("submit", e => {
            if (!this.validate()) {
                e.preventDefault();
                return false;
            }
        });
    }
    add() {
        const content = this.templateTarget.content.cloneNode(true);
        this.itemsTarget.appendChild(content);
        setTimeout(() => {
            const item = this.items.pop();
            item.itemKeyTarget.focus();
            item.name = this.name;
        }, 0);
    }
    update() {
        this.items.forEach(item => {
            item.update();
        });
    }
    validate() {
        const messages = [];
        const mk = this.regexKey;
        const mv = this.regexValue;
        const rk = this.regexKeyPattern;
        const rv = this.regexValuePattern;
        const items = this.items;

        items.forEach(item => {
            const mk = !rk.exec(item.key) ? this.regexKey : null;
            const mv = !rv.exec(item.value) ? this.regexValue : null;
            item.validKey = !mk;
            item.validValue = !mv;
            messages.push(mk);
            messages.push(mv);
        });
        
        for (let i = 0; i < items.length; i++) {
            for (let j = 0; j < i; j++) {
                if (items[i].key === items[j].key) {
                    items[j].validKey = false;
                    items[i].validKey = false;
                    messages.push(this.duplicateKey);
                }
            }
        }

        this.message = messages.filter((_, i) => _ !== null && messages.indexOf(_) === i).join("<br>");

        return !this.message;
    }
    get items() {
        return this.itemTargets.map(item => this.application.resolve(item, "aiplugs-dictionary-item"));
    }
    set message(value) {
        this.messageTarget.innerHTML = value;
    }
    get regexKey() {
        return this.data.get("regex-key");
    }
    get regexKeyPattern() {
        return new RegExp(this.data.get("regex-key-pattern") || "^.*$");
    }
    get regexValue() {
        return this.data.get("regex-value");
    }
    get regexValuePattern() {
        return new RegExp(this.data.get("regex-value-pattern") || "^.*$");
    }
    get duplicateKey() {
        return this.data.get("duplicate-key") || "Duplicate keys detected.";
    }
    get name() {
        return this.data.get('name') || '';
    }
    set name(value) {
        this.data.set('name', value);
    }
    setNamePrefix(prefix) {
        const name = prefix + '.' + (this.data.get('nameTemplate') || '');
        for (let item of this.children('aiplugs-dictionary-item')) {
            item.name = name;
        }
        this.name = name;
    }
});
AiplugsElements.register("aiplugs-dictionary-item", class extends Stimulus.Controller {
    static get targets() {
        return ["itemKey", "itemValue"];
    }
    update() {
        const name = this.name;
        if (name !== null) {
            this.itemValueTarget.name = name;
        }
    }
    remove() {
        const dict = this.application.closestRoot(this.element, "aiplugs-dictionary");
        this.element.remove();
        dict.validate();
    }
    get name() {
        const n = this.data.get("name") || "";
        const k = this.itemKeyTarget.value;
        return k ? `${n}[${k}]` : null;
    }
    set name(value) {
        this.data.set('name', value);
        this.update();
    }
    get key() {
        return this.itemKeyTarget.value;
    }
    get value() {
        return this.itemValueTarget.value;
    }
    set validKey(value) {
        this.itemKeyTarget.classList.toggle("aiplugs-dictionary__item--invalid", !value);
    }
    set validValue(value) {
        this.itemValueTarget.classList.toggle("aiplugs-dictionary__item--invalid", !value);
    }
});