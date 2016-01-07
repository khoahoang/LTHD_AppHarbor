mobileStoreApp.controller('ShowProductByCategoryController', function ($scope, $routeParams, $http) {
    $http.get("http://mobilestore.apphb.com/api/category/getproductofcategory?id=" + $routeParams.catId)
    .then(function (response) {
        var list = response.data;
        $scope.pro = list.listProduct;
        $scope.Category = list.category.CATEGORY_NAME;
    });
})