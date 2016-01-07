var appAdmin = angular.module('appAdmin', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);


appAdmin.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/home', {
              templateUrl: 'app/template/admin/home.html',
              controller: 'homeAdminController'
          }).

          when('/managerProduct', {
              templateUrl: 'app/template/admin/managerProduct.html',
              controller: 'managerProductController'
          }).

          when('/managerCategory', {
              templateUrl: 'app/template/admin/managerCategory.html',
              controller: 'managerCategory'
          }).

          when('/managerAccount', {
              templateUrl: 'app/template/admin/managerAccount.html',
              controller: 'managerUserController'
          }).

          when('/managerOrder', {
              templateUrl: 'app/template/admin/managerOrder.html',
              controller: 'managerOrder'
          }).

          when('/orderDetail/:Id', {
              templateUrl: 'app/template/admin/orderDetail.html',
              controller: 'orderDetailController'
          }).

          when('/productimg/:Id', {
              templateUrl: 'app/template/admin/productimg.html',
              controller: 'imgController'
          }).

          when('/managerUser', {
              templateUrl: 'app/template/admin/managerUser.html',
              controller: 'managerUserController'
          }).

          otherwise({
              redirectTo: '/home'
          });
  }]);


appAdmin.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

var serviceBase = 'http://localhost:41127/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
appAdmin.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

appAdmin.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

