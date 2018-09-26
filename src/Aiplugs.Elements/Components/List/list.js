AiplugsElements.register("aiplugs-list", class extends Stimulus.Controller {
  static get targets() {
    return ["item"];
  }
  select(e) {
    this.itemTargets.forEach(el => {
      this.application.closestRoot(el, 'aiplugs-list-item').unselect();
    })
    this.application.closestRoot(e.target, "aiplugs-list-item").select();
  }
});

AiplugsElements.register("aiplugs-list-item", class extends Stimulus.Controller {
  static get targets() {
    return ["checkbox"];
  }
  initialize() {
    this.update();
  }
  update() {
    this.element.classList.toggle("--checked", this.checked);
    this.element.classList.toggle("--selected", this.selected);
  }
  select() {
    this.selected = true;
  }
  unselect() {
    this.selected = false;
  }
  get checked() {
    return this.checkboxTarget.checked;
  }
  set checked(value) {
    this.checkboxTarget.checked = value;
    this.update();
  }
  get selected() {
    return this.data.get('selected') === 'true';
  }
  set selected(value) {
    this.data.set('selected', value);
    this.update();
  }
});

