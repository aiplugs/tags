AiplugsElements.register('aiplugs-selectable-button', class extends Stimulus.Controller {
    static get targets() {
        return ['select', 'button'];
    }
    initialize() {
        this.update(this.selected || this.first);
    }
    change() {
        this.update(this.selected);
    }
    update(value) {
        if (!this.selectTarget.name) {
            if (this.buttonTarget.constructor === HTMLAnchorElement) {
                this.buttonTarget.href = value;
            }
            else {
                this.buttonTarget.name = value;
            }
        }
    }
    get selected() {
        const option = this.selectTarget.querySelector('option:checked');
        return option ? option.value : '';
    }
    get first() {
        const option = this.selectTarget.querySelector('option:first-of-type').value;
        return option ? option.value : '';
    }
})