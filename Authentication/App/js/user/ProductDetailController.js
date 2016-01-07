mobileStoreApp.controller('ProductDetailController', function ($scope, $routeParams, $http) {
    $http.get("http://localhost:41127/api/product/getproduct?id=" + $routeParams.proId)
    .then(function(response) {
      $scope.pro = response.data;
    });
    $http.get("http://mobilestore.apphb.com/api/home")
    .then(function (response) {
        $scope.cat = response.data;
    });
})