document.addEventListener("DOMContentLoaded", function() {
    // const app = Stimulus.Application.start();
    // const $$ = (el, identifier) => app.getControllerForElementAndIdentifier(el, identifier);
    // const $$closest = (el, identifier) => $$(el.closest(`[data-controller="${identifier}"]`),identifier);
    // const $$child = (el, identifier) => $$(el.querySelector(`[data-controller="${identifier}"]`),identifier);

    // app.register("aiplugs-list", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["item"];
    //     }
    //     initialize() {
    //     }
    //     select(e) {
    //         this.itemTargets.forEach(el => {
    //             $$closest(el, "aiplugs-list-item").unselect();
    //         })
    //         $$closest(e.target, "aiplugs-list-item").select();
    //     }
    // });
    // app.register("aiplugs-list-item", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["checkbox"];
    //     }
    //     initialize() {
    //         this.update();
    //     }
    //     update() {
    //         this.element.classList.toggle("aiplugs-list__item--checked", this.checked);
    //         this.element.classList.toggle("aiplugs-list__item--selected", this.selected);
    //     }
    //     select() {
    //         this.selected = true;
    //     }
    //     unselect() {
    //         this.selected = false;
    //     }
    //     get checked() {
    //         return this.checkboxTarget.checked;
    //     }
    //     set checked(value) {
    //         this.checkboxTarget.checked = value;
    //         this.update();
    //     }
    //     get selected() {
    //         return this.data.get("selected") === "true";
    //     }
    //     set selected(value) {
    //         this.data.set("selected", value);
    //         this.update();
    //     }
    // });

    // app.register("aiplugs-input", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["input", "description","suggestion"];
    //     }
    //     initialize() {
    //         this.visibleDescription = false;
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-input__description--visible", this.visibleDescription);
    //     }
    //     search() {
    //         const url = this.ajaxUrl.replace("{0}", this.inputTarget.value);
    //         const opts = {
    //             method: "GET",
    //             headers: this.ajaxHeaders,
    //             mode: "cors",
    //             credentials: "include"
    //         }
    //         return fetch(url, opts).then(res => {
    //             if (res.ok)
    //                 return res.json();
    //         }).then(data => Array.isArray(data) ? data : []);
    //     }
    //     onInput() {
    //         if (this.debounceId)
    //             clearTimeout(this.debounceId);

    //         this.debounceId = setTimeout(() => {
    //             if(this.ajaxUrl) {
    //                 if (this.unique)
    //                     this.check();
    //                 else
    //                     this.suggestion();
    //             }
    //         }, 300);
    //     }
    //     onBlur() {
    //         if(this.ajaxUrl && !this.unique) {
    //             const value = this.inputTarget.value;
    //             const valueKey = this.ajaxValue;
    //             const rule = this.ignoreCase ? datum => (datum[valueKey]||"").toLowerCase() === value
    //                                          : datum => (datum[valueKey]||"") === value 
    //             this.search().then(data => {
    //                 const datum = data.find(rule) || data.find(datum => (datum[valueKey]||"").startsWith(value)) || data[0];
    //                 if (datum) {
    //                     this.inputTarget.value = datum[valueKey];
    //                 }
    //             })
    //         }
    //     }
    //     check() {
    //         const value = this.inputTarget.value;
    //         const valueKey = this.ajaxValue;
    //         const rule = this.ignoreCase ? datum => (datum[valueKey]||"").toLowerCase() === value
    //                                      : datum => (datum[valueKey]||"") === value 
    //         this.search().then(data => {
    //             const duplicated = !!data.find(rule);
    //             this.element.classList.toggle("aiplugs-input--duplicated", duplicated);
    //             this.element.classList.toggle("aiplugs-input--unique", !duplicated);
    //         })
    //     }
    //     suggestion() {
    //         const labelKey = this.ajaxLabel;
    //         const valueKey = this.ajaxValue;
    //         this.search().then(data => {
    //             this.suggestionTarget.innerHTML = "";
    //             data.forEach(datum => {
    //                 const option = document.createElement("option");
    //                 option.innerText = datum[labelKey];
    //                 option.label = datum[labelKey];
    //                 option.value = datum[valueKey];
    //                 this.suggestionTarget.appendChild(option);
    //             });
    //         });
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    //     get unique() {
    //         return this.data.get("unique") === "true";
    //     }
    //     get ignoreCase() {
    //         return this.data.get("ignore-case") === "true";
    //     }
    //     get ajaxUrl() {
    //         return this.data.get("ajax-url");
    //     }
    //     get ajaxHeaders() {
    //         const prefix = "data-aiplugs-input-ajax-headers-";
    //         return Array.from(this.element.attributes)
    //             .filter(attr => attr.name.startsWith(prefix))
    //             .reduce((headers, attr) => {
    //                 headers[attr.name.replace(prefix,"")] = attr.value;
    //             }, {});
    //     }
    //     get ajaxLabel() {
    //         return this.data.get("ajax-label") || "label";
    //     }
    //     get ajaxValue() {
    //         return this.data.get("ajax-value") || "value";
    //     }
    // });

    // app.register("aiplugs-checkbox", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["checkbox", "description"];
    //     }
    //     initialize() {
    //         this.visibleDescription = false;
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-input__description--visible", this.visibleDescription);
    //         this.element.classList.toggle("aiplugs-checkbox--checked", this.checked);
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     get checked() {
    //         return this.checkboxTarget.checked;
    //     }
    //     set checked(value) {
    //         this.checkboxTarget.checked = value;
    //         this.update();
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    // });
    // app.register("aiplugs-select", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["checkbox", "description"];
    //     }
    //     initialize() {
    //         this.visibleDescription = false;
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-select__description--visible", this.visibleDescription);
    //         this.checkboxTargets.forEach(el => {
    //             el.parentElement.classList.toggle("aiplugs-select__checkbox--checked", el.checked);
    //         });
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    // });
    // app.register("aiplugs-textarea", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["textarea", "description"];
    //     }
    //     initialize() {
    //         this.visibleDescription = false;
    //     }
    //     connect() {
    //         const input = this.textareaTarget;
    //         const style = getComputedStyle(input);
    //         this._diffWidth = parseInt(style.paddingLeft) + parseInt(style.paddingRight);
    //         this._diffHeight = -(parseInt(style.paddingBottom) + parseInt(style.paddingTop));
    //         const next = window.requestIdleCallback || (callback => { setTimeout(callback, 100); });
    //         next(() => {
    //           this.updateHeight();
    //         })
    //     }
    //     updateHeight() {
    //         const container = this.element;
    //         const input = this.textareaTarget;
    //         const len = (input.value || '').length;
    //         const style = getComputedStyle(input);
    //         const scrollTop = container.scrollTop;
    
    //         if (this._len >= len) {
    //           input.style.height = 'auto';
    //         }
    
    //         if (input.scrollHeight > input.clientHeight) {
    //           input.style.height = input.scrollHeight + this._diffHeight + 'px';
    //         }
    
    //         this._len = len;
    //         container.scrollTop = scrollTop;
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-textarea__description--visible", this.visibleDescription);
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    // });
    // app.register("aiplugs-code", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["view","input", "description"];
    //     }
    //     initialize() {
    //         this.visibleDescription = false;
    //     }
    //     edit() {
    //         const target = this.container;
    //         const template = this.element.querySelector("template");
    //         const id = this.editorId || (this.editorId = "editor-" + (this.inputTarget.id || (~~(Math.random() * Math.pow(2, 16))).toString(16)));
    //         const blade = target.querySelector(`#${id}`);

    //         if (!blade && template) {
    //             const content = template.content.cloneNode(true);
    //             content.firstElementChild.setAttribute('id', id);
    //             target.appendChild(content);

    //             this.trySetup(id);
    //         }
    //     }
    //     trySetup(id, timeout) {
    //         setTimeout(() => {
    //             this.setup(id);
    //         }, timeout || 0);
    //     }
    //     setup(id) {
    //         const blade = this.container.querySelector(`#${id}`);
    //         const close = blade.querySelector('.aiplugs-code__close');
    //         const cancel = blade.querySelector('.aiplugs-code__cancel');
    //         let editor = blade.querySelector('.aiplugs-code__editor');
    //         if (editor) {
    //             if (editor.classList.contains("aiplugs-tinymce")) {
    //                 editor = $$(editor, "aiplugs-tinymce");
    //             }
    //             else if (editor.classList.contains("aiplugs-monaco")) {
    //                 editor = $$(editor, "aiplugs-monaco");
    //             }
    //         }
    //         if (editor == null) {
    //             this.trySetup(id, 10);
    //             return;
    //         }
            
    //         editor.value = this.value;
            
    //         if (close) {
    //             close.addEventListener('click', () => {
    //                 if (editor) {
    //                     editor.value.then(value => {
    //                         this.value = value;
    //                         editor.close();
    //                     });
    //                 }
    //                 blade.remove();
    //             })
    //         }

    //         if (cancel) {
    //             cancel.addEventListener('click', () => {
    //                 if (editor) {
    //                     editor.close();
    //                 }
    //                 blade.remove();
    //             })
    //         }
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-code__description--visible", this.visibleDescription);
    //         this.element.classList.toggle("aiplugs-code--visible-all", this.visibleView);
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     toggleView() {
    //         this.visibleView = !this.visibleView;
    //     }
    //     get value() {
    //         return this.inputTarget.value;
    //     }
    //     set value(value) {
    //         this.inputTarget.value = value;
    //         this.viewTarget.innerText = value;
    //         hljs.highlightBlock(this.viewTarget);
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    //     get visibleView() {
    //         return this.data.get("visible-view") === "true";
    //     }
    //     set visibleView(value) {
    //         this.data.set("visible-view", value);
    //         this.update();
    //     }
    //     get container() {
    //         return document.querySelector(this.data.get("container")) || document.body;
    //     }
    // });
    // app.register("aiplugs-tinymce", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["textarea"];
    //     }
    //     initialize() {
    //         const opts = Object.assign({
    //             selector: '#' + this.textareaTarget.id,
    //             plugins: ['paste', 'table', 'lists', 'code', 'link', 'save', 'template', 'image', 'media', 'anchor'],
    //             menubar: 'edit format insert',
    //             toolbar: [
    //               'undo redo | formatselect | removeformat bold italic underline | alignleft aligncenter alignright | outdent indent | bullist numlist | blockquote table fileimage filemedia insert | code',
    //             ],
    //             templates: [],
    //             paste_as_text: true,
    //             resize: false,
    //             height: document.body.scrollHeight - this.element.offsetTop - 110,
    //             save_onsavecallback: function(){},
    //             setup: function(){},
    //             init_instance_callback: function(){}
    //         }, {})
    //         tinymce.init(opts);
    //     }
    //     close() {
    //         tinymce.activeEditor.remove();
    //     }
    //     get value() {
    //         return new Promise(resolve => {
    //             resolve(tinymce.activeEditor.getContent());
    //         });
    //     }
    //     set value(value) {
    //         tinymce.activeEditor.setContent(value);
    //     }
    // });
    // app.register("aiplugs-monaco", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["progress"];
    //     }
    //     initialize() {
    //         this.init = new Promise((resolve, reject) => {
    //             require(['vs/editor/editor.main'], () => {
    //                 const created = monaco.editor.onDidCreateEditor(() => {
    //                     this.progressTarget.remove();
    //                     created.dispose();
    //                 });
    //                 const editor = monaco.editor.create(this.element, { language: 'html' });
    //                 window.addEventListener('resize', () => {
    //                     editor.layout();
    //                 });
    //                 resolve(editor);
    //             });
    //         });
            
    //     }
    //     close() {
    //         this.init.then(editor => {
    //             editor.dispose();
    //         });
    //     }
    //     get value() {
    //         return this.init.then(editor => editor.getValue());
    //     }
    //     set value(value) {
    //         this.init.then(editor => {
    //             editor.setValue(value);
    //         });
    //     }
    // });
    // app.register("aiplugs-dictionary", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["items", "description", "template", "message", "item"];
    //     }
    //     initialize() {
    //         this.element.closest("form").addEventListener("submit", e => {
    //             this.validate();
    //             if (!this.message) {
    //                 e.preventDefault();
    //                 return false;
    //             }
    //         });
    //     }
    //     add() {
    //         const content = this.templateTarget.content.cloneNode(true);
    //         this.itemsTarget.appendChild(content);
    //         setTimeout(() => {
    //             this.items.pop().itemKeyTarget.focus();
    //         }, 0);
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-dictionary__description--visible", this.visibleDescription);
    //         this.items.forEach(item => {
    //             item.update();
    //         });
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     validate() {
    //         const messages = [];
    //         const mk = this.regexKey;
    //         const mv = this.regexValue;
    //         const rk = this.regexKeyPattern;
    //         const rv = this.regexValuePattern;
    //         const items = this.items;

    //         items.forEach(item => {
    //             const mk = !rk.exec(item.key) ? this.regexKey : null;
    //             const mv = !rv.exec(item.value) ? this.regexValue : null;
    //             item.validKey = !mk;
    //             item.validValue = !mv;
    //             messages.push(mk);
    //             messages.push(mv);
    //         });
            
    //         for (let i = 0; i < items.length; i++) {
    //             for (let j = 0; j < i; j++) {
    //                 if (items[i].key === items[j].key) {
    //                     items[j].validKey = false;
    //                     items[i].validKey = false;
    //                     messages.push(this.duplicateKey);
    //                 }
    //             }
    //         }

    //         this.message = messages.filter((_, i) => _ != null && messages.indexOf(_) === i).join("<br>");
    //     }
    //     get items() {
    //         return this.itemTargets.map(item => $$(item, "aiplugs-dictionary-item"));
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    //     set message(value) {
    //         this.messageTarget.innerHTML = value;
    //     }
    //     get regexKey() {
    //         return this.data.get("regex-key");
    //     }
    //     get regexKeyPattern() {
    //         return new RegExp(this.data.get("regex-key-pattern") || ".*");
    //     }
    //     get regexValue() {
    //         return this.data.get("regex-value");
    //     }
    //     get regexValuePattern() {
    //         return new RegExp(this.data.get("regex-value-pattern") || ".*");
    //     }
    //     get duplicateKey() {
    //         return this.data.get("duplicate-key") || "Duplicate keys detected.";
    //     }
    // });
    // app.register("aiplugs-dictionary-item", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["itemKey", "itemValue"];
    //     }
    //     initialize() {
    //     }
    //     update() {
    //         const name = this.name;
    //         if (name != null) {
    //             this.itemValueTarget.name = name;
    //         }
    //     }
    //     remove() {
    //         const dict = $$closest(this.element, "aiplugs-dictionary");
    //         this.element.remove();
    //         dict.validate();
    //     }
    //     get name() {
    //         const n = this.data.get("name") || "";
    //         const k = this.itemKeyTarget.value;
    //         return k ? `${n}[${k}]` : null;
    //     }
    //     get key() {
    //         return this.itemKeyTarget.value;
    //     }
    //     get value() {
    //         return this.itemValueTarget.value;
    //     }
    //     set validKey(value) {
    //         this.itemKeyTarget.classList.toggle("aiplugs-dictionary__item--invalid", !value);
    //     }
    //     set validValue(value) {
    //         this.itemValueTarget.classList.toggle("aiplugs-dictionary__item--invalid", !value);
    //     }
    // });
    // app.register("aiplugs-tag", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["description","template","input","suggestion","item"];
    //     }
    //     // initialize() {
    //     //     this.element.closest("form").addEventListener("submit", e => {
    //     //         if (!this.validate(true)) {
    //     //             e.preventDefault();
    //     //             return false;
    //     //         }
    //     //     });
    //     // }
    //     update() {
    //         // this.element.classList.toggle("aiplugs-tag--invalid", !this.valid);
    //         this.element.classList.toggle("aiplugs-tag--focus-last", this.focusLast);
    //         this.descriptionTarget.classList.toggle("aiplugs-tag__description--visible", this.visibleDescription);
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     onKeydown(e) {
    //         if (this.debounceId)
    //             clearTimeout(this.debounceId);

    //         this.debounceId = setTimeout(() => {
    //             if (this.ajaxUrl)
    //                 this.suggestion();
    //         }, 100);

    //         if (e.keyCode === 13) {
    //             e.preventDefault();
    //             this.add();
    //         }
    //         else if (e.keyCode === 8) {
    //             this.tryRemoveLast();
    //         }
    //         else if (e.keyCode === 39 || e.keyCode == 27) {
    //             this.tryCancelRemoveLast();
    //         }
    //     }
    //     exists(value) {
    //         if (this.ignoreCase) 
    //             return !!this.items.find(item => item.value.toLowerCase() === value.toLowerCase());
            
    //         return !!this.items.find(item => item.value === value)
    //     }
    //     add() {
    //         if (this.inputTarget.value.length == 0)
    //             return; 

    //         const value = this.inputTarget.value;
    //         const option = this.ajaxUrl ? this.suggestionTarget.querySelector(`option[value="${value}"]`) : null;
    //         const label = option ? option.label : value;

    //         if (this.ajaxUrl && !option)
    //             return;
            
    //         if (!this.exists(value)) {
    //             const content = this.templateTarget.content.cloneNode(true);
    //             this.inputTarget.parentElement.insertBefore(content, this.inputTarget);
    //             setTimeout(() => {
    //                 const item = this.items.pop();
    //                 item.label = label;
    //                 item.value = value;
    //                 item.validate();
    //                 this.inputTarget.value = "";
    //             }, 0);
    //         }
    //     }
    //     tryRemoveLast() {
    //         if (this.inputTarget.value.length === 0 && this.items.length > 0) {
    //             if (this.focusLast) {
    //               this.items.pop().remove();
    //               this.focusLast = false;
    //             }
    //             else {
    //               this.focusLast = true;
    //             }
    //           }
    //     }
    //     tryCancelRemoveLast() {
    //         if (this.focusLast) {
    //             this.focusLast = false;
    //         }
    //     }
    //     search() {
    //         const url = this.ajaxUrl.replace("{0}", this.inputTarget.value);
    //         const opts = {
    //             method: "GET",
    //             headers: this.ajaxHeaders,
    //             mode: "cors",
    //             credentials: "include"
    //         }
    //         return fetch(url, opts).then(res => {
    //             if (res.ok)
    //                 return res.json();
    //         }).then(data => Array.isArray(data) ? data : []);
    //     }
    //     suggestion() {
    //         const labelKey = this.ajaxLabel;
    //         const valueKey = this.ajaxValue;
    //         this.search().then(data => {
    //             this.suggestionTarget.innerHTML = "";
    //             data.forEach(datum => {
    //                 const option = document.createElement("option");
    //                 option.label = datum[labelKey];
    //                 option.value = datum[valueKey];
    //                 this.suggestionTarget.appendChild(option);
    //             });
    //         });
    //     }
    //     get items() {
    //         return this.itemTargets.map(item => $$(item, "aiplugs-tag-item"));
    //     }
    //     get focusLast() {
    //         return this.data.get("focus-last") === "true";
    //     }
    //     set focusLast(value) {
    //         this.data.set("focus-last", value);
    //         this.update();
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    //     get ignoreCase() {
    //         return this.data.get("ignore-case") === "true";
    //     }
    //     get ajaxUrl() {
    //         return this.data.get("ajax-url");
    //     }
    //     get ajaxHeaders() {
    //         const prefix = "data-aiplugs-tag-ajax-headers-";
    //         return Array.from(this.element.attributes)
    //             .filter(attr => attr.name.startsWith(prefix))
    //             .reduce((headers, attr) => {
    //                 headers[attr.name.replace(prefix,"")] = attr.value;
    //             }, {});
    //     }
    //     get ajaxLabel() {
    //         return this.data.get("ajax-label") || "label";
    //     }
    //     get ajaxValue() {
    //         return this.data.get("ajax-value") || "value";
    //     }
    // });
    // app.register("aiplugs-tag-item", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["input", "label"];
    //     }
    //     initialize() {
    //         $.validator.unobtrusive.parseElement(this.inputTarget, false);
    //     }
    //     validate(form, name) {
    //         form = form || this.element.closest("form");
    //         name = name || this.name;
    //         $(form).validate().element(`[name="${name}"]`);
    //     }
    //     remove() {
    //         const form = this.element.closest("form");
    //         const name = this.name;
    //         this.element.remove();
    //         this.validate(form, name);
    //     }
    //     get name() {
    //         return this.inputTarget.name;
    //     }
    //     get value() {
    //         return this.inputTarget.value;
    //     }
    //     set value(value) {
    //         this.inputTarget.value = value;
    //     }
    //     get label() {
    //         return this.labelTarget.innerText;
    //     }
    //     set label(value) {
    //         this.labelTarget.innerText = value;
    //     }
    // });
    // app.register("aiplugs-array", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["description", "items", "add", "item"];
    //     }
    //     add() {
    //         const template = Array.from(this.itemsTarget.children).find(el => el.constructor === HTMLTemplateElement);
    //         if (template) {
    //             const content = template.content.cloneNode(true);
    //             this.itemsTarget.appendChild(content); 
    //             this.update();
                
    //         }
    //     }
    //     update() {
    //         this.descriptionTarget.classList.toggle("aiplugs-array__description--visible", this.visibleDescription);
    //         setTimeout(() => {
    //             this.items.forEach((item, index) => {
    //                 item.index = index;
    //             });
    //         }, 0)
            
    //     }
    //     toggleDescription() {
    //         this.visibleDescription = !this.visibleDescription;
    //     }
    //     get items () {
    //         return this.itemTargets.map(el => this.application.getControllerForElementAndIdentifier(el, "aiplugs-array-item")).filter(_ => _);
    //     }
    //     get visibleDescription() {
    //         return this.data.get("visible-description") === "true";
    //     }
    //     set visibleDescription(value) {
    //         this.data.set("visible-description", value);
    //         this.update();
    //     }
    // });
    // app.register("aiplugs-array-item", class extends Stimulus.Controller {
    //     static get targets() {
    //         return ["label", "up", "down", "remove", "label"];
    //     }
    //     initialize() {
    //         this.update();
    //     }
    //     up() {
    //         if (!this.upDisabled) {
    //             const target = this.element.previousElementSibling;
    //             target.insertAdjacentElement('beforebegin', this.element);
    //             this.update();
    //             this.application.getControllerForElementAndIdentifier(target, "aiplugs-array-item").update();
    //             this.application.getControllerForElementAndIdentifier(this.element.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();
    //         }
    //     }
    //     down() {
    //         if (!this.downDisabled) {
    //             const target = this.element.nextElementSibling;
    //             target.insertAdjacentElement('afterend', this.element);
    //             this.update();
    //             this.application.getControllerForElementAndIdentifier(target, "aiplugs-array-item").update();
    //             this.application.getControllerForElementAndIdentifier(this.element.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();                
    //         }
    //     }
    //     remove() {
    //         const next = !this.downDisabled ? this.element.nextElementSibling : null;
            
    //         this.element.remove();

    //         if (next) {
    //             this.application.getControllerForElementAndIdentifier(next, "aiplugs-array-item").update();
    //             this.application.getControllerForElementAndIdentifier(next.closest("[data-controller='aiplugs-array']"), "aiplugs-array").update();
    //         }
    //     }
    //     update() {
    //         this.upTarget.disabled = this.upDisabled;
    //         this.downTarget.disabled = this.downDisabled;
    //         this.removeTarget.disabled = this.removeDisabled;
    //         this.labelTarget.innerText = this.label + "#" + this.index;
    //     }
    //     get upDisabled() {
    //         const el = this.element.previousElementSibling;
    //         return !el || el.constructor === HTMLTemplateElement;
    //     }
    //     get downDisabled() {
    //         const el = this.element.nextElementSibling;
    //         return !el || el.constructor === HTMLTemplateElement;
    //     }
    //     get removeDisabled() {
    //         return false;
    //     }
    //     get label() {
    //         return this.data.get("label") || "";
    //     }
    //     set label(value) {
    //         this.data.set("label", value);
    //         this.update();
    //     }
    //     get index() {
    //         return  (parseInt(this.data.get("index")) || 0) + 1;
    //     }
    //     set index(value) {
    //         this.data.set("index", value);
    //         this.update();
    //     }
    // });
    
});