angular.module('VirtoCommerce.MarketingCampaigns')
    .factory('VirtoCommerce.MarketingCampaigns.webApi', ['$resource', function ($resource) {
        return $resource('api/marketing-campaigns');
    }]);
