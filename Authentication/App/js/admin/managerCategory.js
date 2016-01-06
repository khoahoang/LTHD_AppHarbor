appAdmin.controller('managerCategory', function ($scope, $http) {
    $scope.currentPage = 1;

    $http.get('http://localhost:41127/api/CatergoryAdmin/GetAll')
    .then(function (response) {
        $scope.list = response.data;
        $scope.totalItems = response.data.length;
    })

    $scope.pageChanged = function () {

    }

    $scope.editItem = function (item) {
        item.Editing = true;
    }

    $scope.doneEditing = function (item) {
        item.Editing = false;
        //dong some background ajax calling for persistence...
        $http.post('http://localhost:41127/api/CatergoryAdmin/EditCategory?id=' + item.ID + '&name=' + item.Name)
        .then(function (response) {

        })
    };
})