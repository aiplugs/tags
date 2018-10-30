AiplugsElements.register("aiplugs-tinymce", class extends Stimulus.Controller {
  static get targets() {
    return ["textarea", "insertImage", "insertVideo"];
  }
  initialize() {
    const self = this;
    const defaultOptions = {
      selector: '#' + this.textareaTarget.id,
      plugins: ['paste', 'table', 'lists', 'code', 'link', 'save', 'template', 'image', 'media', 'anchor'],
      menubar: 'edit format insert',
      toolbar: [
        'undo redo | formatselect | removeformat bold italic underline | alignleft aligncenter alignright | outdent indent | bullist numlist | blockquote table fileimage filevideo insert | code',
      ],
      templates: [],
      paste_as_text: true,
      resize: false,
      height: document.body.scrollHeight - this.element.offsetTop - 110,
      save_onsavecallback: function () { },
      setup: function onSetup(editor) {
        editor.addButton('fileimage', {
          icon: 'image',
          tooltip: 'Insert image',
          onclick: function () {
            self.insertImageTarget.click();
          }
        });
        editor.addButton('filevideo', {
          icon: 'media',
          tooltip: 'Insert video',
          onclick: function () {
            self.insertVideoTarget.click();
          }
        });
      },
      init_instance_callback: function (editor) {
        editor.on('Change', function () {
          self.textareaTarget.value = editor.getContent();
        });
        self.disposable(() => {
          editor.remove();
        })
      }
    };
    Promise.all([
      this.getText(this.valueFrom),
      this.getText(this.settingsFrom),
    ]).then(values => {
      const value = values[0] || this.textareaTarget.value;
      const settings = JSON.parse(values[1] || '{}');
      const options = Object.assign(defaultOptions, settings);
      this.textareaTarget.value = value;
      tinymce.init(options);
    });
  }
  close() {
    tinymce.activeEditor.remove();
  }
  getText(url) {
    if (!url)
        return new Promise(resolve => { resolve(); });
    
    if (url.startsWith('#')) {
        const el = document.querySelector(url);
        return new Promise(resolve => { resolve(el.value || el.innerText); });
    }

    return fetch(url, {mode:'cors',credentials:'include'}).then(res => res.text());
  }
  get valueFrom() {
    return this.data.get('value-from') || '';
  }
  get settingsFrom() {
      return this.data.get('settings-from') || '';
  }
});

AiplugsElements.registerIcProxy('POST', /\/\/aiplugs-tinymce\/active\/images/, function (_, body) {
  const data = AiplugsElements.parseFormUrlEncoded(body);
  const src = data['src'];
  const alt = data['alt'];
  const title = data['title'];
  const html = `<img src="${src}" alt="${alt} title="${title}"/>`;
  tinymce.activeEditor.insertContent(html);
  return '\n';
})
AiplugsElements.registerIcProxy('POST', /\/\/aiplugs-tinymce\/active\/videos/, function (_, body) {
  const data = AiplugsElements.parseFormUrlEncoded(body);
  const src = data['src'];
  const alt = data['alt'];
  const title = data['title'];
  const html = `<video src="${src}" alt="${alt} title="${title}"></video>`;
  tinymce.activeEditor.insertContent(html);
  return '\n';
})