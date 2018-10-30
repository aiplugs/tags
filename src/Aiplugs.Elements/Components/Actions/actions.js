AiplugsElements.register('aiplugs-actions', class extends Stimulus.Controller {
    initialize() {
        this.update();
    }
    update() {
        const items = this.items;
        this.element.querySelectorAll('[when="any"]').forEach(el => {
            el.disabled = items <= 0;
            el.classList.toggle('--disabled', items <= 0);
        });
        this.element.querySelectorAll('[when="one"]').forEach(el => {
            el.disabled = items != 1;
            el.classList.toggle('--disabled', items != 1);
        });
    }
    get items() {
        return this.data.get('items');
    }
    set items(value) {
        this.data.set('items', value);
        this.update();
    }
})