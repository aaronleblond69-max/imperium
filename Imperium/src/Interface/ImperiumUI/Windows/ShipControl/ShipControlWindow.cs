#region

using Imperium.Interface.Common;
using Imperium.Interface.ImperiumUI.Windows.ShipControl.Widgets;
using Imperium.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Imperium.Interface.ImperiumUI.Windows.ShipControl;

internal class ShipControlWindow : ImperiumWindow
{
    private Transform content;

    protected override void InitWindow()
    {
        content = transform.Find("Content");

        InitSettings();

        RegisterWidget<Destinations>(content, "Destinations");
    }

    private void InitSettings()
    {
        ImpToggle.Bind(
            "ShipSettings/InstantTakeoff",
            content,
            Imperium.ShipManager.InstantTakeoff,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Instant Takeoff",
                Description = "Skips the ship's take-off animation.",
                Tooltip = tooltip
            }
        );
        ImpToggle.Bind(
            "ShipSettings/InstantLanding",
            content,
            Imperium.ShipManager.InstantLanding,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Instant Landing",
                Description = "Skips the ship's landing animation.",
                Tooltip = tooltip
            }
        );
        {
            // TODO: temporary hack while native UI is missing
            var grid = content.Find("ShipSettings");
            var source = content.Find("ShipSettings/InstantLanding");
            var instantRoute = Instantiate(source.gameObject, grid);
            instantRoute.name = "InstantRoute";
            var text = (instantRoute.transform.Find("Text") ?? instantRoute.transform.Find("Text (TMP)"))?.GetComponent<TMP_Text>();
            text.text = "Instant Route";
            // place it directly after the original
            instantRoute.transform.SetSiblingIndex(source.GetSiblingIndex() + 1);

            // add an empty spacer cell after the clone
            var spacer = new GameObject("EmptyCell", typeof(RectTransform));
            spacer.transform.SetParent(grid, false);
            spacer.transform.SetSiblingIndex(instantRoute.transform.GetSiblingIndex() + 1);

            // force grid to resize vertically
            var gridRect = grid.GetComponent<RectTransform>();
            var fitter = ImpUtils.GetOrAddComponent<ContentSizeFitter>(grid);
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            LayoutRebuilder.ForceRebuildLayoutImmediate(gridRect);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        }
        ImpToggle.Bind(
            "ShipSettings/InstantRoute",
            content,
            Imperium.ShipManager.InstantRoute,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Instant Route",
                Description = "Skips the ship's route animation.",
                Tooltip = tooltip
            }
        );
        ImpToggle.Bind(
            "ShipSettings/OverrideDoors",
            content,
            Imperium.Settings.Ship.OverwriteDoors,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Override Doors",
                Description = "Enables door ship controls when in space.",
                Tooltip = tooltip
            }
        );
        ImpToggle.Bind(
            "ShipSettings/PreventLeave",
            content,
            Imperium.ShipManager.PreventShipLeave,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Prevent Leave",
                Description = "Disables the ship's automatic leave timer.",
                Tooltip = tooltip
            }
        );
        ImpToggle.Bind(
            "ShipSettings/DisableAbandoned",
            content,
            Imperium.ShipManager.DisableAbandoned,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Disable Abandoned",
                Description =
                    "Prevents the game from killing abandoned players.\nAll players will be teleported into the ship instead.",
                Tooltip = tooltip
            }
        );
        ImpToggle.Bind(
            "ShipSettings/MuteSpeaker",
            content,
            Imperium.Settings.Ship.MuteSpeaker,
            theme: theme,
            tooltipDefinition: new TooltipDefinition
            {
                Title = "Mute Speaker",
                Description = "Please just shut up!",
                Tooltip = tooltip
            }
        );
    }
}