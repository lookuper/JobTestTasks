angular.module('DaxxApp', [])
    .controller('DaxxCtrl', function ($scope, $http) {
        $scope.email = '';
		$scope.pass = '';
		$scope.passConfirm = '';		
		$scope.agree = false;
		$scope.isValie = false;
		$scope.testUser = '';
		
		$scope.next = function () {
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

		$scope.save = function() {
			$http.post('/api/trivia', { 'questionId': option.questionId, 'optionId': option.id }).success(function (data, status, headers, config) {
				$scope.correctAnswer = (data === "true");
				$scope.working = false;
        }).error(function (data, status, headers, config) {
				$scope.title = "Oops... something went wrong";
				$scope.working = false;
        });
		}
    });