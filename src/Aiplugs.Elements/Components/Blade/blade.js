AiplugsElements.register('aiplugs-blade', class extends Stimulus.Controller {
    initialize() {
        this.update();
    }
    close() {
        this.element.remove();
    }
    toggle() {
        this.expanded = !this.expanded;
    }
    update() {
        this.element.classList.toggle('--expanded', this.expanded);
    }
    get expanded() {
        return this.data.get('expanded') === 'true';
    }
    set expanded(value) {
        this.data.set('expanded', value);
        this.update();
    }
});