AiplugsElements.register("aiplugs-checkbox", class extends Stimulus.Controller {
  static get targets() {
    return ["checkbox"];
  }
  initialize() {
    this.update();
  }
  update() {
    this.element.classList.toggle("aiplugs-checkbox--checked", this.checked);
  }
  get checked() {
    return this.checkboxTarget.checked;
  }
  set checked(value) {
    this.checkboxTarget.checked = value;
    this.update();
  }
});