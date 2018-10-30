AiplugsElements.register("aiplugs-list", class extends Stimulus.Controller {
  static get targets() {
    return ["item"];
  }
  select(e) {
    this.items.forEach(item => { item.unselect(); });
    this.application.closestRoot(e.target, "aiplugs-list-item").select();
  }
  dispatchUpdate() {
    this.throttle('change-selects', 100, () => {
      this.element.dispatchEvent(new CustomEvent('change'));
    })
  }
  get items() {
    return this.itemTargets.map(el => this.application.resolve(el, 'aiplugs-list-item'));
  }
  get selectedItem() {
    return this.items.filter(item => item.selected)[0];
  }
  get checkedItems() {
    return this.items.filter(item => item.checked);
  }
  get electedItems() {
    return this.items.filter(item => item.selected || item.checked);
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
    this.parent('aiplugs-list').dispatchUpdate();
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

