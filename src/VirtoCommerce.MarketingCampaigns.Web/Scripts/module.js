// Call this to register your module to main application
var moduleName = 'VirtoCommerce.MarketingCampaigns';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.MarketingCampaignsState', {
                    url: '/marketing-campaigns',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'VirtoCommerce.MarketingCampaigns.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.MarketingCampaigns)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/marketing-campaigns',
                icon: 'fa fa-cube',
                title: 'MarketingCampaigns',
                priority: 100,
                action: function () { $state.go('workspace.MarketingCampaignsState'); },
                permission: 'marketing-campaigns:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
