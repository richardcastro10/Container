myApp.constant('BASE_URL', '/api/');

myApp.constant('TEMPLATES', {
    ClienteList: { controllerAs: 'vm', templateUrl: 'source/cliente/clienteList.html', controller: 'ClienteListCtrl', url: '/cliente' },
    ClienteEditar: { controllerAs: 'vm', templateUrl: 'source/cliente/clienteEditar.html', controller: 'ClienteEditarCtrl', url: '/cliente-editar/:id' },

    ContainerList: { controllerAs: 'vm', templateUrl: 'source/container/containerList.html', controller: 'ContainerListCtrl', url: '/container' },
    ContainerEditar: { controllerAs: 'vm', templateUrl: 'source/container/containerEditar.html', controller: 'ContainerEditarCtrl', url: '/container-editar/:id' },

    MovimentacaoList: { controllerAs: 'vm', templateUrl: 'source/movimentacao/movimentacaoList.html', controller: 'MovimentacaoListCtrl', url: '/movimentacao' },
    MovimentacaoEditar: { controllerAs: 'vm', templateUrl: 'source/movimentacao/movimentacaoEditar.html', controller: 'MovimentacaoEditarCtrl', url: '/movimentacao-editar/:id' },

    Dashboard: { controllerAs: 'vm', templateUrl: 'source/dashboard/dashboard.html', controller: 'DashboardCtrl', url: '/' },
    Header: { controllerAs: 'vm', templateUrl: 'source/components/header/header.html', controller: 'HeaderCtrl' },
});
