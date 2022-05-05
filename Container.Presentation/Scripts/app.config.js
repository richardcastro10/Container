(function () {
    'use strict';
    angular.module('container').config(ContainerConfiguration);
    ContainerConfiguration.$inject = ['$locationProvider', '$routeProvider', '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', '$qProvider',  'TEMPLATES'];

    function ContainerConfiguration($locationProvider, $routeProvider, $stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $qProvider, TEMPLATES) {
        $qProvider.errorOnUnhandledRejections(false);
        $locationProvider.hashPrefix('');
        configureRoutes($stateProvider, $routeProvider, $urlRouterProvider, $urlMatcherFactoryProvider, TEMPLATES);
    }

    function configureRoutes($stateProvider, $routeProvider, $urlRouterProvider, $urlMatcherFactoryProvider, TEMPLATES) {
        $urlRouterProvider.deferIntercept();
        $urlMatcherFactoryProvider.strictMode(false);

        $routeProvider
            .when("/", TEMPLATES.Dashboard)
            .when("/cliente", TEMPLATES.ClienteList)
            .when("/cliente-editar/:id", TEMPLATES.ClienteEditar)
            .when("/container", TEMPLATES.ContainerList)
            .when("/container-editar/:id", TEMPLATES.ContainerEditar)
            .when("/movimentacao", TEMPLATES.MovimentacaoList)
            .when("/movimentacao-editar/:id", TEMPLATES.MovimentacaoEditar);

        //$stateProvider
        //    .state('cliente', TEMPLATES.ClienteList)
        //    .state('cliente-edit', TEMPLATES.ClienteEditar)
        //    .state('cliente-new', TEMPLATES.ClienteEditar)
        //    .state('container', TEMPLATES.ContainerList)
        //    .state('container-edit', TEMPLATES.ContainerEditar)
        //    .state('movimentacao', TEMPLATES.MovimentacaoList)
        //    .state('movimentacao-edit', TEMPLATES.MovimentacaoEditar)
        //    .state('dashboard', TEMPLATES.Dashboard);

        $urlRouterProvider.otherwise(TEMPLATES.Dashboard.url);
    }

})();



