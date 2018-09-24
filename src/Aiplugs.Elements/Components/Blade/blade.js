document.addEventListener("DOMContentLoaded", function() {
    for (let btn of document.querySelectorAll('.aiplugs-blade-expand')) {
        btn.addEventListener('click', function(e) {
            const blade = e.target.closest('.aiplugs-blade');
            blade.classList.toggle('expanded');
        });
    }
});