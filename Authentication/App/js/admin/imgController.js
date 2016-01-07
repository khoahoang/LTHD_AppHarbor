appAdmin.controller('imgController', function ($scope, $http, $routeParams) {
    $http.get('http://mobilestore.apphb.com/api/productadmin/get?id=' + $routeParams.Id)
    .then(function (response) {
        var pro = response.data;
        $scope.link = pro.PRODUCT_IMG;
    })

    $scope.upload = function () {
        var link = $scope.link;
        var ID = $routeParams.Id;
        var params = { 'ID': ID, 'Link': link };
        $http.post(http://mobilestore.apphb.com/api/productadmin/uploadimg', params)
        .success(function (response) {
            window.alert('Upload thành công');
            $scope.link = "";
        })
        .error(function (response) {
            window.alert('Upload thất bại');
        })
    }
})