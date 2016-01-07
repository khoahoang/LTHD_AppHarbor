'use strict';
mobileStoreApp.controller('ShoppingCartController', ['$scope', '$http', 'shoppingService', 'authService', function ($scope, $http, shoppingService, authService) {
    var list = shoppingService.get();

    $scope.listProduct = list;

    $scope.All = shoppingService.getAll();

    $scope.TangSoLuong = function (id) {
        var list = shoppingService.tang(id);
        shoppingService.set(list);

        $scope.listProduct = list;

        $scope.All = shoppingService.getAll();
    };

    $scope.GiamSoLuong = function (id) {
        var list = shoppingService.giam(id);
        shoppingService.set(list);

        $scope.listProduct = list;

        $scope.All = shoppingService.getAll();

        $scope.count = shoppingService.count();
    };

    $scope.count = shoppingService.count();

    $scope.ThanhToan = function () {
        var username = authService.authentication.userName;
        var name = $scope.name;
        var addr = $scope.addr;
        var phone = $scope.phone;
        if (name.length > 0 && addr.length > 0 && phone.length > 0) {
            var data = {};
            var user_info = { 'username': username, 'name': name, 'address': addr, 'phone': phone };
            data.user_info = user_info;
            var list = shoppingService.get();
            var order = [];
            for (var i = 0; i < list.length; i++) {
                var order_info = { 'product_id': list[i].ID, 'quantity': list[i].Quantity, 'unit_price': list[i].PriceDouble };
                order.push(order_info);
            }
            data.order_info = order;

            $http.post('http://localhost:41127/api/orders/submit', data).then(function (response) {
                $scope.listProduct = shoppingService.clear();
                $scope.All = 0;
                $scope.count = 0;
                $scope.name = "";
                $scope.addr = "";
                $scope.phone = "";
                window.alert('Cảm ơn đã mua hàng');
            })
        }
    }


    // $scope.listProduct = [
    //  {
    //    "ModelProduct" : "Alfreds Futterkiste",
    //    "Price" : "Berlin",
    //    "Number" : "Germany"
    //  },
    //  {
    //    "ModelProduct" : "Berglunds snabbköp",
    //    "Price" : "Luleå",
    //    "Number" : "Sweden"
    //  },
    //  {
    //    "ModelProduct" : "Centro comercial Moctezuma",
    //    "Price" : "México D.F.",
    //    "Number" : "Mexico"
    //  }]


    $scope.authentication = authService.authentication;
}]);