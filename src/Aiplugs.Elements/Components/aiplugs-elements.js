(function(global){
    Stimulus.Application.prototype.resolve = function(element, identifier) {
        return this.getControllerForElementAndIdentifier(element, identifier);
    }
    Stimulus.Application.prototype.closestRoot = function(element, identifier) {
        const el = element.closest('[data-controller="'+identifier+'"]');
        return this.resolve(el, identifier);
    }
    Stimulus.Application.prototype.closestLeaf = function(element, identifier) {
        const el = element.querySelector('[data-controller="'+identifier+'"]');
        return this.resolve(el, identifier);
    }
    Stimulus.Controller.prototype.parent = function(identifier) {
        return this.application.closestRoot(this.element, identifier);
    }
    Stimulus.Controller.prototype.child = function(identifier) {
        return this.application.closestLeaf(this.element, identifier);
    }
    Stimulus.Controller.prototype.children = function(identifier) {
        return Array.from(this.element.querySelectorAll('[data-controller="'+identifier+'"]'))
                    .map(el => this.application.resolve(el, identifier));
    }
    Stimulus.Controller.prototype.debounce = function(code, dt, callback) {
        if (!this.__debounce) this.__debounce = {};
        if (this.__debounce[code]) clearTimeout(this.__debounce[code]);
        this.__debounce[code] = setTimeout(callback, dt);
    }
    Stimulus.Controller.prototype.throttle = function(code, dt, callback) {
        if (!this.__throttle) this.__throttle = {};
        if (!this.__throttle[code]) {
            callback();
            this.__throttle[code] = setTimeout(() => {
                this.__throttle[code] = null;
            }, dt);
        }
    }
    global.AiplugsElements =  Stimulus.Application.start();
}(window))