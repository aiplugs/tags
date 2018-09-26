AiplugsElements.register("aiplugs-tag", class extends Stimulus.Controller {
    static get targets() {
        return ["template","input","suggestion","item"];
    }
    // initialize() {
    //     this.element.closest("form").addEventListener("submit", e => {
    //         if (!this.validate(true)) {
    //             e.preventDefault();
    //             return false;
    //         }
    //     });
    // }
    update() {
        // this.element.classList.toggle("aiplugs-tag--invalid", !this.valid);
        this.element.classList.toggle("aiplugs-tag--focus-last", this.focusLast);
    }
    onKeydown(e) {
        if (this.debounceId)
            clearTimeout(this.debounceId);

        this.debounceId = setTimeout(() => {
            if (this.ajaxUrl)
                this.suggestion();
        }, 100);

        if (e.keyCode === 13) { // ENTER
            e.preventDefault();
            this.add();
        }
        else if (e.keyCode === 8) { // BACKSPACE
            this.tryRemoveLast();
        }
        else if (e.keyCode === 39 || e.keyCode == 27) { // RIGHT || ESCAPE
            this.tryCancelRemoveLast();
        }
    }
    exists(value) {
        if (this.ignoreCase) 
            return !!this.items.find(item => item.value.toLowerCase() === value.toLowerCase());
        
        return !!this.items.find(item => item.value === value)
    }
    add() {
        if (this.inputTarget.value.length == 0)
            return; 

        const value = this.inputTarget.value;
        const option = this.ajaxUrl ? this.suggestionTarget.querySelector(`option[value="${value}"]`) : null;
        const label = option ? option.label : value;

        if (this.ajaxUrl && !option)
            return;
        
        if (!this.exists(value)) {
            const content = this.templateTarget.content.cloneNode(true);
            this.inputTarget.parentElement.insertBefore(content, this.inputTarget);
            setTimeout(() => {
                const item = this.items.pop();
                item.label = label;
                item.value = value;
                item.validate();
                this.inputTarget.value = "";
            }, 0);
        }
    }
    tryRemoveLast() {
        if (this.inputTarget.value.length === 0 && this.items.length > 0) {
            if (this.focusLast) {
              this.items.pop().remove();
              this.focusLast = false;
            }
            else {
              this.focusLast = true;
            }
          }
    }
    tryCancelRemoveLast() {
        if (this.focusLast) {
            this.focusLast = false;
        }
    }
    search() {
        const url = this.ajaxUrl.replace("{0}", this.inputTarget.value);
        const opts = {
            method: "GET",
            headers: this.ajaxHeaders,
            mode: "cors",
            credentials: "include"
        }
        return fetch(url, opts).then(res => {
            if (res.ok)
                return res.json();
        }).then(data => Array.isArray(data) ? data : []);
    }
    suggestion() {
        const labelKey = this.ajaxLabel;
        const valueKey = this.ajaxValue;
        this.search().then(data => {
            this.suggestionTarget.innerHTML = "";
            data.forEach(datum => {
                const option = document.createElement("option");
                option.label = datum[labelKey];
                option.value = datum[valueKey];
                this.suggestionTarget.appendChild(option);
            });
        });
    }
    get items() {
        return this.itemTargets.map(item => this.application.resolve(item, "aiplugs-tag-item"));
    }
    get focusLast() {
        return this.data.get("focus-last") === "true";
    }
    set focusLast(value) {
        this.data.set("focus-last", value);
        this.update();
    }
    get ignoreCase() {
        return this.data.get("ignore-case") === "true";
    }
    get ajaxUrl() {
        return this.data.get("ajax-url");
    }
    get ajaxHeaders() {
        const prefix = "data-aiplugs-tag-ajax-headers-";
        return Array.from(this.element.attributes)
            .filter(attr => attr.name.startsWith(prefix))
            .reduce((headers, attr) => {
                headers[attr.name.replace(prefix,"")] = attr.value;
            }, {});
    }
    get ajaxLabel() {
        return this.data.get("ajax-label") || "label";
    }
    get ajaxValue() {
        return this.data.get("ajax-value") || "value";
    }
});
AiplugsElements.register("aiplugs-tag-item", class extends Stimulus.Controller {
    static get targets() {
        return ["input", "label"];
    }
    initialize() {
        $.validator.unobtrusive.parseElement(this.inputTarget, false);
    }
    validate(form, name) {
        form = form || this.element.closest("form");
        name = name || this.name;
        $(form).validate().element(`[name="${name}"]`);
    }
    remove() {
        const form = this.element.closest("form");
        const name = this.name;
        this.element.remove();
        this.validate(form, name);
    }
    get name() {
        return this.inputTarget.name;
    }
    get value() {
        return this.inputTarget.value;
    }
    set value(value) {
        this.inputTarget.value = value;
    }
    get label() {
        return this.labelTarget.innerText;
    }
    set label(value) {
        this.labelTarget.innerText = value;
    }
});