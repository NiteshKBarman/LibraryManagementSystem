var app = angular.module('studentBookEnrollment', []);
app.controller('myCtrl', function ($scope, $http) {
    $scope.studentList = [];
    $scope.bookList = [];
    $scope.studentBookEnrollment = [];
    $scope.init = function (studentData, bookData, studentBookEnrollment) {
        debugger;
        //var data = [{ Id: 1, Name: "Ram" }, { Id: 2, Name: "Shyam" },
        //{ Id: 3, Name: "Gita" }, { Id: 4, Name: "Nita" }, { Id: 5, Name: "Hari" }
        //];
        $scope.studentList = studentData;
        $scope.selectedName = $scope.studentList.filter(x => x.Id == 1)[0];

        //var book = [{ Id: 1, Name: "Math" }, { Id: 2, Name: "Science" },
        //{ Id: 3, Name: "Big Data" }, { Id: 4, Name: "HPC" }];
        $scope.bookList = bookData;
        $scope.studentBookEnrollment = studentBookEnrollment;
        $scope.displayStudentBookName();
        $scope.checkAlreadyExistData();
        //$scope.onChangeStudentName();
    };
    $scope.checkAlreadyExistData = function () {
        angular.forEach($scope.studentBookEnrollment, function (value, key) {
            var index = $scope.bookList.findIndex(x => x.Id == value.BookId);
            if (index != -1) {
                $scope.bookList[index].bookCheck = true;
            }
        });
    }
    $scope.onClickCheckBox = function () {

        if ($scope.checkValue) {
            angular.forEach($scope.bookList, function (value, key) {
                value.bookCheck = true;
            });
        } else {
            angular.forEach($scope.bookList, function (value, key) {
                value.bookCheck = false;
            });
        }
    }
    $scope.onChangeStudentName = function () {
        debugger;
        var studentId = $scope.selectedName.Id;
        var filterData = $scope.studentBookCheckList.filter(x => x.studentId
            == studentId);
        if (filterData.length > 0) {
            angular.forEach($scope.bookList, function (value, key) {
                var obtData = filterData.filter(x => x.bookId == value.Id)[0];
                if (obtData != null) {
                    value.bookCheck = true;
                } else {
                    value.bookCheck = false;
                }
            });

        } else {
            angular.forEach($scope.bookList, function (value, key) {
                value.bookCheck = false;
            });
        }

    }
    $scope.studentBookCheckList = [];
    $scope.onClickAddBtn = function () {
        debugger;
        var studentId = $scope.selectedName.Id;
        $scope.clearListData(studentId);

        angular.forEach($scope.bookList, function (value, key) {
            if (value.bookCheck) {
                var data = {
                    studentId: studentId,
                    bookId: value.Id
                }
                $scope.studentBookCheckList.push(data);
            }
        });
        $scope.displayStudentBookName();
    }
    $scope.displayList = [];
    $scope.displayStudentBookName = function () {
        angular.forEach($scope.studentBookCheckList, function (value, key) {
            var studentDatas = $scope.studentList.filter(x => x.Id == value.studentId)[0];
            var bookDatas = $scope.bookList.filter(x => x.Id == value.bookId)[0];
            if (studentDatas != null && studentDatas != "" && bookDatas != null && bookDatas != "") {
                var dataExist = $scope.displayList.filter(x => x.StudentId == studentDatas.Id && x.BookId ==
                    bookDatas.Id)[0];
                if (dataExist == null || dataExist == "") {
                    var data = {
                        StudentId: studentDatas.Id,
                        BookId: bookDatas.Id,
                        StudentName: studentDatas.Name,
                        BookName: bookDatas.Name
                    }
                    $scope.displayList.push(data);
                }

            }

        });
        angular.forEach($scope.studentBookEnrollment, function (value, key) {
            var studentDatas = $scope.studentList.filter(x => x.Id == value.StudentId)[0];
            var bookDatas = $scope.bookList.filter(x => x.Id == value.BookId)[0];
            if (studentDatas != null && studentDatas != "" && bookDatas != null && bookDatas != "") {
                var dataExist = $scope.displayList.filter(x => x.StudentId == studentDatas.Id && x.BookId ==
                    bookDatas.Id)[0];
                if (dataExist == null || dataExist == "") {
                    var data = {
                        StudentId: studentDatas.Id,
                        BookId: bookDatas.Id,
                        StudentName: studentDatas.Name,
                        BookName: bookDatas.Name
                    }
                    $scope.displayList.push(data);
                }

            }

        });

    }

    $scope.clearListData = function (studentId) {
        var findData = $scope.studentBookCheckList.filter(x => x.studentId
            == studentId);
        if (findData.length > 0) {
            for (var i = $scope.studentBookCheckList.length - 1; i >= 0; i--) {
                if ($scope.studentBookCheckList[i].studentId == studentId) {
                    $scope.studentBookCheckList.splice(i, 1);
                }
            }
        }

    }
    $scope.onClickSaveBtn = function () {
        debugger;
        $http.post("/StudentBookEnrollments/saveStudentBookData", $scope.displayList)
            .then(function (response) {
                debugger;
                var data = response.data;
                alert(data);
                window.location.href = "/StudentBookEnrollments/Index";
            });

    }

});