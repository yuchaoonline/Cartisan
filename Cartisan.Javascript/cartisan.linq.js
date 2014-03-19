(function () {
    function Enumerable(dataItems) {
        this.dataItems = dataItems;
    };

    Array.prototype.asEnumerable = function () {
        return new Enumerable(this);
    };

    Enumerable.prototype.toArray = function () {
        return this.dataItems;
    };

    Enumerable.prototype.forEach = function (eachFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            eachFunc(this.dataItems[i], i);
        }

        return this;
    };

    Enumerable.prototype.where = function (conditionFunc) {
        var arr = [];
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (conditionFunc(this.dataItems[i], i)) {
                arr[arr.length] = this.dataItems[i];
            }
        }
        return arr.asEnumerable();
    };

    Enumerable.prototype.contains = function (containsFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (containsFunc(this.dataItems[i])) {
                return true;
            }
        }
        return false;
    };

    Enumerable.prototype.select = function (selectFunc) {
        var arr = [];
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            arr[arr.length] = selectFunc(this.dataItems[i], i);
        }
        return arr.asEnumerable();
    };

    Enumerable.prototype.any = function (containsFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (containsFunc(this.dataItems[i])) {
                return true;
            }
        }
        return false;
    };

    Enumerable.prototype.all = function (containsFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (!containsFunc(this.dataItems[i])) {
                return false;
            }
        }
        return true;
    };
})();