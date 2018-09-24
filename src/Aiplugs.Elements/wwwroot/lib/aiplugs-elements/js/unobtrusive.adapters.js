(function() {
    function selectedOptions(elem) {
        return elem.constructor === HTMLSelectElement ? Array.from(elem.querySelectorAll("option")).filter(opt => opt.selected).length
             : elem.constructor === HTMLInputElement && (elem.type === "checkbox" || elem.type === "radio") ? Array.from((elem.closest("form")||document).querySelectorAll(`[name="${elem.name}"]`)).filter(opt => opt.checked).length
             : null;
    }
    jQuery.validator.addMethod("select-maxlength", function (value, elem, params) {
        if (params.length === 0)
            return true;

        const keys = Object.keys(params);
        const selecteds = selectedOptions(elem);

        if (selecteds != null && selecteds > params[0])
            return false;

        return true;
    }, '');
    jQuery.validator.addMethod("select-minlength", function (value, elem, params) {
        if (params.length === 0)
            return true;

        const keys = Object.keys(params);
        const selecteds = selectedOptions(elem);

        if (selecteds != null && selecteds < params[0])
            return false;

        return true;
    }, '');
    jQuery.validator.unobtrusive.adapters.add("select-maxlength", ["max"], function (options) {
        options.rules["select-maxlength"] = [options.params.max];
        options.messages['select-maxlength'] = options.message;
    });
    jQuery.validator.unobtrusive.adapters.add("select-minlength", ["min"], function (options) {
        options.rules["select-minlength"] = [options.params.min];
        options.messages['select-minlength'] = options.message;
    });
}())