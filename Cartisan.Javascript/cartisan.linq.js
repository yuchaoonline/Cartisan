(function () {
    function Enumerable(dataItems) {
        this.dataItems = dataItems;
    };

    Array.prototype.AsEnumerable = function () {
        return new Enumerable(this);
    };

    Enumerable.prototype.ToArray = function () {
        return this.dataItems;
    };

    Enumerable.prototype.ForEach = function (eachFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            eachFunc(this.dataItems[i], i);
        }

        return this;
    };

    Enumerable.prototype.Where = function (conditionFunc) {
        var arr = [];
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (conditionFunc(this.dataItems[i], i)) {
                arr[arr.length] = this.dataItems[i];
            }
        }
        return arr.AsEnumerable();
    };

    Enumerable.prototype.Contains = function (containsFunc) {
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            if (containsFunc(this.dataItems[i])) {
                return true;
            }
        }
        return false;
    };

    Enumerable.prototype.Select = function (selectFunc) {
        var arr = [];
        for (var i = 0, length = this.dataItems.length; i < length; i++) {
            arr[arr.length] = selectFunc(this.dataItems[i], i);
        }
        return arr.AsEnumerable();
    };

})();