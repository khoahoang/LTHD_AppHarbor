appAdmin.controller('managerOrder', function ($scope, $http) {
    $http.get('http://localhost:41127/api/Orders')
    .then(function (response) {
        $scope.list = response.data;
    })

    $scope.xoa = function (id) {
        var c = confirm('Xác nhận xóa!');
        if (c == true) {
            $http.post('http://localhost:41127/api/Orders/remove?id=' + id)
            .then(function (response) {
                window.alert('Xóa thành công!');
                $http.get('http://localhost:41127/api/Orders')
                .then(function (response3) {
                    $scope.list = response3.data;
                })
            })
        }
    }

    $scope.thanhToan = function (id) {
        var c = confirm('Xác nhận đã thanh toán!');
        if (c == true) {
            $http.post('http://localhost:41127/api/Orders/paid?id=' + id)
            .then(function (response) {
                $http.get('http://localhost:41127/api/Orders')
                .then(function (response3) {
                    $scope.list = response3.data;
                })
            })
        }
    }
})