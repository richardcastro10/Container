(function () {
    'use strict';
    angular.module('container.header').controller('HeaderCtrl', HeaderCtrl);
    HeaderCtrl.$inject = [];

    function HeaderCtrl() {
        var vm = this;
        vm.changeCollapseMenu = changeCollapseMenu;

        load();

        function load() {
            vm.isCollapsed = true;
        }

        function changeCollapseMenu() {
            vm.isCollapsed = !vm.isCollapsed;
        }
    }
})();