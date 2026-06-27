// SiteManager - Scripts personalizados

document.addEventListener('DOMContentLoaded', function () {
    const deleteForms = document.querySelectorAll('form[action*="Delete"]');
    deleteForms.forEach(form => {
        form.addEventListener('submit', function (e) {
            if (!confirm('¿Está seguro de que desea eliminar este registro?')) {
                e.preventDefault();
            }
        });
    });
});