appAdmin.controller('orderDetailController', function ($scope, $http, $routeParams) {
    $http.get('http://localhost:41127/api/Orders/orderdetail?id=' + $routeParams.Id)
    .then(function (response) {
        $scope.detail = response.data;
    })
})

