(function () {
    'use strict';
    angular.module('container.container').controller('ContainerEditarCtrl', ContainerEditarCtrl);
    ContainerEditarCtrl.$inject = ['$state', '$location', '$resource'];

    function ContainerEditarCtrl($state, $location, $resource) {
        var vm = this;
        var containerId = angular.isDefined($location.search().id) ? $location.search().id :
            angular.isDefined($state.params.id) ? $state.params.id : undefined;

        containerId = containerId == 0 ? undefined : containerId;
        vm.model = {};
        vm.save = save;


        load();

        function load() {
            setLists();

            if (containerId) {
                getContainerModel();
            }

            getClients();
        }

        function setLists() {
            vm.status = [
                { id: 0, description: "Vazio" },
                { id: 1, description: "Cheio" }
            ];

            vm.categories = [
                { id: 0, description: "Importação" },
                { id: 1, description: "Exportação" }
            ];
        }

        function getContainerModel() {
            $resource('/api/container/:id', { id: containerId }, {}).get().$promise.then(function (response) {
                vm.model = response;
            }, function (data) {
                alert("Erro ao receber dados do container.");
            });
        }

        function getClients() {
            $resource('/api/clientes/', {}, {}).query().$promise.then(function (response) {
                vm.clients = response;
            }, function (data) {
                alert("Erro ao receber dados do container.");
            });
        }

        function save(form) {
            if (form.$valid) {
                if (containerId) {
                    putData();
                } else {
                    postData();
                }
            } else {
                alert("Campos inválidos, verifique o que foi digitado e tente novamente.");
            }
        }

        function postData() {
            $resource('/api/container', {}, {}).save(vm.model).$promise.then(function (response) {
                $state.go('container');
            }, function (response) {
                alert("Erro ao salvar dados do container.");
            });
        }

        function putData() {
            $resource('/api/container/:id', { id: containerId }, {
                update: { method: 'PUT' }
            }).update(vm.model).$promise.then(function (response) {
                $state.go('container');
            }, function (response) {
                alert("Erro ao salvar dados do container.");
            });
        }
    }
})();