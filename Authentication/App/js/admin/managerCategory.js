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
        var id = item.ID;
        var name = item.Name;
        var params = { 'ID': id, 'Name': name };
        //dong some background ajax calling for persistence...
        $http.post('http://localhost:41127/api/CatergoryAdmin/EditCategory', params)
        .then(function (response) {

        })
    };

    $scope.them = function () {
        var name = $scope.nameadd;
        $http.post('http://localhost:41127/api/CatergoryAdmin/AddCategory?name=' + name)
        .then(function (response) {
            $scope.nameadd = "";
            $http.get('http://localhost:41127/api/CatergoryAdmin/GetAll')
            .then(function (response) {
                $scope.list = response.data;
                $scope.totalItems = response.data.length;
            })
        })
    }
})