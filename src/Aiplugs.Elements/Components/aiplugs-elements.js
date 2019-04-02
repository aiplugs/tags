(function(global){
    if (typeof window.requestIdleCallback === 'function') {
        window.requestIdleCallback = function(callback) { return setTimeout(callback, 0); };
    }
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
    Stimulus.Application.prototype.find = function(query) {
        const el = document.querySelector(query);
        if (el) {
            for (let c of el.classList) {
                const ctl = this.resolve(el, c);
                if (ctl) return ctl;
            }
        }
        return null;
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
            requestIdleCallback(callback);
            this.__throttle[code] = setTimeout(() => {
                this.__throttle[code] = null;
            }, dt);
        }
    }
    Stimulus.Controller.prototype.initialized = function () {
        requestIdleCallback(() => {
            this.element.dispatchEvent(new CustomEvent('initialized'));
        });
    }
    Stimulus.Controller.prototype.disposable = function (callback) {
        const observer = new MutationObserver(records => {
            for (let record of records) {
                for (let node of record.removedNodes) {
                    const result = this.element.compareDocumentPosition(node);
                    if (result & this.element.DOCUMENT_POSITION_CONTAINS) {
                        callback();
                        observer.disconnect();
                        break;
                    }
                }
            }
        });
        observer.observe(document.body, { childList: true });
    }
    global.AiplugsElements =  Stimulus.Application.start();
}(window));

(function ($, aiplugs) {

    const registers = [];
    function register(method, url, handler) {
        registers.push({
            method,
            url,
            handler
        });
    }

    $.ajaxTransport('text', function (opts, settings) {
        let method = (settings.type || 'get').toLowerCase();
        const override = settings.headers['X-HTTP-Method-Override'];
        if (override) {
            method = override.toLowerCase();
        }
        for (let r of registers) {
            const m = r.url.exec(settings.url);
            if (method === r.method.toLowerCase() && m !== null) {
                return {
                    send: function (headers, completeCallback) {
                        completeCallback("200", "OK", { html: r.handler() });
                    },
                    abort: function () {
                    }
                }
            }
        }
    });

    function decode(str) {
        return decodeURIComponent((str||'').replace(/\+/g, '%20'));
    }

    function parse(str) {
        return (str||'').split('&')
                        .map(set => set.split('='))
                        .reduce((a, b) => { a[decode(b[0])] = decode(b[1]); return a}, {});
    }

    register('PUT', /\/\/#(.+?)\/(.+)/, function (m, body) {
        const id = m[1];
        const prop = m[2];
        const el = document.getElementById(id);

        el[prop] = parse(body)[prop];
        el.dispatchEvent(new CustomEvent('change'));

        return '\n';
    })

    register('GET', /\/\/null/, function () {
        return '\n';
    })

    aiplugs.registerIcProxy = register;
    aiplugs.parseFormUrlEncoded = parse;
}(jQuery, AiplugsElements));

(function(){
    Intercooler.ready(function () {
        const observer = new MutationObserver(records => {
            for (let record of records) {
                for (let node of record.addedNodes) {
                    if (node.nodeType === node.ELEMENT_NODE && !node.hasAttribute('ic-id')) {
                        Intercooler.processNodes(node);
                    }
                }
            }
        });
        observer.observe(document.body, { childList: true });
    })
    $(function () {
        $('form').each(function (i, form) {
            const validation = $(form).data('validator');
            if (validation) {
                validation.settings.ignore = validation.settings.ignore + ', .val-ignore';
            }
        })
    })
}())