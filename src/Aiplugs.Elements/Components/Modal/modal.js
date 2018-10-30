AiplugsElements.register('aiplugs-modal', class extends Stimulus.Controller {
    close() {
        this.element.remove();
    }
});