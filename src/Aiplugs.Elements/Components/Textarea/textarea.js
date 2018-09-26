AiplugsElements.register("aiplugs-textarea", class extends Stimulus.Controller {
    static get targets() {
        return ["textarea"];
    }
    connect() {
        const input = this.textareaTarget;
        const style = getComputedStyle(input);
        this._diffWidth = parseInt(style.paddingLeft) + parseInt(style.paddingRight);
        this._diffHeight = -(parseInt(style.paddingBottom) + parseInt(style.paddingTop));
        const next = window.requestIdleCallback || (callback => { setTimeout(callback, 100); });
        next(() => {
          this.updateHeight();
        })
    }
    updateHeight() {
        const container = this.element;
        const input = this.textareaTarget;
        const len = (input.value || '').length;
        const style = getComputedStyle(input);
        const scrollTop = container.scrollTop;

        if (this._len >= len) {
          input.style.height = 'auto';
        }

        if (input.scrollHeight > input.clientHeight) {
          input.style.height = input.scrollHeight + this._diffHeight + 'px';
        }

        this._len = len;
        container.scrollTop = scrollTop;
    }
});