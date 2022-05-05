(function () {
    'use strict';
    angular.module('container').run(ContainerRun);
    ContainerRun.$inject = ['$rootScope', 'TEMPLATES'];

    function ContainerRun($rootScope, TEMPLATES) {
        $rootScope.TEMPLATES = TEMPLATES;
    }
})();

