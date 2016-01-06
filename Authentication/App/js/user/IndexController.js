'use strict';
mobileStoreApp.controller('IndexController', ['$scope', '$location', 'authService', 'shoppingService', function ($scope, $location, authService, shoppingService) {
 
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }
 
    $scope.authentication = authService.authentication;
    $scope.count = shoppingService.count();
 
}]);