AiplugsElements.register("aiplugs-info", class extends Stimulus.Controller {
    static get targets() {
        return ['detail'];
    }
    update(){
        this.descriptionTarget.classList.toggle("--visible", this.visible);
    }
    toggle() {
        this.visible = !this.visible;
    }
    get visible() {
        return this.data.get('visible') === 'true';
    }
    set visible(value) {
        this.data.set('visible', value);
        this.update();
    }
});

