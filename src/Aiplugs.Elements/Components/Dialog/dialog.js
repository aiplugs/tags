document.addEventListener("DOMContentLoaded", function() {
    for (let template of document.querySelectorAll('.aiplugs-dialog-template')) {
        const open = template.getAttribute('data-open');
        if (open) {
            for (let btn of document.querySelectorAll(open)) {
                btn.addEventListener('click', openDialog.bind(null, template));
            }
        }
    }
    function openDialog(tempalte) {
        const node = tempalte.content.cloneNode(true);
        document.body.appendChild(node);
    }
    function closeDialog(id) {

    }
});