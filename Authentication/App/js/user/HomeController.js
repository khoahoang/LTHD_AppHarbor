	mobileStoreApp.controller('HomeController', function ($http, $scope) {
	    $http.get("http://mobilestore.apphb.com/api/home")
    .then(function(response) {
      $scope.cat = response.data;
    });
})