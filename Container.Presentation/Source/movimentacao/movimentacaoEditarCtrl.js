(function () {
    'use strict';
    angular.module('container.movimentacao').controller('MovimentacaoEditarCtrl', MovimentacaoEditarCtrl);
    MovimentacaoEditarCtrl.$inject = ['$state', '$location', '$resource', '$routeParams'];

    function MovimentacaoEditarCtrl($state, $location, $resource, $routeParams) {
        var vm = this;
        var movimentacaoId = $routeParams.id;
        movimentacaoId = movimentacaoId == 0 ? undefined : movimentacaoId;
        vm.model = {};
        vm.save = save;
        vm.goBack = goBack;

        load();

        function load() {
            setLists();

            if (movimentacaoId) {
                getMovimentacaoModel();
            }

            getMovimentacoes();
        }

        function goBack() {
            $location.path('/movimentacao');
        }

        function setLists() {
            vm.status = [
                { id: 0, description: "Embarque" },
                { id: 1, description: "Descarga" },
                { id: 2, description: "Gate In" },
                { id: 3, description: "Gate Out" },
                { id: 4, description: "Reposicionamento" },
                { id: 5, description: "Pesagem" },
                { id: 6, description: "Scanner" },
            ];
        }

        function getMovimentacaoModel() {
            $resource('/api/movimentacao/:id', { id: movimentacaoId }, {}).get().$promise.then(function (response) {
                vm.model = response;
                vm.model.start = new Date(response.start);

                if (response.end)
                    vm.model.end = new Date(response.end);

            }, function (data) {
                alert("Erro ao receber dados da movimentação.");
            });
        }

        function getMovimentacoes() {
            $resource('/api/container', {}, {}).query().$promise.then(function (response) {
                vm.containers = response;
            }, function (data) {
                alert("Erro ao receber dados da movimentação.");
            });
        }

        function save(form) {
            if (form.$valid) {
                if (movimentacaoId) {
                    putData();
                } else {
                    postData();
                }
            } else {
                alert("Campos inválidos, verifique o que foi digitado e tente novamente.");
            }
        }

        function postData() {
            $resource('/api/movimentacao', {}, {}).save(vm.model).$promise.then(function (response) {
                goBack();
            }, function (response) {
                alert("Erro ao salvar dados da movimentação.");
            });
        }

        function putData() {
            $resource('/api/movimentacao/:id', { id: movimentacaoId }, {
                update: { method: 'PUT' }
            }).update(vm.model).$promise.then(function (response) {
                goBack();
            }, function (response) {
                alert("Erro ao salvar dados da movimentação.");
            });
        }
    }
})();