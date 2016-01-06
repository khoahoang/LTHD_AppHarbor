appAdmin.controller('managerProductController', function ($scope, $http) {
    $scope.currentPage = 1;
    var data;

    $http.get('http://localhost:41127/api/productadmin/getall')
    .then(function (response) {
        data = response.data;
        $scope.list = data.Products;
        $scope.mans = data.Mans;
        $scope.totalItems = data.Products.length;
    })

    $scope.pageChanged = function () {
        
    }

    $scope.editItem = function (item) {
        item.Editing = true;
    }

    $scope.doneEditing = function (item) {
        item.Editing = false;
        //dong some background ajax calling for persistence...
        $http.post('http://localhost:41127/api/ProductAdmin/EditProduct?id=' + item.ID + '&name=' + item.Name + '&man=' + item.NSX + '&price=' + item.Price)
        .then(function (response) {

        })
    };

    $scope.xoa = function (item) {
        var r = confirm("Xác nhận xóa!")
        if (r == true) {
            $http.post('http://localhost:41127/api/productadmin/remove?id=' + item.ID)
            .then(function (response) {
                window.alert('Xóa thành công');
                var index = data.Products.indexOf(item);
                data.Products.splice(index, 1);
            })
        }
    }
})