window.cclHelperFunctions = {
    getElementTopLeftCornerPageCoordsOld: function (el) {
        return JSON.stringify({ X: el.offsetLeft, Y: el.offsetTop });
    },
    getElementTopLeftCornerPageCoords: function (el, arr) {
        var obj = {};
        for (var i = 0; i < arr.length; i++) {
            obj[arr[i]] = el[arr[i]];
        }
        return JSON.stringify(obj);
    }
};
