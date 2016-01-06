mobileStoreApp.controller('SearchController', function ($http, $scope, $routeParams) {
    $scope.currentPage = 1;
    $http.get("http://localhost:41127/api/Search/Search?keyword=" + $routeParams.str + "&page=1")
      .then(function (response) {
          var product = response.data;

          $scope.pro = product;
          $scope.SearchName = $routeParams.str;

          $scope.totalItems = product.TotalPages * 10;
      });

    $scope.pageChanged = function () {
        $http.get("http://localhost:41127/api/Search/Search?keyword=" + $routeParams.str + "&page=" + $scope.currentPage)
        .then(function (response) {
            var product = response.data;

            $scope.pro = product;
            $scope.SearchName = $routeParams.str;
        });
    }
})