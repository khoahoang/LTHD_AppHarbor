var appAdmin = angular.module('appAdmin', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);


appAdmin.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/home', {
              templateUrl: 'app/template/admin/home.html',
              controller: 'HomeAdminController'
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
              controller: 'managerOrderController'
          }).

          when('/orderDetail', {
              templateUrl: 'app/template/admin/orderDetail.html'
          }).

          otherwise({
              redirectTo: '/home'
          });
  }]);


