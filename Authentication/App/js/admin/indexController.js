'use strict';
appAdmin.controller('indexController', ['$scope', '$http', 'authService', function ($scope, $http, authService) {
    $scope.authentication = authService.authentication;

    $scope.logout = function () {
        authService.logOut();
    }
}])