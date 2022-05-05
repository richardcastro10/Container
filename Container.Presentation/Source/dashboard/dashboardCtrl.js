(function () {
    'use strict';
    angular.module('container.dashboard').controller('DashboardCtrl', DashboardCtrl);
    DashboardCtrl.$inject = ['$controller', '$state', '$location', '$resource'];

    function DashboardCtrl($controller, $state, $location, $resource) {
        var vm = this;
        vm.isLoadingChart = true;
        vm.isLoading = true;

        vm.model = {};

        load();

        function load() {
            getData();
            getChartData();
        }

        function getData() {
            $resource('/api/dashboard', {}, {}).get().$promise.then(function (response) {
                vm.listModel = response.relatorios;
                vm.totalImport = response.totalImport;
                vm.totalExport = response.totalExport;
                vm.isLoading = false;
            }, function (data) {
                alert("Erro ao receber dados.");
            });
        }

        function getChartData() {
            $resource('/api/dashboard/panel', {}, {}).get().$promise.then(function (response) {
                response.dates.forEach(d => {
                    d = new Date(d);
                });
                vm.modelChartData = response;
                
                const ctx = document.getElementById('myChart').getContext('2d');
                const myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: vm.modelChartData.dates,
                        datasets: [{
                            label: 'Qtd. de Importações',
                            data: vm.modelChartData.countImport,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                            ],
                            borderColor: [
                                'rgba(255, 99, 132, 1)'
                            ],
                            borderWidth: 1
                        }, {
                            label: 'Qtd. de Exportações',
                                data: vm.modelChartData.countExport,
                                backgroundColor: [
                                    'rgba(54, 162, 235, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(54, 162, 235, 1)'
                                ],
                                borderWidth: 1
                            }]
                    },
                    options: {
                        maintainAspectRatio: false,
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });

                vm.isLoadingChart = false;
            }, function (data) {
                alert("Erro ao receber dados.");
            });
        }
    }
})();