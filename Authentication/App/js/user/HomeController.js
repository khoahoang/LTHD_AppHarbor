	mobileStoreApp.controller('HomeController', function ($http, $scope) {
    $http.get("http://localhost:41127/api/home")
    .then(function(response) {
      $scope.cat = response.data;
    });
    $http.get("http://localhost:41127/api/getacticles")
    .then(function (response) {
        $scope.acticle = response.data;
    });
	   
	})