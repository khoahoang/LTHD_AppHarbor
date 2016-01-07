var mobileStoreApp = angular.module('mobileStoreApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);


mobileStoreApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/home', {
              templateUrl: 'app/template/user/home.html',
              controller: 'HomeController'
          }).

          when('/contact', {
              templateUrl: 'app/template/user/contact.html'
          }).

          when('/about', {
              templateUrl: 'app/template/user/about.html'
          }).

          when('/showproductbycategory/:catId', {
              templateUrl: 'app/template/user/showproductbycategory.html',
              controller: 'ShowProductByCategoryController'
          }).

          when('/shoppingcart', {
              templateUrl: 'app/template/user/shoppingcart.html',
              controller: 'ShoppingCartController'
          }).

          when('/addproduct/:proId', {
              templateUrl: 'app/template/user/shoppingcart.html',
              controller: 'AddProductToShoppingCartController'
          }).

          when('/productdetail/:proId', {
              templateUrl: 'app/template/user/productdetail.html',
              controller: 'ProductDetailController'
          }).

          when('/login', {
              templateUrl: 'app/template/user/login.html',
              controller: 'LoginController'
          }).

          when('/signup', {
              templateUrl: 'app/template/user/signup.html',
              controller: 'SignUpController'
          }).

          when('/search/:str', {
              templateUrl: 'app/template/user/search.html',
              controller: 'SearchController'
          }).

          when('/managerAccount', {
              templateUrl: 'app/template/user/managerAccount.html',
              controller: 'ManagerAccountController'
          }).

          when("/associate", {              
              templateUrl: "app/template/user/associate.html",
              controller: "AssociateController"
          }).

          when("/order", {
              templateUrl: "app/template/user/order.html",
              controller: "OrdersController"
          }).

          otherwise({
              redirectTo: '/home'
          });
  }]);


var serviceBase = 'http://localhost:41127/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
mobileStoreApp.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});


mobileStoreApp.run(['authService', function (authService) {
    authService.fillAuthData();
    authService.setDataShopping();
}]);
/*
 .run( function($rootScope, $location) {

    // register listener to watch route changes
    $rootScope.$on( "$routeChangeStart", function(event, next, current) {
      if ( $rootScope.loggedUser == null ) {
        // no logged user, we should be going to #login
        if ( next.templateUrl == "partials/login.html" ) {
          // already going to #login, no redirect needed
        } else {
          // not going to #login, we should redirect now
          $location.path( "/login" );
        }
      }         
    });
    */
mobileStoreApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});


