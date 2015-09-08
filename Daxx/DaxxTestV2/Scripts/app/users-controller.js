var app = angular.module('DaxxApp', ['ngRoute', 'ngComboBox']);

app.config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {
	$routeProvider
		.when('/', {
			templateUrl: '/Views/list.html',
			controller: 'MoviesListController'
		})
		.when('/movies/add', {
			templateUrl: '/Views/add.html',
			controller: 'MoviesAddController'
		})
		.when('/movies/edit/:id', {
			templateUrl: '/Views/edit.html',
			controller: 'MoviesEditController'
		})
		.when('/movies/delete/:id', {
			templateUrl: '/Views/delete.html',
			controller: 'MoviesDeleteController'
		});
 
	$locationProvider.html5Mode(true); 
}]);

app.controller('DaxxCtrl', function ($scope, $http, $log) {
		
		$http.get("/api/country").success(function (data, status, headers, config) {
			$scope.countries = data;
		}).error(function (data, status, headers, config) {
			$log(data);			
		});

		$scope.user = {};
		$scope.user.email = 2 + 2 ;	
		$scope.user.password = 'testpass';
		$scope.user.agreement = false;

		$scope.user.selectedCountry = {};
		$scope.user.selectedProvince = {};
		
		$scope.isValid = false;
		$scope.testUser = '';
		
		$scope.save = function() {
			var i = 5;
			var userDto = {
				'Id' : '0',
				'Login' : $scope.user.email,
				'Password' : $scope.user.password,
				'AgreeToWorkForFood' : $scope.user.agreement,
				'CountryId' : $scope.user.selectedCountry.id,
				'ProvinceId' : $scope.user.selectedProvince.id
			};

			$http.post('/api/users',
			    userDto,
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
			).success(function (data, status, headers, config) {
				$scope.correctAnswer = (data === "true");
				$scope.working = false;
			}).error(function (data, status, headers, config) {
				$scope.title = "Oops... something went wrong";
				$scope.working = false;
			});
		};

		$scope.next = function () {
			var i = 4;
			$http.get("/api/users").success(function (data, status, headers, config) {
				$scope.options = data.options;
				$scope.title = data.title;
				$scope.answered = false;
				$scope.working = false;
				$scope.testUser = data[0];
			}).error(function (data, status, headers, config) {
				$scope.title = "Oops... something went wrong";
				$scope.working = false;
			});

			$scope.email = 'asdf';
			$scope.pass = 'asdf';
			$scope.passConfirm = 'fda';		
			$scope.agree = true;
		};

		$scope.sendAnswer = function (option) {
			$scope.working = true;
			$scope.answered = true;

			$http.post('/api/users', { }).success(function (data, status, headers, config) {
				$scope.correctAnswer = (data === "true");
				$scope.working = false;
			}).error(function (data, status, headers, config) {
				$scope.title = "Oops... something went wrong";
				$scope.working = false;
			});
    };
});

app.directive('compareTo', function() {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function(scope, element, attributes, ngModel) {
             
            ngModel.$validators.compareTo = function(modelValue) {
                return modelValue == scope.otherModelValue;
            };
 
            scope.$watch("otherModelValue", function() {
                ngModel.$validate();
            });
        }
    };
});
