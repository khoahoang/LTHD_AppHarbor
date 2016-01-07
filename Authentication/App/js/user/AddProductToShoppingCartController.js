mobileStoreApp.controller('AddProductToShoppingCartController', function ($scope, authService, shoppingService, $routeParams, $http) {
    var list = shoppingService.get();

    $http.get("http://localhost:41127/api/product/getproduct?id=" + $routeParams.proId)
    .then(function (response) {
        list = shoppingService.them(response.data);

        shoppingService.set(list);

        $scope.listProduct = list;

        $scope.All = shoppingService.getAll();

        $scope.count = shoppingService.count();
    });

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

    // var count = 0;
    // for (var index = 0; index < list.length; index++){
    //     count=list[index].Quantity + count;
    //   }

    //   $rootScope.SoLuong = count;
    $scope.authentication = authService.authentication;
})

// mobileStoreApp.controller('AddProductToShoppingCartController', 
//   ['$scope', 'localStorageService', '$routeParams', '$http', AddProductToShoppingCartController]);

// function AddProductToShoppingCartController($scope, localStorageService, $routeParams, $http){
//   $scope.listProduct = localStorageService.get('dataShopping');
//   $http.get("http://localhost:41127/api/product/getproduct?id=" + $routeParams.proId)
//     .then(function(response) {
//       var pro = response.data;

//       var proID = pro.product.PRODUCT_ID;
//       var modelProduct = pro.product.MODEL;
//       var price = pro.Price;
//       var quantity = 1;

//       var flag = false;
//       for  (index = 0; index < $scope.listProduct.length; index++) {
//        if ($scope.listProduct[index].ID == proID){
//          $scope.listProduct[index].Quantity++;
//          flag = true;
//        }
//       }

//       if (flag == false){
//        var item = {"ID": proID, "ModelProduct": modelProduct, "Price": price, "Quantity": quantity};
//        list.push(item);
//       }

//       localStorageService.set('dataShopping', list);
//       $scope.listProduct = list;
//   });


//   $scope.TangSoLuong = function(id){
//     for  (index = 0; index < $scope.listProduct.length; index++) {
//      if ($scope.listProduct[index].ID == id){
//        $scope.listProduct[index].Quantity++;
//        break;
//      }
//     }

//     localStorageService.set('dataShopping', list);
//   };

//   $scope.GiamSoLuong = function(id){
//     for  (index = 0; index < $scope.listProduct.length; index++) {
//      if ($scope.listProduct[index].ID == id && $scope.listProduct[index].Quantity > 0){
//        $scope.listProduct[index].Quantity--;
//        break;
//      }
//     }

//     localStorageService.set('dataShopping', list);
//   };
// }