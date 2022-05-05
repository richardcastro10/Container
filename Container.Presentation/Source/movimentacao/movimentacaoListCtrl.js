(function () {
    'use strict';
    angular.module('container.movimentacao').controller('MovimentacaoListCtrl', MovimentacaoListCtrl);
    MovimentacaoListCtrl.$inject = ['$controller', '$http', '$resource'];

    function MovimentacaoListCtrl($controller, $http, $resource) {
        var vm = this;
        vm.isLoading = true;

        vm.filter = {
            page: 0,
            search: '',
            orderAsc: true,
            orderBy: 'start'
        };

        vm.remove = remove;
        vm.list = list;
        vm.changeSortOrder = changeSortOrder;

        load();

        function load() {
            vm.list();
        }

        function list(addPage) {
            vm.filter.scrollDisabled = true;
            if (addPage) {
                vm.filter.page += 1;
            } else {
                vm.filter.page = 0;
            }

            if (!addPage) {
                vm.total = undefined;
                getTotal();
            }

            getList();
        }

        function getTotal() {
            $resource('/api/movimentacao/total', vm.filter, {}).get().$promise.then(function (response) {
                vm.total = response.total;
            }, function (response) {
                alert("Erro ao receber dados das movimentações.");
            });
        }

        function getList() {
            $resource('/api/movimentacao', vm.filter, {}).query().$promise.then(function (response) {
                vm.listModel = response;
                vm.isLoading = false;
                vm.listModel.scrollDisabled = angular.isDefined(vm.total) ? vm.listModel.length >= vm.total : vm.listModel.length < 20 * (vm.filter.page + 1);

            }, function (response) {
                alert("Erro ao receber dados das movimentações.");
                vm.isLoading = false;
            });
        }

        function remove(index) {
            var currentUserId = vm.listModel[index].id;

            $resource('/api/movimentacao/:id', { id: currentUserId }, {}).delete().$promise.then(function (response) {
                vm.listModel.splice(index, 1);
                vm.total -= 1;
            }, function (response) {
                alert("Erro ao remover movimentação.");
            });
        }

        function changeSortOrder(name) {
            if (name === vm.filter.orderBy) {
                vm.filter.orderAsc = !vm.filter.orderAsc;
            } else {
                vm.filter.orderAsc = true;
                vm.filter.orderBy = name;
            }
            vm.list();
        }
    }
})();