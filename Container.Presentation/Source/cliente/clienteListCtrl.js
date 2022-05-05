(function () {
    'use strict';
    angular.module('container.cliente').controller('ClienteListCtrl', ClienteListCtrl);
    ClienteListCtrl.$inject = ['$resource'];

    function ClienteListCtrl($resource) {
        var vm = this;

        vm.removeClient = removeClient;
        vm.list = list;
        vm.changeSortOrder = changeSortOrder;

        vm.isLoading = true;

        vm.filter = {
            page: 0,
            search: '',
            orderAsc: true
        }; 

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
            $resource('/api/clientes/total', vm.filter, {}).get().$promise.then(function (response) {
                vm.total = response.total;
            }, function (response) {
                alert("Erro ao receber dados dos clientes.");
            });
        }

        function getList() {
            $resource('/api/clientes', vm.filter, {}).query().$promise.then(function (response) {
                vm.listModel = response;
                vm.isLoading = false;
                vm.listModel.scrollDisabled = angular.isDefined(vm.total) ? vm.listModel.length >= vm.total : vm.listModel.length < 20 * (vm.filter.page + 1);

            }, function (response) {
                alert("Erro ao receber dados dos clientes.");
                vm.isLoading = false;
            });
        }

        function removeClient(index) {
            var currentUserId = vm.listModel[index].id;

            $resource('/api/clientes/' + currentUserId, {}, {}).delete().$promise.then(function (response) {
                vm.listModel.splice(index, 1);
            }, function (response) {
                alert("Erro ao remover cliente.");
            });
        }

        function changeSortOrder() {
            vm.filter.orderAsc = !vm.filter.orderAsc;
            vm.list();
        }
    }
})();