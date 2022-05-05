
angular.module('container.core', [
    'ngRoute', 'ui.router', 'ngResource', 'infinite-scroll'
]);

var myApp = angular.module('container', [
    'container.core',
    'container.cliente',
    'container.container',
    'container.movimentacao',
    'container.dashboard',
    'container.header'
]);

angular.module('container.cliente', ['container.core']);
angular.module('container.header', ['container.core']);
angular.module('container.container', ['container.core']);
angular.module('container.movimentacao', ['container.core']);
angular.module('container.dashboard', ['container.core']);
