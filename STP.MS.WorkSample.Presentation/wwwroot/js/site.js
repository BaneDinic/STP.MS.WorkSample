// Write your Javascript code.
var webapiUrl = 'http://localhost:56062/';

window.onerror = function (msg) {

    alert(msg);
};

$(document).ajaxError(function () {

    alert('An AJAX error has occured. Please contact the lousy developer!');
});


function parseQueryString() {
    var parsedParameters = {},
        uriParameters = location.search.substr(1).split('&');

    for (var i = 0; i < uriParameters.length; i++) {
        var parameter = uriParameters[i].split('=');
        parsedParameters[parameter[0]] = decodeURIComponent(parameter[1]);
    }

    return parsedParameters;
}


ko.extenders.numeric = function (target, precision) {

    var result = ko.pureComputed({
        read: target,
        write: function (newValue) {
            var current = target(),
                roundingMultiplier = Math.pow(10, precision),
                newValueAsNum = isNaN(newValue) ? 0 : +newValue,
                valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

            if (valueToWrite !== current) {
                target(valueToWrite);
            } else {

                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    }).extend({ notify: 'always' });

    result(target());

    return result;
};


ko.extenders.requiredText = function (target, options) {

    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    function validate(newValue) {

        target.hasError(newValue ? false : true);
        target.validationMessage(newValue ? "" : options.overrideMessage || "This field is required");
    }

    if (!options.skipOnInit) {
        validate(target());
    }
    target.subscribe(validate);
    return target;
};


ko.extenders.requiredDate = function (target, options) {

    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    function validate(newValue) {

        target.hasError(isNaN(newValue));
        target.validationMessage(!isNaN(newValue) ? "" : options.overrideMessage || "This field is required");
    }

    if (!options.skipOnInit) {
        
        validate(target());
    }
    target.subscribe(validate);
    return target;
};



ko.bindingHandlers.datePicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        ko.utils.registerEventHandler(element, "change", function () {
            var value = valueAccessor();
            value(new Date(element.value));
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        if (value()) {
            var timeZoneOffset = value().getTimezoneOffset();
            var dateCleared = new Date(value().getTime() - timeZoneOffset * 60000);
            element.valueAsDate = dateCleared;
        }
    }
};