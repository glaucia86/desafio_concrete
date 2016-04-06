'use strict';

app.factory('ordersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
        
    var serviceBase = 'https://glauthenticationdesafioconcrete.azurewebsites.net/';
    var ordersServiceFactory = {};

    var _getOrders = function() {

        return $http.get(serviceBase + 'api/orders').then(function(results) {
            return results;
        });
    };

    ordersServiceFactory.getOrders = _getOrders;

    return ordersServiceFactory;
}]);