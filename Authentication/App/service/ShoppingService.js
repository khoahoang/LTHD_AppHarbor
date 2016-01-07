'use strict';
mobileStoreApp.factory('shoppingService', ['localStorageService', function (localStorageService) {

    var shopping = {};
    var _count = function () {
        var list = localStorageService.get('dataShopping');
        var sum = 0;
        for (var i = 0; i < list.length; i++) {
            sum = sum + list[i].Quantity;
        }

        return sum;
    }

    var _get = function () {
        var list = localStorageService.get('dataShopping');
        return list;
    }

    var _set = function(value){
        localStorageService.set('dataShopping', value);
    }

    var _getAll = function () {
        var list = localStorageService.get('dataShopping');
        var all = 0;
        for (var index = 0; index < list.length; index++) {
            all = list[index].PriceDouble * list[index].Quantity + all;
        }

        return all;
    }

    var _tang = function (id) {
        var list = localStorageService.get('dataShopping');
        for (var index = 0; index < list.length; index++) {
            if (list[index].ID == id) {
                list[index].Quantity++;
                list[index].Total = list[index].PriceDouble * list[index].Quantity;
                break;
            }
        }

        return list;
    }

    var _giam = function (id) {
        var list = localStorageService.get('dataShopping');
        for (var index = 0; index < list.length; index++) {
            if (list[index].ID == id) {
                if (list[index].Quantity > 1) {
                    list[index].Quantity--;
                    list[index].Total = list[index].PriceDouble * list[index].Quantity;
                    break;
                }
                else {
                    list.splice(index, 1);
                    break;
                }
            }
        }

        return list;
    }

    var _them = function (pro) {
        var list = localStorageService.get('dataShopping');
        var proID = pro.product.PRODUCT_ID;
        var modelProduct = pro.product.MODEL;
        var priceDouble = pro.product.PRICE;
        var quantity = 1;
        var total = pro.product.PRICE;

        var flag = false;
        for (var index = 0; index < list.length; index++) {
            if (list[index].ID == proID) {
                list[index].Quantity++;
                list[index].Total = list[index].PriceDouble * list[index].Quantity;
                flag = true;
                break;
            }
        }

        if (flag == false) {
            var item = { "ID": proID, "ModelProduct": modelProduct, "PriceDouble": priceDouble, "Quantity": quantity, "Total": total };
            list.push(item);
        }

        return list;
    }

    var _clear = function () {
        var items = [];
        localStorageService.set('dataShopping', items);

        return items;
    }

    shopping.get = _get;
    shopping.set = _set;
    shopping.getAll = _getAll;
    shopping.tang = _tang;
    shopping.giam = _giam;
    shopping.them = _them;
    shopping.count = _count;
    shopping.clear = _clear;

    return shopping;
}]);