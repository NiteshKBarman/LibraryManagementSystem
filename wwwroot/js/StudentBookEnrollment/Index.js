var app = angular.module('studentBookEnrollment', []);
app.controller('myCtrl', function ($scope) {
    $scope.studentList = [];
    $scope.init = function () {
        debugger;
        var data = [{ Id: 1, Name: "Ram" }, { Id: 2, Name: "Shyam" },
        { Id: 3, Name: "Gita" }, { Id: 4, Name: "Nita" }, { Id: 5, Name: "Hari" }
        ];
        $scope.studentList = data;
    };

});