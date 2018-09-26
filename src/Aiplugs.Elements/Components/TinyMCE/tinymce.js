AiplugsElements.register("aiplugs-tinymce", class extends Stimulus.Controller {
    static get targets() {
        return ["textarea"];
    }
    initialize() {
        const opts = Object.assign({
            selector: '#' + this.textareaTarget.id,
            plugins: ['paste', 'table', 'lists', 'code', 'link', 'save', 'template', 'image', 'media', 'anchor'],
            menubar: 'edit format insert',
            toolbar: [
              'undo redo | formatselect | removeformat bold italic underline | alignleft aligncenter alignright | outdent indent | bullist numlist | blockquote table fileimage filemedia insert | code',
            ],
            templates: [],
            paste_as_text: true,
            resize: false,
            height: document.body.scrollHeight - this.element.offsetTop - 110,
            save_onsavecallback: function(){},
            setup: function(){},
            init_instance_callback: function(){}
        }, {})
        tinymce.init(opts);
    }
    close() {
        tinymce.activeEditor.remove();
    }
    get value() {
        return new Promise(resolve => {
            resolve(tinymce.activeEditor.getContent());
        });
    }
    set value(value) {
        tinymce.activeEditor.setContent(value);
    }
});