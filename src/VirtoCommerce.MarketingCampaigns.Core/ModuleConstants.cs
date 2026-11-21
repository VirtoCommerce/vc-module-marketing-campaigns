using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.MarketingCampaigns.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "marketing-campaigns:access";
            public const string Create = "marketing-campaigns:create";
            public const string Read = "marketing-campaigns:read";
            public const string Update = "marketing-campaigns:update";
            public const string Delete = "marketing-campaigns:delete";

            public static string[] AllPermissions { get; } =
            [
                Access,
                Create,
                Read,
                Update,
                Delete,
            ];
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor MarketingCampaignsEnabled { get; } = new()
            {
                Name = "MarketingCampaigns.Enabled",
                GroupName = "MarketingCampaigns|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return MarketingCampaignsEnabled;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
