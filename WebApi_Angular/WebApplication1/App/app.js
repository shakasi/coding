(function () {
    var myApp = angular.module("myApp", ['ngRoute']);
    myApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/index', {
            templateUrl: '/App/Show.html',
            controller: 'showCtrl'
        }).otherwise({ redirectTo: '/index' });
    }]);
    myApp.controller('showCtrl', ['$scope','$http', function ($scope,$http) {
        $http.get('api/UserInfo').success(function (data) {
            $scope.item = data;
        });
    }])
})();