appAdmin.controller('orderDetailController', function ($scope, $http, $routeParams) {
    $http.get('http://mobilestore.apphb.com/api/Orders/orderdetail?id=' + $routeParams.Id)
    .then(function (response) {
        $scope.detail = response.data;
    })
})

