AiplugsElements.register('aiplugs-dialog', class extends Stimulus.Controller {
    initialize() {
        this.initialized();
    }
    close() {
        this.element.remove();
    }
})