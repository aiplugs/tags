AiplugsElements.register("aiplugs-select", class extends Stimulus.Controller {
    static get targets() {
        return ["checkbox"];
    }
    initialize() {
        this.update();
    }
    update() {
        this.checkboxTargets.forEach(el => {
            el.parentElement.classList.toggle("aiplugs-select__checkbox--checked", el.checked);
        });
    }
    setNamePrefix(prefix) {
        for (let input of this.checkboxTargets) {
            input.name = prefix + '.' + (this.data.get('nameTemplate') || '') + (input.name.endsWith('[]') ? '[]' : '');
        }
    }
});