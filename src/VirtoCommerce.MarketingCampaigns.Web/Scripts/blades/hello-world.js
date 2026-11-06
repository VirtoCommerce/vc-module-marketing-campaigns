angular.module('VirtoCommerce.MarketingCampaigns')
    .controller('VirtoCommerce.MarketingCampaigns.helloWorldController', ['$scope', 'VirtoCommerce.MarketingCampaigns.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'MarketingCampaigns';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'MarketingCampaigns.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
