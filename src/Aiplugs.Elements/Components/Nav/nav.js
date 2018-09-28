AiplugsElements.register('aiplugs-nav', class extends Stimulus.Controller {
    initialize() {
        this.force = this.fold;
        window.addEventListener('resize', () => {
            this.resize();
        })
        this.update();
        this.resize();
    }
    toggle() {
        this.fold = !this.fold;
        this.force = !this.force;
        this.update();
    }
    resize() {
        if (!this.force) {   
            this.fold = window.innerWidth < this.threshold;
            this.update();
        }
    }
    update() {
        this.element.classList.toggle('--fold', this.fold);
        this.element.classList.toggle('--force', this.force);
    }
    get fold() {
        return sessionStorage.getItem('aiplugs-nav-fold') === 'true';
    }
    set fold(value) {
        sessionStorage.setItem('aiplugs-nav-fold', value);
    }
    get force() {
        return sessionStorage.getItem('aiplugs-nav-force') === 'true';
    }
    set force(value) {
        sessionStorage.setItem('aiplugs-nav-force', value);
    }
    get threshold() {
        return parseInt(this.data.get('threshold') || 800);
    }
})