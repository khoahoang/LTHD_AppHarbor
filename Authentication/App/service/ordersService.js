'use strict';
mobileStoreApp.factory('ordersService', ['$http', 'localStorageService', function ($http, localStorageService) {
 
    var serviceBase = 'http://mobilestore.apphb.com/';
    var ordersServiceFactory = {};
 
    var _getOrders = function () {
        var list = localStorageService.get('dataShopping');

        return $http.post(serviceBase + 'api/orders/submit').then(function (results) {
            return results;
        });
    };
 
    ordersServiceFactory.getOrders = _getOrders;
 
    return ordersServiceFactory;
 
}]);