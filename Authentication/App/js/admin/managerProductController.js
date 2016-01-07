appAdmin.controller('managerProductController', function ($scope, $http) {
    $scope.currentPage = 1;
    var data;

    $http.get('http://localhost:41127/api/productadmin/getall')
    .then(function (response) {
        data = response.data;
        $scope.list = data.Products;
        $scope.mans = data.Mans;
        $scope.cats = data.Cats;
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
        $http.post('http://localhost:41127/api/ProductAdmin/EditProduct?id=' + item.ID + '&name=' + item.Name + '&cat=' + item.Category + '&man=' + item.NSX + '&price=' + item.Price)
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

    $scope.them = function () {
        var name = $scope.nameadd;
        var price = $scope.priceadd;
        var man = $scope.manadd;
        var cat = $scope.catadd;
        var params = { 'Name': name, 'Price': price, 'Category': cat, 'NSX': man };
        $http.post('http://localhost:41127/api/productadmin/addproduct', params)
        .then(function (response) {
            $http.get('http://localhost:41127/api/productadmin/getall')
            .then(function (response2) {
                data = response2.data;
                $scope.list = data.Products;
                $scope.totalItems = data.Products.length;
            })

            $scope.nameadd = "";
            $scope.priceadd = "";
            $scope.manadd = "";
            $scope.catadd = "";

            window.alert('Thêm thành công');
        })
    }
})