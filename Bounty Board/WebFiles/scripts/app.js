(function app(angular) {
    var module = angular.module('app');

    module.controller('main', [mainControllerFunction]);

    function mainControllerFunction(test) {
        var ctrl = this;
         
        ctrl.test = "Hello World - Bounty_Board";
    }
})(angular);