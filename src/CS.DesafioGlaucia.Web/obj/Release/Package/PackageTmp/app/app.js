/* Arquivo responsável para a manipulação das páginas via controller através do
AngularJs * /
 */

var app = angular.module('SpaApplicationAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl:"/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensController",
        templateUrl: "/app/view/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl:"/app/view/associate.html"
    });

    /*Redirecionar para a Página Principal quando clicar em qualquer parte da página*/
    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'https://glauthenticationdesafioconcrete.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run([
    'authService', function (authService) {
        authService.fillAuthData();
}]);