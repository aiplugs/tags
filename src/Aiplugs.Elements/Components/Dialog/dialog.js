// document.addEventListener("DOMContentLoaded", function() {
//     for (let template of document.querySelectorAll('.aiplugs-dialog-template')) {
//         const open = template.getAttribute('data-open');
//         if (open) {
//             for (let btn of document.querySelectorAll(open)) {
//                 btn.addEventListener('click', openDialog.bind(null, template));
//             }
//         }
//     }
//     function openDialog(tempalte) {
//         const node = tempalte.content.cloneNode(true);
//         document.body.appendChild(node);
//     }
//     function closeDialog(id) {

//     }
// });
AiplugsElements.register('aiplugs-dialog-template', class extends Stimulus.Controller {
    initialize() {
        const query = this.opener;
        if (query) {
            this.registerAll(query);
        }
    }
    registerAll(query) {
        for (let el of document.querySelectorAll(query)) {
            this.register(el);
        }
    }
    register(el) {
        el.addEventListener('click', () => {
            this.open();
        });
    }
    open() {
        const tempalte = this.element;
        const node = tempalte.content.cloneNode(true);
        document.body.appendChild(node);
    }
    get opener() {
        return this.data.get('open');
    }
})
AiplugsElements.register('aiplugs-dialog', class extends Stimulus.Controller {
    initialize() {
        console.log('dialog');
    }
    close() {
        this.element.remove();
    }
})