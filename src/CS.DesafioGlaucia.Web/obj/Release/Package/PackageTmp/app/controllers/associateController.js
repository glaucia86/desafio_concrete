'use strict';
app.controller('associateController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registerData = {
        userName: authService.externalAuthData.userName,
        provider: authService.externalAuthData.provider,
        externalAccessToken: authService.externalAuthData.externalAccessToken
    };

    $scope.registerExternal = function () {

        authService.registerExternal($scope.registerData).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "Usuário registrado com sucesso, você será direcionado para a página de Login em 2 segundos.";
            startTimer();
        },
            function (response) {
                var errors = [];
                for (var key in response.modelState) {
                    errors.push(response.modelState[key]);
                }

                $scope.message = "Falha ao registrar o usuário devido a:" + errors.join(' ');
            });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/orders');
        }, 2000);
    }
}]);
