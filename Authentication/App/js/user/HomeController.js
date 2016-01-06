	mobileStoreApp.controller('HomeController', function ($http, $scope) {
    $http.get("http://localhost:41127/api/home")
    .then(function(response) {
      $scope.cat = response.data;
    });
})