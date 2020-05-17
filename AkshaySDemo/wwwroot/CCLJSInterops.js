window.cclHelperFunctions = {
    getElementProps: function (elem, props) {
        var obj = {};
        for (var i = 0; i < props.length; i++) {
            obj[props[i]] = elem[props[i]];
        }
        return JSON.stringify(obj);
    },
    getViewportDimensions: function () {
        return JSON.stringify({ clientWidth: document.documentElement.clientWidth, clientHeight: document.documentElement.clientHeight });
    }
};
