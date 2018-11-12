(function($jQval) {
    const adapters = $jQval.unobtrusive.adapters;
    function selectedOptions(elem) {
        return elem.constructor === HTMLSelectElement ? Array.from(elem.querySelectorAll("option")).filter(opt => opt.selected).length
             : elem.constructor === HTMLInputElement && (elem.type === "checkbox" || elem.type === "radio") ? Array.from((elem.closest("form")||document).querySelectorAll(`[name="${elem.name}"]`)).filter(opt => opt.checked).length
             : null;
    }
    $jQval.addMethod("maxcount", function (value, elem, params) {
        if (params.length === 0)
            return true;

        const keys = Object.keys(params);
        const selecteds = selectedOptions(elem);

        if (selecteds !== null && selecteds > params[0])
            return false;

        return true;
    });
    $jQval.addMethod("mincount", function (value, elem, params) {
        if (params.length === 0)
            return true;

        const keys = Object.keys(params);
        const selecteds = selectedOptions(elem);

        if (selecteds !== null && selecteds < params[0])
            return false;

        return true;
    });
    adapters.add("maxcount", ["max"], function (options) {
        options.rules["maxcount"] = [options.params.max];
        options.messages['maxcount'] = options.message;
    });
    adapters.add("mincount", ["min"], function (options) {
        options.rules["mincount"] = [options.params.min];
        options.messages['mincount'] = options.message;
    });
}(jQuery.validator))