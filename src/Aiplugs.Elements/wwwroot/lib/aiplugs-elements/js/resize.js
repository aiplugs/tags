document.addEventListener("DOMContentLoaded", function() {
    const FOLD = 'fold';
    const EXPLICITLY = 'explicitly';
    const THRESHOLD = 800;
    const width = () => window.outerWidth;

    const nav = document.querySelector('.aiplugs-nav');
    const btn = document.querySelector('.fold-action');

    function resize(size) {
        if (nav.classList.contains(EXPLICITLY) === false) {   
            if (size < THRESHOLD) {
                nav.classList.add(FOLD);
            } else {
                nav.classList.remove(FOLD);        
            }
        }
    }

    btn.addEventListener('click', () => {
        nav.classList.toggle(FOLD);
        nav.classList.toggle(EXPLICITLY);
    })

    window.addEventListener('resize', () => {
        resize(width());
    })

    resize(width());
});