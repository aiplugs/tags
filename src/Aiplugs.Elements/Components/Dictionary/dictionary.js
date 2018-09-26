AiplugsElements.register("aiplugs-dictionary", class extends Stimulus.Controller {
    static get targets() {
        return ["items", "template", "message", "item"];
    }
    initialize() {
        this.element.closest("form").addEventListener("submit", e => {
            this.validate();
            if (!this.message) {
                e.preventDefault();
                return false;
            }
        });
    }
    add() {
        const content = this.templateTarget.content.cloneNode(true);
        this.itemsTarget.appendChild(content);
        setTimeout(() => {
            this.items.pop().itemKeyTarget.focus();
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

        this.message = messages.filter((_, i) => _ != null && messages.indexOf(_) === i).join("<br>");
    }
    get items() {
        return this.itemTargets.map(item => $$(item, "aiplugs-dictionary-item"));
    }
    set message(value) {
        this.messageTarget.innerHTML = value;
    }
    get regexKey() {
        return this.data.get("regex-key");
    }
    get regexKeyPattern() {
        return new RegExp(this.data.get("regex-key-pattern") || ".*");
    }
    get regexValue() {
        return this.data.get("regex-value");
    }
    get regexValuePattern() {
        return new RegExp(this.data.get("regex-value-pattern") || ".*");
    }
    get duplicateKey() {
        return this.data.get("duplicate-key") || "Duplicate keys detected.";
    }
});
AiplugsElements.register("aiplugs-dictionary-item", class extends Stimulus.Controller {
    static get targets() {
        return ["itemKey", "itemValue"];
    }
    update() {
        const name = this.name;
        if (name != null) {
            this.itemValueTarget.name = name;
        }
    }
    remove() {
        const dict = $$closest(this.element, "aiplugs-dictionary");
        this.element.remove();
        dict.validate();
    }
    get name() {
        const n = this.data.get("name") || "";
        const k = this.itemKeyTarget.value;
        return k ? `${n}[${k}]` : null;
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