// Main configuration file. Sets up AngularJS module and routes and any other config objects

var appRoot = angular.module('main', ['ngRoute', 'ngGrid', 'ngResource', 'angularStart.services', 'angularStart.directives']);     //Define the main module

appRoot.config(['$routeProvider', function ($routeProvider) {
        //Setup routes to load partial templates from server. TemplateUrl is the location for the server view (Razor .cshtml view)
        $routeProvider
            .when('/home', { templateUrl: '/home/main', controller: 'DaxxCtrl' })
            .when('/contact', { templateUrl: '/home/contact', controller: 'ContactController' })
            .when('/about:user', { templateUrl: '/home/about', controller: 'DaxxCtrl2' })
            .when('/demo', { templateUrl: '/home/demo', controller: 'DemoController2' })
            .when('/angular', { templateUrl: '/home/angular' })
            .otherwise({ redirectTo: '/home' });
    }])
    .controller('RootController', ['$scope', '$route', '$routeParams', '$location', function ($scope, $route, $routeParams, $location) {
        $scope.$on('$routeChangeSuccess', function (e, current, previous) {
            $scope.activeViewPath = $location.path();
        });
    }]);

appRoot.directive('compareTo', function() {
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

appRoot.controller('DemoController2', function ($scope, $http, $location, $route, $routeParams) {	
	$http.get("/api/users").success(function (data, status, headers, config) {
		$scope.avaliableUsers = data;
	}).error(function (data, status, headers, config) {
		$scope.title = "Oops... something went wrong";
		$scope.working = false;
	});
});

appRoot.controller('DaxxCtrl2', function ($scope, $http, $location, $route, $routeParams) {	
	$scope.user = JSON.parse($routeParams.user);
	$scope.user.selectedCountry = null;
	$scope.user.selectedProvince = null;

	$http.get("/api/country").success(function (data, status, headers, config) {
		$scope.countries = data;
	}).error(function (data, status, headers, config) {
		$log(data);			
	});

	$scope.save = function() {
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
			$location.path('/demo');
		}).error(function (data, status, headers, config) {
			$scope.title = "Oops... something went wrong";
			$scope.working = false;
		});
	};
});

appRoot.controller('DaxxCtrl', function ($scope, $http, $location, $route, $routeParams) {
	$scope.user = {};
	$scope.user.email = 'test@test.com' ;	
	$scope.user.password = 'testpass';
	$scope.user.passwordConfirm = $scope.user.password;
	$scope.user.agreement = true;

	$scope.user.selectedCountry = {};
	$scope.user.selectedProvince = {};
		
	$scope.isValid = false;

	$scope.next = function () {
		var ser = JSON.stringify($scope.user);
		$location.path('/about' + ser);
	};
});
