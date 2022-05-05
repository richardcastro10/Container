(function () {
    'use strict';
    angular.module('container.cliente').controller('ClienteEditarCtrl', ClienteEditarCtrl);
    ClienteEditarCtrl.$inject = ['$state', '$location', '$resource'];

    function ClienteEditarCtrl($state, $location, $resource) {
        var vm = this;
        var userId = angular.isDefined($location.search().id) ? $location.search().id :
            angular.isDefined($state.params.id) ? $state.params.id : undefined;

        userId = userId == 0 ? undefined : userId;

        vm.model = {};
        vm.save = save;
        vm.goBack = goBack;

        load();

        function load() {
            if (userId) {
                getUserModel();
            }
        }

        function getUserModel() {
            $resource('/api/clientes/:id', { id: userId}, {}).get().$promise.then(function (response) {
                vm.model = response;
            }, function (data) {
                alert("Erro ao receber dados do cliente.");
            });
        }

        function goBack() {
            $state.go('movimentacao');
        }

        function save(form) {
            if (form.$valid) {
                if (userId) {
                    putData();
                } else {
                    postData();
                }
            } else {
                alert("Campos inválidos, verifique o que foi digitado e tente novamente.");
            }
        }

        function postData() {
            $resource('/api/clientes', {}, {}).save(vm.model).$promise.then(function (response) {
                goBack();
            }, function (response) {
                alert("Erro ao salvar dados do cliente.");
            });
        }

        function putData() {
            $resource('/api/clientes/:id', { id: userId }, {
                update: { method: 'PUT' }
            }).update(vm.model).$promise.then(function (response) {
                goBack();
            }, function (response) {
                alert("Erro ao salvar dados do cliente.");
            });
        }
    }
})();