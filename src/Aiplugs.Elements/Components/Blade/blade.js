document.addEventListener("DOMContentLoaded", function() {
    for (let btn of document.querySelectorAll('.aiplugs-blade-expand')) {
        btn.addEventListener('click', function(e) {
            const blade = e.target.closest('.aiplugs-blade');
            blade.classList.toggle('expanded');
        });
    }
});
AiplugsElements.register('aiplugs-blade', class extends Stimulus.Controller {
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