AiplugsElements.register("aiplugs-array", class extends Stimulus.Controller {
    static get targets() {
        return ["items", "add", "item"];
    }
    add() {
        const template = Array.from(this.itemsTarget.children).find(el => el.constructor === HTMLTemplateElement);
        if (template) {
            const content = template.content.cloneNode(true);
            this.itemsTarget.appendChild(content); 
            this.update();
        }
    }
    update() {
        setTimeout(() => {
            this.items.forEach((item, index) => {
                item.index = index;
            });
        }, 0)
        
    }
    get items () {
        return this.itemTargets.map(el => this.application.getControllerForElementAndIdentifier(el, "aiplugs-array-item")).filter(_ => _);
    }
});
AiplugsElements.register("aiplugs-array-item", class extends Stimulus.Controller {
    static get targets() {
        return ["label", "up", "down", "remove", "label"];
    }
    initialize() {
        this.update();
    }
    up() {
        if (!this.upDisabled) {
            const target = this.element.previousElementSibling;
            target.insertAdjacentElement('beforebegin', this.element);
            this.update();
            this.application.getControllerForElementAndIdentifier(target, "aiplugs-array-item").update();
            this.application.getControllerForElementAndIdentifier(this.element.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();
        }
    }
    down() {
        if (!this.downDisabled) {
            const target = this.element.nextElementSibling;
            target.insertAdjacentElement('afterend', this.element);
            this.update();
            this.application.getControllerForElementAndIdentifier(target, "aiplugs-array-item").update();
            this.application.getControllerForElementAndIdentifier(this.element.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();                
        }
    }
    remove() {
        const next = !this.downDisabled ? this.element.nextElementSibling : null;
        
        this.element.remove();

        if (next) {
            this.application.getControllerForElementAndIdentifier(next, "aiplugs-array-item").update();
            this.application.getControllerForElementAndIdentifier(next.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();
        }
    }
    update() {
        this.upTarget.disabled = this.upDisabled;
        this.downTarget.disabled = this.downDisabled;
        this.removeTarget.disabled = this.removeDisabled;
        this.labelTarget.innerText = this.label + "#" + this.index;
    }
    get upDisabled() {
        const el = this.element.previousElementSibling;
        return !el || el.constructor === HTMLTemplateElement;
    }
    get downDisabled() {
        const el = this.element.nextElementSibling;
        return !el || el.constructor === HTMLTemplateElement;
    }
    get removeDisabled() {
        return false;
    }
    get label() {
        return this.data.get("label") || "";
    }
    set label(value) {
        this.data.set("label", value);
        this.update();
    }
    get index() {
        return  (parseInt(this.data.get("index")) || 0) + 1;
    }
    set index(value) {
        this.data.set("index", value);
        this.update();
    }
});