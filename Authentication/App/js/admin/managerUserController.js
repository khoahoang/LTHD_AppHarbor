appAdmin.controller('managerUserController', function ($scope, $http) {
    $scope.them = function () {
        var username = $scope.nameadd;
        var pass = $scope.pass;
        var passconfirm = $scope.passconfirm;
        var params = { 'UserName': username, 'Password': pass, 'ConfirmPassword': passconfirm };
        $http.post('http://localhost:41127/api/account/register', params)
        .then(function (response) {
            $scope.nameadd = "";
            $scope.pass = "";
            $scope.passconfirm = "";
            window.alert('Thêm thành công');
            return response;
        });
    }
})