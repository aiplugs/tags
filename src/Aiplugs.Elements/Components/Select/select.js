AiplugsElements.register("aiplugs-select", class extends Stimulus.Controller {
    static get targets() {
        return ["checkbox"];
    }
    update() {
        this.checkboxTargets.forEach(el => {
            el.parentElement.classList.toggle("aiplugs-select__checkbox--checked", el.checked);
        });
    }
});