﻿angular.module("todoApp", ['ui.router', 'ngResource'])
    .config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
        $httpProvider.defaults.headers.delete = { 'Content-Type': 'application/json' }; //default to application/json for deletes
        
        //set up robust, extendable routing that supports 1-to-lots of different pages
        $urlRouterProvider.otherwise("/todoLists");
        $stateProvider
            .state('todoLists', {
                url: "/todoLists",
                templateUrl: "/WebApiAngular/Views/todoLists.html",
                controller: 'todoLists'
            })
            .state('todoLists.detail', {
                url: "/:id",
                templateUrl: "/WebApiAngular/Views/todoLists.detail.html",
                controller: 'todoDetail'
            });
    })

    .factory('Parkings', function ($resource) {
        //resource provides easy interaction with a RESTful endpoint. DI'ed into controllers below
        return new $resource('/api/Parking/:id');
    })

    .controller('todoLists', function ($scope, Parkings) {
        $scope.parkings = Parkings.query();

        //delete remove just the item from DB and client, don't re-query. less chatty
        $scope.delete = function (parking, index) {
            todoList.$delete(function () {
                $scope.parkings.splice(index, 1);
            });
        };

        $scope.$on('todoChanged', function() { //could get crazy here and ONLY replace the item that changed, but not doing for brevity
            $scope.parkings = Parkings.query();
        });
    });

    //.controller('todoDetail', function ($scope, TodoList, $stateParams, $state) {
    //    $scope.todoList =  $stateParams.id === 'new' ? 
    //        new TodoList({Items: []}) :
    //        TodoList.get({ id: $stateParams.id });

    //    $scope.save = function() {
    //        $scope.todoList.$save(function () {
    //            $scope.errors = null;
    //            $state.go('^');
    //            $scope.$emit('todoChanged');
    //        }, function(resp) {
    //            $scope.errors = resp.data;
    //        });
    //    };
    //});