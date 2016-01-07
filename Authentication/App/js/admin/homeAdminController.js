'use strict';
appAdmin.controller('homeAdminController', ['$scope', '$http', 'authService', function ($scope, $http, authService) {
    $http.get('http://mobilestore.apphb.com/api/Home/GetAdmin')
    .success(function (response) {

    })
    .error(function () {
        location.assign('index.html#/home');
    })
}])